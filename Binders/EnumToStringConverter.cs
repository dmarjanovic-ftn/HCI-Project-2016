using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HCI_2016_Project.Binders
{
    public class EnumToStringConverter : IValueConverter
    {
        private Dictionary<String, String> EnumValues = 
            new Dictionary<string,string>()
            {
                {"NO_ALCOHOL", "Nije dozvoljen"},
                {"CAN_BUY", "Može se kupiti"},
                {"CAN_BRING", "Može se donijeti"},
                {"FREE", "Besplatno"},
                {"LOW_PRICE", "Niske cijene"},
                {"MEDIUM_PRICE", "Srednje cijene"},
                {"HIGH_PRICE", "Visoke cijene"}
            };

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string EnumString;
            try
            {
                EnumString = Enum.GetName((value.GetType()), value);
                return EnumValues[EnumString];
            }
            catch
            {
                return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
