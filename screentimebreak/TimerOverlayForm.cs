using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace screentimebreak {
    public partial class TimerOverlayForm : Form {
        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        private int screenTimeMinutes;
        private int screenTimeSeconds;
        private int breakTimeMinutes;
        private int breakTimeSeconds;
        private int megaBreakTimeMinutes;
        private int megaBreakTimeSeconds;
        private int breakCount = 0;
        private int breaksBeforeMega = 5;
        private bool megaBreaksEnabled;
        private static Label screenTimeLabel;
        private Label breakTimeLabel;

        private TrayMenu trayMenu;


        public TimerOverlayForm() {

            this.screenTimeMinutes = Properties.Settings.Default.ScreenTimeMinutes;
            this.screenTimeSeconds = Properties.Settings.Default.ScreenTimeSeconds;
            this.breakTimeMinutes = Properties.Settings.Default.BreakTimeMinutes;
            this.breakTimeSeconds = Properties.Settings.Default.BreakTimeSeconds;
            this.megaBreakTimeMinutes = Properties.Settings.Default.MegaBreakTimeMinutes;
            this.megaBreakTimeSeconds = Properties.Settings.Default.MegaBreakTimeSeconds;
            this.megaBreaksEnabled = Properties.Settings.Default.MegaBreaksEnabled;

            // Labels
            screenTimeLabel = new CountdownLabel(15);
            screenTimeLabel.Location = new Point(1600, 5);
            screenTimeLabel.Visible = Properties.Settings.Default.ShowScreenTimer;

            breakTimeLabel = new CountdownLabel(50);
            breakTimeLabel.Location = new Point(500, 500);
            breakTimeLabel.Visible = false;

            Controls.Add(screenTimeLabel);
            Controls.Add(breakTimeLabel);

            InitializeComponent();

            // Visual properties
            FormBorderStyle = FormBorderStyle.None;
            Opacity = 0.70;
            BackColor = Color.Black;
            TransparencyKey = BackColor;
            TopMost = true;

            setSizeToAllScreens();

            // Enable clicking through the form
            int initialStyle = GetWindowLong(this.Handle, -20);
            SetWindowLong(this.Handle, -20, initialStyle | 0x80000 | 0x20);

            // Call function TimerOverlayForm_Load on form load
            Load += new EventHandler(TimerOverlayForm_Load);

        }

        // Set the form's size to cover all screens
        // Does not work properly if form was launched from non-main screen
        // This function could probably be named better
        private void setSizeToAllScreens() {
            Rectangle r = new Rectangle();

            foreach (Screen s in Screen.AllScreens) {
                r = Rectangle.Union(r, s.Bounds);
            }

            Top = r.Top;
            Left = r.Left;
            Width = r.Width;
            Height = r.Height;
        }

        // Do things when the form is loaded
        private void TimerOverlayForm_Load(object sender, EventArgs e) {

            // Maybe put these in their own class?
            trayMenu = new TrayMenu(this);
            startMainLoop();
        }

        // Start the main timer loop
        private void startMainLoop() {

            int screenTime = (screenTimeMinutes * 60) + screenTimeSeconds;
            int breakTime = (breakTimeMinutes * 60) + breakTimeSeconds;
            int megaBreakTime = (megaBreakTimeMinutes * 60) + megaBreakTimeSeconds;

            // Total countdown time left (seconds)
            int totalCountdownTimeLeft = screenTime;

            int countdownMinutesLeft;
            int countdownSecondsLeft;

            bool screenMode = true;
            bool megaBreak = false;

            Timer countdownTimer = new Timer();
            countdownTimer.Interval = 1000;
            countdownTimer.Enabled = true;

            countdownTimer.Tick += new EventHandler(countdownTimer_Tick);

            void countdownTimer_Tick(object sender, EventArgs e) {

                if (totalCountdownTimeLeft <= 0) {
                    if (screenMode) {
                        enterBreakMode();
                    }
                    else {
                        enterScreenMode();
                    }
                }

                countdownMinutesLeft = totalCountdownTimeLeft / 60;
                countdownSecondsLeft = totalCountdownTimeLeft - (60 * countdownMinutesLeft);

                if (screenMode) {
                    screenTimeLabel.Text = countdownMinutesLeft.ToString("00") + ":" + countdownSecondsLeft.ToString("00");
                    screenTimeLabel.Refresh();
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

            void enterScreenMode() {
                screenMode = true;
                fadeOutOfBlack();
                breakTimeLabel.Visible = false;
                totalCountdownTimeLeft = screenTime;
            }

            void enterBreakMode() {
                if (breakCount == breaksBeforeMega) {
                    megaBreak = true;
                }
                else {
                    megaBreak = false;
                }
                
                screenMode = false;
                fadeIntoBlack();
                screenTimeLabel.Visible = false;
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

            void fadeIntoBlack() {
                double originalOpacity = Opacity;
                Opacity = 0.00;
                Timer fadeInTimer = new Timer();
                fadeInTimer.Interval = 10;
                fadeInTimer.Enabled = true;

                fadeInTimer.Tick += new EventHandler(fadeInTimer_Tick);

                TransparencyKey = Color.Magenta;

                void fadeInTimer_Tick(object sender, EventArgs e) {
                    if (Opacity < originalOpacity) {
                        Opacity += 0.02;
                    }
                    else {
                        fadeInTimer.Enabled = false;
                    }
                }
            }

            void fadeOutOfBlack() {
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
                        Opacity = 0.70;
                        fadeOutTimer.Enabled = false;
                        screenTimeLabel.Visible = true;
                    }
                }
            }
        }
        public static Label getScreenTimeLabel() {
            return screenTimeLabel;
        }
    }
}
