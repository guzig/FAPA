using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FaPA.GUI.Design.Templates
{
    /// <summary>
    /// Interaction logic for InstanceNullManager.xaml
    /// </summary>
    public partial class InstanceNullManager : UserControl
    {
        #region empty message ap

        public static readonly DependencyProperty EmptyMessageProperty = DependencyProperty.RegisterAttached(
            "EmptyMessage", typeof (string), typeof (InstanceNullManager), new PropertyMetadata(default(string)));

        public static void SetEmptyMessage(DependencyObject element, string value)
        {
            element.SetValue(EmptyMessageProperty, value);
        }

        public static string GetEmptyMessage(DependencyObject element)
        {
            return (string) element.GetValue(EmptyMessageProperty);
        }

        #endregion

        #region ActionCommandMessage

        public static readonly DependencyProperty ActionCommandMessageProperty = DependencyProperty.RegisterAttached(
            "ActionCommandMessage", typeof (string), typeof (InstanceNullManager), new PropertyMetadata(default(string)));

        public static void SetActionCommandMessage(DependencyObject element, string value)
        {
            element.SetValue(ActionCommandMessageProperty, value);
        }

        public static string GetActionCommandMessage(DependencyObject element)
        {
            return (string) element.GetValue(ActionCommandMessageProperty);
        }

        #endregion

        #region binded instance

        public static readonly DependencyProperty BindedInstanceProperty = DependencyProperty.RegisterAttached(
            "BindedInstance", typeof (object), typeof (InstanceNullManager), new PropertyMetadata(default(object)));

        public static void SetBindedInstance(DependencyObject element, object value)
        {
            element.SetValue(BindedInstanceProperty, value);
        }

        public static object GetBindedInstance(DependencyObject element)
        {
            return (object) element.GetValue(BindedInstanceProperty);
        }

        #endregion

        #region OnNullCommandProperty

        public static readonly DependencyProperty OnNullCommandProperty = DependencyProperty.RegisterAttached(
            "OnNullCommand", typeof (ICommand), typeof (InstanceNullManager), new PropertyMetadata(default(ICommand)));

        public static void SetOnNullCommand(DependencyObject element, ICommand value)
        {
            element.SetValue(OnNullCommandProperty, value);
        }

        public static ICommand GetOnNullCommand(DependencyObject element)
        {
            return (ICommand) element.GetValue(OnNullCommandProperty);
        }

        #endregion

        #region ActionCommandParamProperty

        public static readonly DependencyProperty ActionCommandParamProperty = DependencyProperty.RegisterAttached(
            "ActionCommandParam", typeof (object), typeof (InstanceNullManager), new PropertyMetadata(default(object)));

        public static void SetActionCommandParam(DependencyObject element, object value)
        {
            element.SetValue(ActionCommandParamProperty, value);
        }

        public static object GetActionCommandParam(DependencyObject element)
        {
            return (object) element.GetValue(ActionCommandParamProperty);
        }

        #endregion






        public InstanceNullManager()
        {
            InitializeComponent();
            LayoutRoot.DataContext = this;
        }
    }
}
