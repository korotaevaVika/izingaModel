using System;
using System.Globalization;
using System.Windows.Data;

namespace Model_Izinga_WPF.Converters
{
    public class NegativeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value != null)
                {
                    if (value is bool)
                    {
                        return !((bool)value);
                    }
                    else if (value is int)
                    {
                        return -((int)value);
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
