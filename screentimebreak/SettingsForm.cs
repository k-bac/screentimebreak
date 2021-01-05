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
        private SettingsLabel screenTimeLabel;
        private SettingsLabel screenTimeSecondsLabel;
        private SettingsLabel screenTimeMinutesLabel;
        private SettingsLabel breakTimeLabel;
        private SettingsLabel breakSecondsLabel;
        private SettingsLabel breakMinutesLabel;
        private SettingsLabel megaBreaksEnableLabel;
        private SettingsLabel megaBreaksLabel;
        private SettingsLabel megaBreakMinutesLabel;
        private SettingsLabel megaBreakSecondsLabel;

        // Value fields
        private SettingsTimeInputField screenTimeMinutesInput;
        private SettingsTimeInputField screenTimeSecondsInput;
        private SettingsTimeInputField breakMinutesInput;
        private SettingsTimeInputField breakSecondsInput;
        private CheckBox megaBreaksCheckbox;
        private SettingsTimeInputField megaBreakMinutesInput;
        private SettingsTimeInputField megaBreakSecondsInput;

        // Tabs + TabControl
        private TabPage timersTabPage;
        private TabPage interfaceTabPage;
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
            screenTimeLabel = new SettingsLabel(new Point(0, 20), "Screen time");
            screenTimeMinutesInput = new SettingsTimeInputField(new Point(75, 20));
            screenTimeMinutesInput.Text = Properties.Settings.Default.ScreenTimeMinutes.ToString();

            screenTimeSecondsInput = new SettingsTimeInputField(new Point(75, 50));
            screenTimeSecondsInput.Text = Properties.Settings.Default.ScreenTimeSeconds.ToString();

            screenTimeMinutesLabel = new SettingsLabel(new Point(105, 20), "minutes");
            screenTimeSecondsLabel = new SettingsLabel(new Point(105, 50), "seconds");

            // Breaks
            breakTimeLabel = new SettingsLabel(new Point(175, 20), "Break time");
            breakMinutesInput = new SettingsTimeInputField(new Point(250, 20));
            breakMinutesInput.Text = Properties.Settings.Default.BreakTimeMinutes.ToString();

            breakSecondsInput = new SettingsTimeInputField(new Point(250, 50));
            breakSecondsInput.Text = Properties.Settings.Default.BreakTimeSeconds.ToString();

            breakMinutesLabel = new SettingsLabel(new Point(280, 20), "minutes");
            breakSecondsLabel = new SettingsLabel(new Point(280, 50), "seconds");

            // Megabreaks
            megaBreaksEnableLabel = new SettingsLabel(new Point(0, 100), "Enable megabreaks");
            megaBreaksLabel = new SettingsLabel(new Point(0, 150), "Megabreaks");
            megaBreakMinutesInput = new SettingsTimeInputField(new Point(75, 150));
            megaBreakMinutesInput.Text = Properties.Settings.Default.MegaBreakTimeMinutes.ToString();

            megaBreakSecondsInput = new SettingsTimeInputField(new Point(75, 180));
            megaBreakSecondsInput.Text = Properties.Settings.Default.MegaBreakTimeSeconds.ToString();

            megaBreakMinutesLabel = new SettingsLabel(new Point(105, 150), "minutes");
            megaBreakSecondsLabel = new SettingsLabel(new Point(105, 185), "seconds");

            megaBreaksCheckbox = new CheckBox();
            megaBreaksCheckbox.Location = new Point(120, 100);
            megaBreaksCheckbox.Checked = Properties.Settings.Default.MegaBreaksEnabled;

            // Tab 1 (Timers)
            timersTabPage = new TabPage();
            timersTabPage.Text = "Timers";
            timersTabPage.TabIndex = 0;

            timersTabPage.Controls.Add(screenTimeLabel);
            timersTabPage.Controls.Add(screenTimeMinutesLabel);
            timersTabPage.Controls.Add(screenTimeMinutesInput);
            timersTabPage.Controls.Add(screenTimeSecondsLabel);
            timersTabPage.Controls.Add(screenTimeSecondsInput);

            timersTabPage.Controls.Add(breakTimeLabel);
            timersTabPage.Controls.Add(breakMinutesLabel);
            timersTabPage.Controls.Add(breakMinutesInput);
            timersTabPage.Controls.Add(breakSecondsLabel);
            timersTabPage.Controls.Add(breakSecondsInput);

            timersTabPage.Controls.Add(megaBreaksEnableLabel);
            timersTabPage.Controls.Add(megaBreaksLabel);
            timersTabPage.Controls.Add(megaBreaksCheckbox);
            timersTabPage.Controls.Add(megaBreakMinutesLabel);
            timersTabPage.Controls.Add(megaBreakMinutesInput);
            timersTabPage.Controls.Add(megaBreakSecondsLabel);
            timersTabPage.Controls.Add(megaBreakSecondsInput);

            // Tab 2 (Interface)
            interfaceTabPage = new TabPage();
            interfaceTabPage.Text = "Interface";
            interfaceTabPage.TabIndex = 1;

            tabControl = new TabControl();

            tabControl.Controls.Add(timersTabPage);
            tabControl.Controls.Add(interfaceTabPage);

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
            // TODO: Add error handling + prompt when invalid values are entered
            Properties.Settings.Default.ScreenTimeMinutes = Convert.ToInt32(screenTimeMinutesInput.Text);
            Properties.Settings.Default.ScreenTimeSeconds = Convert.ToInt32(screenTimeSecondsInput.Text);
            Properties.Settings.Default.BreakTimeMinutes = Convert.ToInt32(breakMinutesInput.Text);
            Properties.Settings.Default.BreakTimeSeconds = Convert.ToInt32(breakSecondsInput.Text);
            Properties.Settings.Default.MegaBreakTimeMinutes = Convert.ToInt32(megaBreakMinutesInput.Text);
            Properties.Settings.Default.MegaBreakTimeSeconds = Convert.ToInt32(megaBreakSecondsInput.Text);
            Properties.Settings.Default.MegaBreaksEnabled = megaBreaksCheckbox.Checked;
            Properties.Settings.Default.Save();
        }
    }
}
