using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace screentimebreak {
    class SettingsField : TextBox {
        public SettingsField(FieldType fieldType, Point point) : base() {
            MaxLength = 2;
            Size = new Size(25, 10);
            Location = point;
        }
    }
    public enum FieldType {
        TimeField,
        IntegerNumberField
    }
}
