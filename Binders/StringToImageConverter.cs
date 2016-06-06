using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Drawing;

namespace HCI_2016_Project.Binders
{
    public class StringToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || value.GetType() != typeof(String))
                return value;

            String path = (String)value;

            if (path.EndsWith(".png") || path.EndsWith(".jpg"))
            {
                ImageSource result = new BitmapImage(new Uri(path));
                return result;
            }
            else
            {
                return path;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
