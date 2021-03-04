using System;
using System.Drawing;
using System.Windows.Forms;

namespace screentimebreak {
    public partial class SettingsForm : Form {

        private TimerOverlayForm timerOverlayForm;

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

        public SettingsForm(TimerOverlayForm timerOverlayForm) {
            this.timerOverlayForm = timerOverlayForm;
            InitComponents();
        }

        private void InitComponents() {
            Size = new Size(360, 345);
            Visible = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Text = "Settings";
            MaximizeBox = false;

            // Main buttons
            Button okButton = new Button();
            okButton.Text = "OK";
            okButton.Location = new Point(15, 275);
            okButton.Click += new EventHandler(okButton_Click);

            Button cancelButton = new Button();
            cancelButton.Text = "Cancel";
            cancelButton.Location = new Point(175, 275);
            cancelButton.Click += new EventHandler(cancelButton_Click);

            Button applyButton = new Button();
            applyButton.Text = "Apply";
            applyButton.Location = new Point(95, 275);
            applyButton.Click += new EventHandler(applyButton_Click);

            // Screen time
            SettingsLabel screenTimeLabel = new SettingsLabel(new Point(0, 20), "Screen time");
            screenTimeMinutesInput = new SettingsField(FieldType.TimeField, new Point(75, 20));
            screenTimeMinutesInput.Text = Properties.Settings.Default.ScreenTimeMinutes.ToString();

            screenTimeSecondsInput = new SettingsField(FieldType.TimeField, new Point(75, 50));
            screenTimeSecondsInput.Text = Properties.Settings.Default.ScreenTimeSeconds.ToString();

            SettingsLabel screenTimeMinutesLabel = new SettingsLabel(new Point(105, 20), "minutes");
            SettingsLabel screenTimeSecondsLabel = new SettingsLabel(new Point(105, 50), "seconds");

            // Breaks
            SettingsLabel breakTimeLabel = new SettingsLabel(new Point(175, 20), "Break time");
            breakMinutesInput = new SettingsField(FieldType.TimeField, new Point(250, 20));
            breakMinutesInput.Text = Properties.Settings.Default.BreakTimeMinutes.ToString();

            breakSecondsInput = new SettingsField(FieldType.TimeField, new Point(250, 50));
            breakSecondsInput.Text = Properties.Settings.Default.BreakTimeSeconds.ToString();

            SettingsLabel breakMinutesLabel = new SettingsLabel(new Point(280, 20), "minutes");
            SettingsLabel breakSecondsLabel = new SettingsLabel(new Point(280, 50), "seconds");

            // Megabreaks
            SettingsLabel megaBreaksEnableLabel = new SettingsLabel(new Point(0, 100), "Enable megabreaks");

            megaBreaksCheckbox = new CheckBox();
            megaBreaksCheckbox.Location = new Point(120, 100);
            megaBreaksCheckbox.Checked = Properties.Settings.Default.MegaBreaksEnabled;
            megaBreaksCheckbox.Click += new EventHandler(megaBreaksCheckbox_Click);

            SettingsLabel breaksBeforeMegaBreakLabel = new SettingsLabel(new Point(0, 130), "Breaks before megabreak");

            breaksBeforeMegaBreakInput = new SettingsField(FieldType.IntegerNumberField, new Point(150, 130));
            breaksBeforeMegaBreakInput.Text = Properties.Settings.Default.BreaksBeforeMegaBreak.ToString();

            SettingsLabel megaBreaksLabel = new SettingsLabel(new Point(0, 160), "Megabreaks");
            megaBreakMinutesInput = new SettingsField(FieldType.TimeField, new Point(75, 160));
            megaBreakMinutesInput.Text = Properties.Settings.Default.MegaBreakTimeMinutes.ToString();

            megaBreakSecondsInput = new SettingsField(FieldType.TimeField, new Point(75, 190));
            megaBreakSecondsInput.Text = Properties.Settings.Default.MegaBreakTimeSeconds.ToString();

            SettingsLabel megaBreakMinutesLabel = new SettingsLabel(new Point(105, 160), "minutes");
            SettingsLabel megaBreakSecondsLabel = new SettingsLabel(new Point(105, 190), "seconds");

            // Fields toggled once here to prevent editing when megabreaks are disabled (when checkbox hasn't been clicked yet)
            ToggleMegaBreakFields();

            // Interface options
            SettingsLabel showScreenTimerLabel = new SettingsLabel(new Point(0, 20), "Show screen timer");
            showScreenTimerCheckBox = new CheckBox();
            showScreenTimerCheckBox.Location = new Point(125, 20);
            showScreenTimerCheckBox.Checked = Properties.Settings.Default.ShowScreenTimer;

            SettingsLabel screenTimerColorLabel = new SettingsLabel(new Point(0, 50), "Screen timer color");
            screenTimerColorPicker = new ColorDialog();
            screenTimerColorPicker.AnyColor = true;

            Button screenTimerColorPickerButton = new Button();
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
            TabPage timersTabPage = new TabPage();
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
            TabPage interfaceTabPage = new TabPage();
            interfaceTabPage.Text = "Interface";
            interfaceTabPage.TabIndex = 1;

            interfaceTabPage.Controls.Add(showScreenTimerLabel);
            interfaceTabPage.Controls.Add(showScreenTimerCheckBox);
            interfaceTabPage.Controls.Add(screenTimerColorLabel);
            interfaceTabPage.Controls.Add(screenTimerColorPickerButton);
            interfaceTabPage.Controls.Add(selectedScreenTimeColorLabel);

            // Tab control
            TabControl tabControl = new TabControl();

            tabControl.Controls.Add(timersTabPage);
            tabControl.Controls.Add(interfaceTabPage);

            tabControl.Dock = DockStyle.Fill;

            Controls.Add(okButton);
            Controls.Add(applyButton);
            Controls.Add(cancelButton);
            Controls.Add(tabControl);
        }

        private void okButton_Click(object sender, EventArgs e) {
            if (ApplySettings() == true) {
                Close();
            } 
        }

        private void applyButton_Click(object sender, EventArgs e) {
            ApplySettings();
        }

        private void cancelButton_Click(object sender, EventArgs e) {
            Close();
        }

        private void megaBreaksCheckbox_Click(object sender, EventArgs e) {
            ToggleMegaBreakFields();
        }

        private void screenTimerColorPickerButton_Click(object sender, EventArgs e) {
            OpenScreenTimerColorPicker();
        }

        // Save the settings to the configuration file
        // Return bool: settings validation success
        private bool ApplySettings() {
            try {
                int screenTimeMinutes = Convert.ToInt32(screenTimeMinutesInput.Text);
                int screenTimeSeconds = Convert.ToInt32(screenTimeSecondsInput.Text);
                int breakTimeMinutes = Convert.ToInt32(breakMinutesInput.Text);
                int breakTimeSeconds = Convert.ToInt32(breakSecondsInput.Text);
                int megabreakMinutes = Convert.ToInt32(megaBreakMinutesInput.Text);
                int megabreakSeconds = Convert.ToInt32(megaBreakSecondsInput.Text);
                int breaksBeforeMega = Convert.ToInt32(breaksBeforeMegaBreakInput.Text);
                int[][] timeValueArrays = {
                    new int[] { screenTimeSeconds, screenTimeMinutes },
                    new int[] { breakTimeMinutes, breakTimeSeconds }, 
                    new int[] { megabreakMinutes, megabreakSeconds }
                };
                foreach (int[] timeValueArray in timeValueArrays) {
                    int timeValueSum = 0;
                    foreach (int timeValue in timeValueArray) {
                        timeValueSum += timeValue;
                        if (timeValue < 0 || timeValue > 59) {
                            throw new InvalidSettingValueException("Minutes and seconds must be values between 0 and 59.");
                        }
                    }
                    if (timeValueSum == 0) {
                        throw new InvalidSettingValueException("Both minutes and seconds cannot be 0.");
                    }
                }
                if (breaksBeforeMega <= 1 || breaksBeforeMega > 10) {
                    throw new InvalidSettingValueException("Breaks between megabreaks must be a value between 1 and 10.");
                }
                Properties.Settings.Default.ScreenTimeMinutes = screenTimeMinutes;
                Properties.Settings.Default.ScreenTimeSeconds = screenTimeSeconds;
                Properties.Settings.Default.BreakTimeMinutes = breakTimeMinutes;
                Properties.Settings.Default.BreakTimeSeconds = breakTimeSeconds;
                Properties.Settings.Default.MegaBreakTimeMinutes = megabreakMinutes;
                Properties.Settings.Default.MegaBreakTimeSeconds = megabreakSeconds;
                Properties.Settings.Default.BreaksBeforeMegaBreak = breaksBeforeMega;
                Properties.Settings.Default.MegaBreaksEnabled = megaBreaksCheckbox.Checked;
                Properties.Settings.Default.ShowScreenTimer = showScreenTimerCheckBox.Checked;
                Properties.Settings.Default.ScreenTimerColor = selectedScreenTimeColorLabel.BackColor;
                Properties.Settings.Default.Save();
                if (timerOverlayForm.ScreenMode) {
                    timerOverlayForm.ScreenTimeLabel.Visible = Properties.Settings.Default.ShowScreenTimer;
                }
                timerOverlayForm.ScreenTimeLabel.ForeColor = Properties.Settings.Default.ScreenTimerColor;
            }
            catch (FormatException) {
                MessageBox.Show("All values must be numerical.", "Input error");
                return false;
            }
            catch (InvalidSettingValueException e) {
                MessageBox.Show(e.Message, "Input error");
                return false;
            }
            return true;
        }

        // Enable/disable megabreak-related input fields according to checkbox value
        private void ToggleMegaBreakFields() {
                megaBreakMinutesInput.Enabled = megaBreaksCheckbox.Checked;
                megaBreakSecondsInput.Enabled = megaBreaksCheckbox.Checked;
                breaksBeforeMegaBreakInput.Enabled = megaBreaksCheckbox.Checked;
        }

        private void OpenScreenTimerColorPicker() {
            screenTimerColorPicker.ShowDialog();
            selectedScreenTimeColorLabel.BackColor = screenTimerColorPicker.Color;
        }
    }
}
