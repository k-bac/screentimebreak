using System;

namespace screentimebreak {
    class InvalidSettingValueException : Exception {
        public InvalidSettingValueException(string Message) : base(Message) {
        }
    }
}
