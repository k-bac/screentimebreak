using System;
using System.Drawing;
using System.Windows.Forms;

namespace screentimebreak {
    class TrayMenu : Form {

        private TimerOverlayForm timerOverlayForm { get; set; }

        // Param timeOverlayForm: the TimerOverlayForm which will closed when exit option is selected
        public TrayMenu(TimerOverlayForm timerOverlayForm) {
            this.timerOverlayForm = timerOverlayForm;
            InitComponents();
        }
       
        private void InitComponents() {
            NotifyIcon trayIcon = new NotifyIcon();
            trayIcon.BalloonTipIcon = ToolTipIcon.Info;
            trayIcon.Icon = new Icon("trayicon.ico");
            trayIcon.Visible = true;

            ContextMenu trayContextMenu = new ContextMenu();
            MenuItem trayExitItem = new MenuItem();
            MenuItem traySettingsItem = new MenuItem();
            trayContextMenu.MenuItems.Add(trayExitItem);
            trayContextMenu.MenuItems.Add(traySettingsItem);

            trayExitItem.Index = 1;
            trayExitItem.Text = "Exit";
            trayExitItem.Click += new EventHandler(TrayExitItem_Click);

            traySettingsItem.Index = 0;
            traySettingsItem.Text = "Settings";
            traySettingsItem.Click += new EventHandler(TraySettingsItem_Click);

            trayIcon.ContextMenu = trayContextMenu;

            void TrayExitItem_Click(object senderObject, EventArgs evArgs) {
                timerOverlayForm.Close();
            }

            void TraySettingsItem_Click(object senderObject, EventArgs evArgs) {
                SettingsForm settingsForm = new SettingsForm(timerOverlayForm);
                settingsForm.Show();
            }

            AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomain_ProcessExit);

            // Clean up tray on program exit
            void CurrentDomain_ProcessExit(object sender, EventArgs e) {
                trayIcon.Visible = false;
                trayIcon.Dispose();
            }
        }

    }
}
