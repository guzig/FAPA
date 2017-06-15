using System;
using System.Linq;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FaPA.Infrastructure.Utils
{
    public static class NumberOnlyBehaviour
    {

        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached( "IsEnabled", typeof( bool ),
                typeof( NumberOnlyBehaviour ), new UIPropertyMetadata( false, OnValueChanged ) );

        public static bool GetIsEnabled( Control o )
        {
            return ( bool ) o.GetValue( IsEnabledProperty );
        }

        public static void SetIsEnabled( Control o, bool value )
        {
            o.SetValue( IsEnabledProperty, value );
        }

        private static void OnValueChanged( DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs e )
        {
            var uiElement = dependencyObject as Control;
            if ( uiElement == null ) return;
            if ( e.NewValue is bool && ( bool ) e.NewValue )
            {
                uiElement.PreviewTextInput += OnTextInput;
                DataObject.AddPastingHandler( uiElement, OnPaste );
            }

            else
            {
                uiElement.PreviewTextInput -= OnTextInput;
                DataObject.RemovePastingHandler( uiElement, OnPaste );
            }
        }

        private static void OnTextInput( object sender, TextCompositionEventArgs e )
        {
            if ( e.Text.Any( c => !IsNumber( c ) ) ) { e.Handled = true; }
        }

        private static bool IsNumber( char c )
        {
            if ( char.IsDigit( c ) )
                return true;

            if ( c != '\r' )
            {
                SystemSounds.Exclamation.Play();
            }

            return false;
        }

        private static void OnPaste( object sender, DataObjectPastingEventArgs e )
        {
            if ( e.DataObject.GetDataPresent( DataFormats.Text ) )
            {
                var text = Convert.ToString( e.DataObject.GetData( DataFormats.Text ) ).Trim();
                if ( text.Any( c => !IsNumber( c ) ) ) { e.CancelCommand(); }
            }
            else
            {
                e.CancelCommand();
            }
        }
    }
}
