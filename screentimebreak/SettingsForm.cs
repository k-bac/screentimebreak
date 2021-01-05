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
        private Button cancelButton;

        // Text labels
        private Label breakTime;
        private Label breakSeconds;
        private Label breakMinutes;
        private Label megaBreaks;

        // Value fields
        private TextBox breakMinutesInput;
        private TextBox breakSecondsInput;
        private CheckBox megaBreaksCheckbox;

        // Tabs + TabControl
        private TabPage tabpage1;
        private TabPage tabpage2;
        private TabControl tabControl;

        public SettingsForm() {
            Size = new Size(500, 400);
            Visible = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Text = "Settings";

            addComponents();
        }

        private void addComponents() {
            okButton = new Button();
            okButton.Text = "OK";
            okButton.Location = new Point(15, 330);
            okButton.Click += new EventHandler(okButton_Click);

            cancelButton = new Button();
            cancelButton.Text = "Cancel";
            cancelButton.Location = new Point(95, 330);
            cancelButton.Click += new EventHandler(cancelButton_Click);

            breakTime = new Label();
            breakTime.Text = "Break time";
            breakTime.Location = new Point(0, 20);
            breakTime.AutoSize = true;

            breakMinutesInput = new TextBox();
            breakMinutesInput.Location = new Point(60, 20);
            breakMinutesInput.Size = new Size(25, 10);
            breakMinutesInput.MaxLength = 2;
            breakMinutesInput.Text = Properties.Settings.Default.BreakTimeMinutes.ToString();

            breakSecondsInput = new TextBox();
            breakSecondsInput.Location = new Point(60, 50);
            breakSecondsInput.Size = new Size(25, 10);
            breakSecondsInput.MaxLength = 2;
            breakSecondsInput.Text = Properties.Settings.Default.BreakTimeSeconds.ToString();

            breakMinutes = new Label();
            breakMinutes.Location = new Point(90, 20);
            breakMinutes.Text = "minutes";
            breakMinutes.AutoSize = true;

            breakSeconds = new Label();
            breakSeconds.Location = new Point(90, 50);
            breakSeconds.Text = "seconds";
            breakSeconds.AutoSize = true;

            megaBreaks = new Label();
            megaBreaks.Location = new Point(0, 100);
            megaBreaks.Text = "Enable megabreaks";
            megaBreaks.AutoSize = true;

            megaBreaksCheckbox = new CheckBox();
            megaBreaksCheckbox.Location = new Point(120, 100);
            megaBreaksCheckbox.Checked = Properties.Settings.Default.MegaBreaksEnabled;

            // Tab 1 (Timers)
            tabpage1 = new TabPage();
            tabpage1.Text = "Timers";
            tabpage1.TabIndex = 0;

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
            Properties.Settings.Default.BreakTimeMinutes = Convert.ToInt32(breakMinutesInput.Text);
            Properties.Settings.Default.BreakTimeSeconds = Convert.ToInt32(breakSecondsInput.Text);
            Properties.Settings.Default.MegaBreaksEnabled = megaBreaksCheckbox.Checked;
            Properties.Settings.Default.Save();
        }
    }
}
