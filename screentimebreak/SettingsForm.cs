using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace screentimebreak {
    public partial class SettingsForm : Form {

        // Buttons
        private Button okButton;
        private Button applyButton;
        private Button cancelButton;


        // Text labels
        private SettingsLabel screenTime;
        private SettingsLabel screenTimeSeconds;
        private SettingsLabel screenTimeMinutes;
        private SettingsLabel breakTime;
        private SettingsLabel breakSeconds;
        private SettingsLabel breakMinutes;
        private SettingsLabel megaBreaks;

        // Value fields
        private SettingsTimeInputField screenTimeMinutesInput;
        private SettingsTimeInputField screenTimeSecondsInput;
        private SettingsTimeInputField breakMinutesInput;
        private SettingsTimeInputField breakSecondsInput;
        private CheckBox megaBreaksCheckbox;

        // Tabs + TabControl
        private TabPage tabpage1;
        private TabPage tabpage2;
        private TabControl tabControl;

        public SettingsForm() {
            Size = new Size(360, 400);
            Visible = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Text = "Settings";

            addComponents();
        }

        private void addComponents() {
            // Buttons
            okButton = new Button();
            okButton.Text = "OK";
            okButton.Location = new Point(15, 330);
            okButton.Click += new EventHandler(okButton_Click);

            cancelButton = new Button();
            cancelButton.Text = "Cancel";
            cancelButton.Location = new Point(175, 330);
            cancelButton.Click += new EventHandler(cancelButton_Click);

            applyButton = new Button();
            applyButton.Text = "Apply";
            applyButton.Location = new Point(95, 330);
            applyButton.Click += new EventHandler(applyButton_Click);

            // Screen time

            screenTime = new SettingsLabel(new Point(0, 20), "Screen time");
            screenTimeMinutesInput = new SettingsTimeInputField(new Point(75, 20));
            screenTimeMinutesInput.Text = Properties.Settings.Default.ScreenTimeMinutes.ToString();

            screenTimeSecondsInput = new SettingsTimeInputField(new Point(75, 50));
            screenTimeSecondsInput.Text = Properties.Settings.Default.ScreenTimeSeconds.ToString();

            screenTimeMinutes = new SettingsLabel(new Point(105, 20), "minutes");
            screenTimeSeconds = new SettingsLabel(new Point(105, 50), "seconds");

            // Breaks
            breakTime = new SettingsLabel(new Point(175, 20), "Break time");

            breakMinutesInput = new SettingsTimeInputField(new Point(250, 20));
            breakMinutesInput.Text = Properties.Settings.Default.BreakTimeMinutes.ToString();

            breakSecondsInput = new SettingsTimeInputField(new Point(250, 50));
            breakSecondsInput.Text = Properties.Settings.Default.BreakTimeSeconds.ToString();

            breakMinutes = new SettingsLabel(new Point(280, 20), "minutes");

            breakSeconds = new SettingsLabel(new Point(280, 50), "seconds");

            megaBreaks = new SettingsLabel(new Point(0, 100), "Enable megabreaks");

            megaBreaksCheckbox = new CheckBox();
            megaBreaksCheckbox.Location = new Point(120, 100);
            megaBreaksCheckbox.Checked = Properties.Settings.Default.MegaBreaksEnabled;

            // Tab 1 (Timers)
            tabpage1 = new TabPage();
            tabpage1.Text = "Timers";
            tabpage1.TabIndex = 0;

            tabpage1.Controls.Add(screenTime);
            tabpage1.Controls.Add(screenTimeMinutes);
            tabpage1.Controls.Add(screenTimeMinutesInput);
            tabpage1.Controls.Add(screenTimeSeconds);
            tabpage1.Controls.Add(screenTimeSecondsInput);

            tabpage1.Controls.Add(breakTime);

            tabpage1.Controls.Add(breakMinutes);
            tabpage1.Controls.Add(breakMinutesInput);

            tabpage1.Controls.Add(breakSeconds);
            tabpage1.Controls.Add(breakSecondsInput);

            tabpage1.Controls.Add(megaBreaks);
            tabpage1.Controls.Add(megaBreaksCheckbox);

            // Tab 2 (Interface)
            tabpage2 = new TabPage();
            tabpage2.Text = "Interface";
            tabpage2.TabIndex = 1;

            tabControl = new TabControl();
            //tabControl.ItemSize = new System.Drawing.Size(400, 200);

            tabControl.Controls.Add(tabpage1);
            tabControl.Controls.Add(tabpage2);

            tabControl.Dock = DockStyle.Fill;

            Controls.Add(okButton);
            Controls.Add(applyButton);
            Controls.Add(cancelButton);
            Controls.Add(tabControl);
        }

        private void okButton_Click(object sender, EventArgs e) {
            applySettings();
            Close();
        }

        private void applyButton_Click(object sender, EventArgs e) {
            applySettings();
        }

        private void cancelButton_Click(object sender, EventArgs e) {
            Close();
        }

        private void applySettings() {
            Properties.Settings.Default.ScreenTimeMinutes = Convert.ToInt32(screenTimeMinutesInput.Text);
            Properties.Settings.Default.ScreenTimeSeconds = Convert.ToInt32(screenTimeSecondsInput.Text);
            Properties.Settings.Default.BreakTimeMinutes = Convert.ToInt32(breakMinutesInput.Text);
            Properties.Settings.Default.BreakTimeSeconds = Convert.ToInt32(breakSecondsInput.Text);
            Properties.Settings.Default.MegaBreaksEnabled = megaBreaksCheckbox.Checked;
            Properties.Settings.Default.Save();
        }
    }
}
