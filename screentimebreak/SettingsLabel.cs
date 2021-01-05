using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace screentimebreak {
    class SettingsLabel : Label {
        public SettingsLabel(Point point, String text) : base() {
            AutoSize = true;
            Text = text;
            Location = point;
        }
    }
}
