using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace screentimebreak {
    public partial class TimerOverlayForm : Form {
        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        const int GWL_EXSTYLE = -20;
        const int WS_EX_LAYERED = 0x80000;
        const int WS_EX_TRANSPARENT = 0x20;

        private int breakCount = 0;

        public bool ScreenMode { get; set; }
        public Label ScreenTimeLabel { get; set; }
        private Label breakTimeLabel;

        public TimerOverlayForm() {
            InitComponents();
        }

        private void InitComponents() {
            // Labels
            ScreenTimeLabel = new Label();
            ScreenTimeLabel.Font = new Font("Calibri", 15);
            ScreenTimeLabel.Location = new Point(1600, 5);
            ScreenTimeLabel.ForeColor = Properties.Settings.Default.ScreenTimerColor;
            ScreenTimeLabel.Visible = Properties.Settings.Default.ShowScreenTimer;
            ScreenTimeLabel.AutoSize = true;

            breakTimeLabel = new Label();
            breakTimeLabel.Font = new Font("Calibri", 50);
            breakTimeLabel.ForeColor = Color.White;
            breakTimeLabel.Location = new Point(500, 500);
            breakTimeLabel.Visible = false;
            breakTimeLabel.AutoSize = true;

            Controls.Add(ScreenTimeLabel);
            Controls.Add(breakTimeLabel);

            // Visual properties
            FormBorderStyle = FormBorderStyle.None;
            BackColor = Color.Black;
            TransparencyKey = BackColor;
            TopMost = true;

            SetSizeToAllScreens();

            // Call function TimerOverlayForm_Load on form load
            Load += new EventHandler(TimerOverlayForm_Load);
        }

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            // Enable clicking through the form
            var initialStyle = GetWindowLong(this.Handle, GWL_EXSTYLE);
            SetWindowLong(this.Handle, GWL_EXSTYLE, initialStyle | WS_EX_LAYERED| WS_EX_TRANSPARENT);
        }

        // Set the form's size to cover all screens
        private void SetSizeToAllScreens() {
            Rectangle r = new Rectangle();

            foreach (Screen s in Screen.AllScreens) {
                r = Rectangle.Union(r, s.Bounds);
            }

            Top = r.Top;
            Left = r.Left;
            Width = r.Width;
            Height = r.Height;
        }

        private void TimerOverlayForm_Load(object sender, EventArgs e) {
            TrayMenu trayMenu = new TrayMenu(this);
            StartMainLoop();
        }

        // Start the main timer loop
        private void StartMainLoop() {
            int screenTimeMinutes = Properties.Settings.Default.ScreenTimeMinutes;
            int screenTimeSeconds = Properties.Settings.Default.ScreenTimeSeconds;
            int breakTimeMinutes = Properties.Settings.Default.BreakTimeMinutes;
            int breakTimeSeconds = Properties.Settings.Default.BreakTimeSeconds;
            int megaBreakTimeMinutes = Properties.Settings.Default.MegaBreakTimeMinutes;
            int megaBreakTimeSeconds = Properties.Settings.Default.MegaBreakTimeSeconds;

            int screenTime = (screenTimeMinutes * 60) + screenTimeSeconds;
            int breakTime = (breakTimeMinutes * 60) + breakTimeSeconds;
            int megaBreakTime = (megaBreakTimeMinutes * 60) + megaBreakTimeSeconds;

            // Total countdown time left (seconds)
            int totalCountdownTimeLeft = screenTime;

            int countdownMinutesLeft;
            int countdownSecondsLeft;

            ScreenMode = true;
            bool megaBreak = false;

            Timer countdownTimer = new Timer();
            countdownTimer.Interval = 1000;
            countdownTimer.Enabled = true;

            countdownTimer.Tick += new EventHandler(countdownTimer_Tick);

            void countdownTimer_Tick(object sender, EventArgs e) {

                if (totalCountdownTimeLeft <= 0) {
                    if (ScreenMode) {
                        EnterBreakMode();
                    }
                    else {
                        EnterScreenMode();
                    }
                }

                countdownMinutesLeft = totalCountdownTimeLeft / 60;
                countdownSecondsLeft = totalCountdownTimeLeft - (60 * countdownMinutesLeft);

                if (ScreenMode) {
                    ScreenTimeLabel.Text = countdownMinutesLeft.ToString("00") + ":" + countdownSecondsLeft.ToString("00");
                    ScreenTimeLabel.Refresh();
                }
                else {
                    if (megaBreak) {
                        breakTimeLabel.Text = "It's time to take a longer break.\nTime left: " + countdownMinutesLeft.ToString("00") + ":" + countdownSecondsLeft.ToString("00");
                        breakTimeLabel.Refresh();
                    }
                    else {
                        breakTimeLabel.Text = "It's time to take a break.\nTime left: " + countdownMinutesLeft.ToString("00") + ":" + countdownSecondsLeft.ToString("00");
                        breakTimeLabel.Refresh();
                    }
                }
                totalCountdownTimeLeft--;
            }

            void EnterScreenMode() {
                ScreenMode = true;
                FadeOutOfBreakScreen();
                breakTimeLabel.Visible = false;
                totalCountdownTimeLeft = screenTime;
            }

            void EnterBreakMode() {
                if (Properties.Settings.Default.MegaBreaksEnabled) {
                    if (breakCount == Properties.Settings.Default.BreaksBeforeMegaBreak) {
                        megaBreak = true;
                    }
                    else {
                        megaBreak = false;
                    }
                }
                else {
                    megaBreak = false;
                }

                ScreenMode = false;
                FadeIntoBreakScreen();
                if (Properties.Settings.Default.ShowScreenTimer) {
                    ScreenTimeLabel.Visible = false;
                }
                breakTimeLabel.Visible = true;

                if (megaBreak) {
                    breakCount = 0;
                    totalCountdownTimeLeft = megaBreakTime;
                }
                else {
                    breakCount++;
                    totalCountdownTimeLeft = breakTime;
                }
            }

            void FadeIntoBreakScreen() {
                double maxOpacity = 0.70f;
                Opacity = 0.00;
                Timer fadeInTimer = new Timer();
                fadeInTimer.Interval = 10;
                fadeInTimer.Enabled = true;

                fadeInTimer.Tick += new EventHandler(fadeInTimer_Tick);

                TransparencyKey = Color.Magenta;

                void fadeInTimer_Tick(object sender, EventArgs e) {
                    if (Opacity < maxOpacity) {
                        Opacity += 0.02;
                    }
                    else {
                        fadeInTimer.Enabled = false;
                    }
                }
            }

            void FadeOutOfBreakScreen() {
                Timer fadeOutTimer = new Timer();
                fadeOutTimer.Interval = 10;
                fadeOutTimer.Enabled = true;

                fadeOutTimer.Tick += new EventHandler(fadeOutTimer_Tick);

                void fadeOutTimer_Tick(object sender, EventArgs e) {
                    if (Opacity > 0.00) {
                        Opacity -= 0.02;
                    }
                    else {
                        TransparencyKey = BackColor;
                        Opacity = 1.0;
                        fadeOutTimer.Enabled = false;
                        if (Properties.Settings.Default.ShowScreenTimer) {
                            ScreenTimeLabel.Visible = true;
                        }
                    }
                }
            }
        }
    }
}
