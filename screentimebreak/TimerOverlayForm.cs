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
        private Label screenTimeLabel;
        private Label breakTimeLabel;

        public TimerOverlayForm(int screenTimeMinutes = 0, int screenTimeSeconds = 5, int breakTimeMinutes = 0, int breakTimeSeconds = 8) {

            this.screenTimeMinutes = screenTimeMinutes;
            this.screenTimeSeconds = screenTimeSeconds;
            this.breakTimeMinutes = breakTimeMinutes;
            this.breakTimeSeconds = breakTimeSeconds;

            // Labels
            screenTimeLabel = new CountdownLabel(30);
            screenTimeLabel.Location = new Point(500, 500);
            screenTimeLabel.Visible = true;

            breakTimeLabel = new CountdownLabel(50);
            breakTimeLabel.Location = new Point(1000, 500);
            breakTimeLabel.Visible = false;

            this.Controls.Add(screenTimeLabel);
            this.Controls.Add(breakTimeLabel);

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
            Load += new EventHandler(this.TimerOverlayForm_Load);

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
            startMainLoop();
        }

        // Start the main timer loop
        private void startMainLoop() {

            int screenTime = (screenTimeMinutes * 60) + screenTimeSeconds;
            int breakTime = (breakTimeMinutes * 60) + breakTimeSeconds;


            int countdownSecondsLeft = screenTime;
            bool screenMode = true;

            Timer countdownTimer = new Timer();
            countdownTimer.Interval = 1000;
            countdownTimer.Enabled = true;

            countdownTimer.Tick += new EventHandler(countdownTimer_Tick);

            void countdownTimer_Tick(object sender, EventArgs e) {

                if (countdownSecondsLeft < 0) {
                    if (screenMode) {
                        enterBreakMode();
                    }
                    else {
                        enterScreenMode();
                    }
                }

                if (screenMode) {
                    screenTimeLabel.Text = countdownSecondsLeft.ToString();
                    screenTimeLabel.Refresh();
                }
                else {
                    breakTimeLabel.Text = countdownSecondsLeft.ToString();
                    breakTimeLabel.Refresh();
                }

                countdownSecondsLeft--;

            }

            void enterScreenMode() {
                countdownTimer.Stop();
                screenMode = true;
                TransparencyKey = BackColor;
                breakTimeLabel.Visible = false;
                screenTimeLabel.Visible = true;
                countdownSecondsLeft = screenTime;
                countdownTimer.Start();
            }

            void enterBreakMode() {
                countdownTimer.Stop();
                screenMode = false;
                TransparencyKey = Color.Magenta;
                screenTimeLabel.Visible = false;
                breakTimeLabel.Visible = true;
                countdownSecondsLeft = breakTime;
                countdownTimer.Start();
            }

        }

    }
}
