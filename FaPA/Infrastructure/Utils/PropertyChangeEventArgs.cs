using System;

namespace FaPA.Infrastructure.Utils
{
    public class PropertyChangeEventArgs : EventArgs
    {
        public string PropertyName { get; internal set; }
        public object OldValue { get; internal set; }
        public object NewValue { get; internal set; }

        public PropertyChangeEventArgs(string propertyName, object oldValue, object newValue)
        {
            PropertyName = propertyName;
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}