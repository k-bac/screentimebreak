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

        public TimerOverlayForm(int screenTimeMinutes = 0, int screenTimeSeconds = 5, int breakTimeMinutes = 2, int breakTimeSeconds = 30) {

            this.screenTimeMinutes = screenTimeMinutes;
            this.screenTimeSeconds = screenTimeSeconds;
            this.breakTimeMinutes = breakTimeMinutes;
            this.breakTimeSeconds = breakTimeSeconds;

            screenTimeLabel = new Label();
            screenTimeLabel.Location = new Point(500, 500);
            screenTimeLabel.ForeColor = Color.White;
            screenTimeLabel.Font = new Font("Calibri", 40);
            screenTimeLabel.AutoSize = true;
            screenTimeLabel.Visible = true;


            breakTimeLabel = new Label();
            breakTimeLabel.Location = new Point(1000, 500);
            breakTimeLabel.ForeColor = Color.White;
            breakTimeLabel.Font = new Font("Calibri", 40);
            breakTimeLabel.AutoSize = true;
            breakTimeLabel.Visible = false;


            this.Controls.Add(screenTimeLabel);
            this.Controls.Add(breakTimeLabel);

            InitializeComponent();
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

            int screenTime = (this.screenTimeMinutes * 60) + this.screenTimeSeconds;
            int decrementingScreenTime = screenTime;

            int breakTime = (this.breakTimeMinutes * 60) + this.breakTimeSeconds;
            int decrementingBreakTime = breakTime;

            Timer screenTimer = new Timer();
            screenTimer.Interval = 1000;
            screenTimer.Enabled = true;

            Timer breakTimer = new Timer();
            breakTimer.Interval = 1000;
            breakTimer.Enabled = false;

            screenTimer.Tick += new EventHandler(screenTimer_Tick);
            breakTimer.Tick += new EventHandler(breakTimer_Tick);


            void screenTimer_Tick(object sender, EventArgs e) {
                if (decrementingScreenTime == 0) {
                    enterBreakMode();
                }
                screenTimeLabel.Text = decrementingScreenTime.ToString();
                screenTimeLabel.Refresh();
                decrementingScreenTime--;
            }

            void breakTimer_Tick(object sender, EventArgs e) {
                if (decrementingBreakTime == 0) {
                    enterScreenMode();
                }
                breakTimeLabel.Text = decrementingBreakTime.ToString();
                breakTimeLabel.Refresh();
                decrementingBreakTime--;
            }

            void enterScreenMode() {
                breakTimer.Stop();
                TransparencyKey = BackColor;
                breakTimeLabel.Visible = false;
                screenTimeLabel.Visible = true;
                decrementingBreakTime = breakTime;
                screenTimer.Start();
            }

            void enterBreakMode() {
                screenTimer.Stop();
                TransparencyKey = Color.Magenta;
                screenTimeLabel.Visible = false;
                breakTimeLabel.Visible = true;
                decrementingScreenTime = screenTime;
                breakTimer.Start();
            }

        }

    }
}
