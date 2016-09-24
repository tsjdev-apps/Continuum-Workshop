using System;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;
using RandomUser.Portable.Model;

namespace RandomUser.Universal.Converter
{
    public class UserToImagePathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var user = value as User;
            if (user == null)
                return null;

            var bmp = new BitmapImage(new Uri(user.PictureUrl));
            return bmp;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}