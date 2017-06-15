using System;

namespace Emule.GUI.Util
{
    public class ValidationEventArgs : EventArgs
    {
        public string PropertyName { get; internal set; }
        public string Result { get; set; }

        public ValidationEventArgs(string propertyName)
        {
            PropertyName = propertyName;
        }

        public ValidationEventArgs(string propertyName, string result)
        {
            PropertyName = propertyName;
            Result = result;
        }
    }
}