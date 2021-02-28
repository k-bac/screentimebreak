using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace screentimebreak {
    class InvalidSettingValueException : Exception {
        public InvalidSettingValueException(string Message) : base(Message) {
        }
    }
}
