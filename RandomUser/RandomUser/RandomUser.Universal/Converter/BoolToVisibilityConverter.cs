using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace RandomUser.Universal.Converter
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (targetType == typeof(Visibility))
            {
                var param = parameter as string;
                if (param != null && param.Equals("r", StringComparison.CurrentCultureIgnoreCase))
                    return !(value is bool && (bool)value) ? Visibility.Visible : Visibility.Collapsed;

                return (value is bool && (bool)value) ? Visibility.Visible : Visibility.Collapsed;
            }

            if (targetType == typeof(double) || targetType == typeof(int))
            {
                var param = parameter as string;
                if (param != null && param.Equals("r", StringComparison.CurrentCultureIgnoreCase))
                    return !(value is bool && (bool)value) ? 1 : 0;

                return (value is bool && (bool)value) ? 1 : 0;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}

