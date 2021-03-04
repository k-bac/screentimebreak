using System.Drawing;
using System.Windows.Forms;

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
