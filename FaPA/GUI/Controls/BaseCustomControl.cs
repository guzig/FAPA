using System.Windows;
using System.Windows.Controls;

namespace FaPA.GUI.Controls
{
    public class BaseCustomControl: UserControl
    {
        private bool _onTime=true;
 
        public BaseCustomControl()
        {

            Loaded+= SetUp;
            
            
            Unloaded += (o, args) =>
                            {
                                Loaded -= SetUp;

                                DataContextChanged -= OnDataContextChanged; 

                                IsVisibleChanged -= OnIsVisibleChanged;

                            };
        }

        private void SetUp(object sender, RoutedEventArgs routedEventArgs)
        {
            SetFocusOnFirstFocusableElement();

            if (_onTime)
            {
                DataContextChanged += OnDataContextChanged;

                IsVisibleChanged += OnIsVisibleChanged;             
            }
            
            _onTime = false;

        }
        

        private void OnDataContextChanged(object o, DependencyPropertyChangedEventArgs args)
        {
            SetFocusOnFirstFocusableElement();
        }

        private void OnIsVisibleChanged(object o, DependencyPropertyChangedEventArgs args)
        {
            if ((bool)args.NewValue)
                SetFocusOnFirstFocusableElement();
        }

        protected virtual void SetFocusOnFirstFocusableElement()
        {}

    }
}