using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace WPFTree2
{
    [ValueConversion(typeof(string), typeof(BitmapImage))]
    public class HeaderToImageConverter : IValueConverter
    {
        public static HeaderToImageConverter Instance = new HeaderToImageConverter();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var path = (string)value;

            if (path == null)
                return null;

            return new BitmapImage(new Uri($"pack://application:,,,/Images/drive.png"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
