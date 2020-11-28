using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace screentimebreak {
    class TrayMenu : Form{

        private NotifyIcon trayIcon;
        private ContextMenu trayContextMenu;
        private MenuItem trayExitItem;
        private MenuItem traySettingsItem;

        public TrayMenu(TimerOverlayForm timerOverlayForm) {
            trayIcon = new NotifyIcon();
            trayIcon.BalloonTipIcon = ToolTipIcon.Info;
            trayIcon.Icon = new Icon("trayicon.ico");
            trayIcon.Visible = true;

            trayContextMenu = new ContextMenu();
            trayExitItem = new MenuItem();
            traySettingsItem = new MenuItem();

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
                SettingsForm settingsForm = new SettingsForm();
                settingsForm.Show();
            }

            // Clean up before program exit
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomain_ProcessExit);

            // Clean up tray on program exit
           void CurrentDomain_ProcessExit(object sender, EventArgs e) {
                trayIcon.Visible = false;
                trayIcon.Dispose();
            }

        }
       

    }
}
