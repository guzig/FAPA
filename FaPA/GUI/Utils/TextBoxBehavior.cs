using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace FaPA.GUI.Utils
{
    public class TextBoxBehavior
    {
        private static Type[] Types = new Type[] {
            //typeof(AutoCompleteBox),
            typeof(ComboBox),
            typeof(DataGridTextColumn)

        };
        public static int GetMaxLength( DependencyObject element )
        {
            return ( int ) element.GetValue( MaxLengthProperty );
        }

        public static void SetMaxLength( DependencyObject element, int value )
        {
            element.SetValue( MaxLengthProperty, value );
        }

        private static void ValidateElement( DependencyObject element )
        {
            if ( element == null )
                throw new ArgumentNullException( "element" );
            if ( !Types.Contains( element.GetType() ) )
                throw new NotSupportedException( "The TextBoxBehavior is not supported for the given element" );
        }


        public static readonly DependencyProperty MaxLengthProperty = DependencyProperty.RegisterAttached( "MaxLength", typeof( int ), typeof( TextBoxBehavior ), new FrameworkPropertyMetadata( int.MaxValue, TextBox_MaxLengthChanged ) );
        private static void TextBox_MaxLengthChanged( object sender, DependencyPropertyChangedEventArgs e )
        {
            if ( sender == null )
                return;

            dynamic value = ( int ) e.NewValue;

            //if ( sender is AutoCompleteBox )
            //{
            //    dynamic acb = ( AutoCompleteBox ) sender;

            //    if ( acb.IsLoaded )
            //    {
            //        dynamic tb = ( TextBox ) acb.Template.FindName( "Text", acb );
            //        tb.MaxLength = value;
            //    }
            //    else {
            //        acb.AddHandler( AutoCompleteBox.LoadedEvent, new RoutedEventHandler( Element_Loaded ) );
            //    }
            //}
            //else 
        
            if ( sender is ComboBox )
            {
                dynamic cb = ( ComboBox ) sender;
                if ( cb.IsLoaded )
                {
                    dynamic tb = ( TextBox ) cb.Template.FindName( "PART_EditableTextBox", cb );
                    tb.MaxLength = value;
                }
                else {
                    cb.AddHandler( ComboBox.LoadedEvent, new RoutedEventHandler( Element_Loaded ) );
                }
            }
            else if ( sender is DataGridTextColumn )
            {
                dynamic dgtc = ( DataGridTextColumn ) sender;

                dynamic setter = GetIsMaxLengthSet( dgtc.EditingElementStyle );
                if ( setter == null )
                {
                    dynamic style = new Style( typeof( TextBox ), dgtc.EditingElementStyle );
                    style.Setters.Add( new Setter( TextBox.MaxLengthProperty, value ) );
                    dgtc.EditingElementStyle = style;
                    style.Seal();
                }
                else {
                    setter.Value = value;
                }
            }
        }

        private static Setter GetIsMaxLengthSet( Style style )
        {
            if ( style == null )
                return null;
            dynamic setter = style.Setters.LastOrDefault( s => s is Setter && object.ReferenceEquals( ( ( Setter ) s ).Property, TextBox.MaxLengthProperty ) );
            if ( setter != null )
                return setter;
            else
                return GetIsMaxLengthSet( style.BasedOn );
        }

        private static void Element_Loaded( object sender, RoutedEventArgs e )
        {
            var uiElement = ( ( UIElement ) sender );
            dynamic ml = GetMaxLength( uiElement );
            TextBox_MaxLengthChanged( sender, new DependencyPropertyChangedEventArgs( TextBox.MaxLengthProperty, -1, ml ) );
            uiElement.RemoveHandler( FrameworkElement.LoadedEvent, new RoutedEventHandler( Element_Loaded ) );
        }
    }
}

