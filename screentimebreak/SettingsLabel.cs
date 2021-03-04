using System;
using System.Drawing;
using System.Windows.Forms;

namespace screentimebreak {
    class SettingsLabel : Label {
        public SettingsLabel(Point point, String text) : base() {
            AutoSize = true;
            Text = text;
            Location = point;
        }
    }
}
