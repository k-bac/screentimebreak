using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace screentimebreak {
    class CountdownLabel : Label {
        public CountdownLabel(int fontSize) : base() {
            ForeColor = Color.White;
            AutoSize = true;
            Font = new Font("Calibri", fontSize);
        }
    }
}
