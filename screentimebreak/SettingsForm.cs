using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace screentimebreak {
    public partial class SettingsForm : Form {

        // Main buttons
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
        private SettingsLabel breaksBeforeMegaBreakLabel;
        private SettingsLabel megaBreaksLabel;
        private SettingsLabel megaBreakMinutesLabel;
        private SettingsLabel megaBreakSecondsLabel;
        private SettingsLabel showScreenTimerLabel;
        private SettingsLabel screenTimerColorLabel;
        private Label selectedScreenTimeColorLabel;

        // Value fields + other controls
        private SettingsField screenTimeMinutesInput;
        private SettingsField screenTimeSecondsInput;
        private SettingsField breakMinutesInput;
        private SettingsField breakSecondsInput;
        private CheckBox megaBreaksCheckbox;
        private SettingsField breaksBeforeMegaBreakInput;
        private SettingsField megaBreakMinutesInput;
        private SettingsField megaBreakSecondsInput;
        private CheckBox showScreenTimerCheckBox;
        private ColorDialog screenTimerColorPicker;
        private Button screenTimerColorPickerButton;
        // Tabs + TabControl
        private TabPage timersTabPage;
        private TabPage interfaceTabPage;
        private TabControl tabControl;

        public SettingsForm() {
            Size = new Size(360, 345);
            Visible = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Text = "Settings";
            MaximizeBox = false;
            addComponents();
        }

        private void addComponents() {
            // Main buttons
            okButton = new Button();
            okButton.Text = "OK";
            okButton.Location = new Point(15, 275);
            okButton.Click += new EventHandler(okButton_Click);

            cancelButton = new Button();
            cancelButton.Text = "Cancel";
            cancelButton.Location = new Point(175, 275);
            cancelButton.Click += new EventHandler(cancelButton_Click);

            applyButton = new Button();
            applyButton.Text = "Apply";
            applyButton.Location = new Point(95, 275);
            applyButton.Click += new EventHandler(applyButton_Click);

            // Screen time
            screenTimeLabel = new SettingsLabel(new Point(0, 20), "Screen time");
            screenTimeMinutesInput = new SettingsField(FieldType.TimeField, new Point(75, 20));
            screenTimeMinutesInput.Text = Properties.Settings.Default.ScreenTimeMinutes.ToString();

            screenTimeSecondsInput = new SettingsField(FieldType.TimeField, new Point(75, 50));
            screenTimeSecondsInput.Text = Properties.Settings.Default.ScreenTimeSeconds.ToString();

            screenTimeMinutesLabel = new SettingsLabel(new Point(105, 20), "minutes");
            screenTimeSecondsLabel = new SettingsLabel(new Point(105, 50), "seconds");

            // Breaks
            breakTimeLabel = new SettingsLabel(new Point(175, 20), "Break time");
            breakMinutesInput = new SettingsField(FieldType.TimeField, new Point(250, 20));
            breakMinutesInput.Text = Properties.Settings.Default.BreakTimeMinutes.ToString();

            breakSecondsInput = new SettingsField(FieldType.TimeField, new Point(250, 50));
            breakSecondsInput.Text = Properties.Settings.Default.BreakTimeSeconds.ToString();

            breakMinutesLabel = new SettingsLabel(new Point(280, 20), "minutes");
            breakSecondsLabel = new SettingsLabel(new Point(280, 50), "seconds");

            // Megabreaks
            megaBreaksEnableLabel = new SettingsLabel(new Point(0, 100), "Enable megabreaks");

            megaBreaksCheckbox = new CheckBox();
            megaBreaksCheckbox.Location = new Point(120, 100);
            megaBreaksCheckbox.Checked = Properties.Settings.Default.MegaBreaksEnabled;
            megaBreaksCheckbox.Click += new EventHandler(megaBreaksCheckbox_Click);

            breaksBeforeMegaBreakLabel = new SettingsLabel(new Point(0, 130), "Breaks before megabreak");

            breaksBeforeMegaBreakInput = new SettingsField(FieldType.IntegerNumberField, new Point(150, 130));
            breaksBeforeMegaBreakInput.Text = Properties.Settings.Default.BreaksBeforeMegaBreak.ToString();

            megaBreaksLabel = new SettingsLabel(new Point(0, 160), "Megabreaks");
            megaBreakMinutesInput = new SettingsField(FieldType.TimeField, new Point(75, 160));
            megaBreakMinutesInput.Text = Properties.Settings.Default.MegaBreakTimeMinutes.ToString();

            megaBreakSecondsInput = new SettingsField(FieldType.TimeField, new Point(75, 190));
            megaBreakSecondsInput.Text = Properties.Settings.Default.MegaBreakTimeSeconds.ToString();

            megaBreakMinutesLabel = new SettingsLabel(new Point(105, 160), "minutes");
            megaBreakSecondsLabel = new SettingsLabel(new Point(105, 190), "seconds");

            // Fields toggled once here to prevent editing when megabreaks are disabled (when checkbox hasn't been clicked yet)
            toggleMegaBreakFields();

            // Interface options
            showScreenTimerLabel = new SettingsLabel(new Point(0, 20), "Show screen timer");
            showScreenTimerCheckBox = new CheckBox();
            showScreenTimerCheckBox.Location = new Point(125, 20);
            showScreenTimerCheckBox.Checked = Properties.Settings.Default.ShowScreenTimer;

            screenTimerColorLabel = new SettingsLabel(new Point(0, 50), "Screen timer color");
            screenTimerColorPicker = new ColorDialog();
            screenTimerColorPicker.AnyColor = true;

            screenTimerColorPickerButton = new Button();
            screenTimerColorPickerButton.Text = "Select color...";
            screenTimerColorPickerButton.Size = new Size(100, 25);
            screenTimerColorPickerButton.Location = new Point(150, 50);
            screenTimerColorPickerButton.Click += new EventHandler(screenTimerColorPickerButton_Click);

            // Normal label, due to SettingsLabel having AutoSize set to true
            selectedScreenTimeColorLabel = new Label();
            selectedScreenTimeColorLabel.Location = new Point(125, 50);
            selectedScreenTimeColorLabel.Size = new Size(15, 15);
            selectedScreenTimeColorLabel.BorderStyle = BorderStyle.Fixed3D;
            selectedScreenTimeColorLabel.BackColor = Properties.Settings.Default.ScreenTimerColor;

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
            timersTabPage.Controls.Add(breaksBeforeMegaBreakLabel);
            timersTabPage.Controls.Add(breaksBeforeMegaBreakInput);
            timersTabPage.Controls.Add(megaBreakMinutesLabel);
            timersTabPage.Controls.Add(megaBreakMinutesInput);
            timersTabPage.Controls.Add(megaBreakSecondsLabel);
            timersTabPage.Controls.Add(megaBreakSecondsInput);

            // Tab 2 (Interface)
            interfaceTabPage = new TabPage();
            interfaceTabPage.Text = "Interface";
            interfaceTabPage.TabIndex = 1;

            interfaceTabPage.Controls.Add(showScreenTimerLabel);
            interfaceTabPage.Controls.Add(showScreenTimerCheckBox);
            interfaceTabPage.Controls.Add(screenTimerColorLabel);
            interfaceTabPage.Controls.Add(screenTimerColorPickerButton);
            interfaceTabPage.Controls.Add(selectedScreenTimeColorLabel);

            // Tab control
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

        private void megaBreaksCheckbox_Click(object sender, EventArgs e) {
            toggleMegaBreakFields();
        }

        private void screenTimerColorPickerButton_Click(object sender, EventArgs e) {
            openScreenTimerColorPicker();
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
            Properties.Settings.Default.BreaksBeforeMegaBreak = Convert.ToInt32(breaksBeforeMegaBreakInput.Text);
            Properties.Settings.Default.ShowScreenTimer = showScreenTimerCheckBox.Checked;
            Properties.Settings.Default.ScreenTimerColor = selectedScreenTimeColorLabel.BackColor;
            Properties.Settings.Default.Save();
            if (TimerOverlayForm.getScreenMode()) {
                TimerOverlayForm.getScreenTimeLabel().Visible = Properties.Settings.Default.ShowScreenTimer;
            }
            TimerOverlayForm.getScreenTimeLabel().ForeColor = Properties.Settings.Default.ScreenTimerColor;
        }

        private void toggleMegaBreakFields() {
            if (!megaBreaksCheckbox.Checked) {
                megaBreakMinutesInput.Enabled = false;
                megaBreakSecondsInput.Enabled = false;
                breaksBeforeMegaBreakInput.Enabled = false;
            }
            else {
                megaBreakMinutesInput.Enabled = true;
                megaBreakSecondsInput.Enabled = true;
                breaksBeforeMegaBreakInput.Enabled = true;
            }
        }

        private void openScreenTimerColorPicker() {
            screenTimerColorPicker.ShowDialog();
            selectedScreenTimeColorLabel.BackColor = screenTimerColorPicker.Color;
        }
    }
}
