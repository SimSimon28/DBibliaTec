using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace DBibliaTec.Classes
{
    internal class ColorClassesStatus : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int status)
            {
                if (status == 2)
                {
                    return "Red";
                }
                else return "Green";
            }
            else return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
