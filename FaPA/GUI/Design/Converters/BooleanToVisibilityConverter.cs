using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace FaPA.GUI.Design.Converters
{
    /// <summary>
    /// Converts a Boolean value into a Visibility enumeration (and back)
    /// </summary>
    [ValueConversion( typeof( bool ), typeof( Visibility ) )]
    [MarkupExtensionReturnType( typeof( BooleanToVisibilityConverter ) )]
    public class BooleanToVisibilityConverter : MarkupExtension, IValueConverter
    {
        /// <summary>
        /// FalseEquivalent (default : Visibility.Collapsed => see Constructor)
        /// </summary>
        public Visibility FalseEquivalent { get; set; }
        /// <summary>
        /// Define whether the opposite boolean value is crucial (default : false)
        /// </summary>
        public bool OppositeBooleanValue { get; set; }

        /// <summary>
        /// Initialize the properties with standard values
        /// </summary>
        public BooleanToVisibilityConverter()
        {
            this.FalseEquivalent = Visibility.Collapsed;
            this.OppositeBooleanValue = false;
        }

        //+------------------------------------------------------------------------
        //+ Usage :
        //+ -------
        //+ 1. <conv:BoolToVisibilityConverter x:Key="BoolToVisibilityConverter" />
        //+ 2. {Binding ... Converter={StaticResource BoolToVisibilityConverter}
        //+------------------------------------------------------------------------
        #region IValueConverter Members

        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            if ( value is bool && targetType == typeof( Visibility ) )
            {
                bool? booleanValue = ( bool? ) value;

                if ( this.OppositeBooleanValue == true )
                {
                    booleanValue = !booleanValue;
                }

                return booleanValue.GetValueOrDefault() ? Visibility.Visible : FalseEquivalent;
            }

            return value;
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            if ( value is Visibility )
            {
                Visibility visibilityValue = ( Visibility ) value;

                if ( this.OppositeBooleanValue == true )
                {
                    visibilityValue = visibilityValue == Visibility.Visible ? FalseEquivalent : Visibility.Visible;
                }

                return visibilityValue == Visibility.Visible;
            }

            return value;
        }

        #endregion // IValueConverter Members

        //+-----------------------------------------------------------------------------------
        //+ Usage :	(wpfsl: => XML Namespace mapping to the "BoolToVisibilityConverter" class)
        //+ -------
        //+ Use as follows : {Binding ... Converter={wpfsl:BoolToVisibilityConverter}
        //+ NO StaticResource required
        //+-----------------------------------------------------------------------------------
        #region MarkupExtension "overrides"

        public override object ProvideValue( IServiceProvider serviceProvider )
        {
            return new BooleanToVisibilityConverter
            {
                FalseEquivalent = this.FalseEquivalent,
                OppositeBooleanValue = this.OppositeBooleanValue
            };
        }

        #endregion
    }
}
