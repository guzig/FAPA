using System.Windows;
using System.Windows.Input;

namespace FaPA.Infrastructure.Utils
{
    public class FocusAdvancement
    {
        public static readonly DependencyProperty AdvancesByEnterKeyProperty =
            DependencyProperty.RegisterAttached( "AdvancesByEnterKey"
                                                , typeof( bool )
                                                , typeof( FocusAdvancement )
                                                , new UIPropertyMetadata( false, OnAdvancesByEnterKeyPropertyChanged ) );

        public static bool GetAdvancesByEnterKey( DependencyObject obj )
        {
            return ( bool ) obj.GetValue( AdvancesByEnterKeyProperty );
        }

        public static void SetAdvancesByEnterKey( DependencyObject obj, bool value )
        {
            obj.SetValue( AdvancesByEnterKeyProperty, value );
        }

        static void ue_PreviewKeyDown( object sender, KeyEventArgs e )
        {
            var ue = e.OriginalSource as FrameworkElement;

            //var tags = ue.Tag as string;

            //if (tags == null || !tags.Contains("EnterAsTab")) 
            //    return;

            if ( e.Key == Key.Enter )
            {
                //e.Handled = true;
                ue.MoveFocus( new TraversalRequest( FocusNavigationDirection.Next ) { Wrapped = true } );
            }
        }

        private static void ue_Unloaded( object sender, RoutedEventArgs e )
        {
            var ue = sender as FrameworkElement;
            if ( ue == null ) return;
            ue.Unloaded -= ue_Unloaded;
            ue.PreviewKeyDown -= ue_PreviewKeyDown;
        }

        static void OnAdvancesByEnterKeyPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var ue = d as FrameworkElement;
            if ( ue == null ) return;
            if ( ( bool ) e.NewValue )
            {
                ue.Unloaded += ue_Unloaded;
                ue.PreviewKeyDown += ue_PreviewKeyDown;
            }
            else
            {
                ue.PreviewKeyDown -= ue_PreviewKeyDown;
            }
        }
    }
}

