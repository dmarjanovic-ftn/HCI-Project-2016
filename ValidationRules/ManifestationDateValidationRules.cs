using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HCI_2016_Project.ValidationRules
{
    public class MinDateValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            DateTime manifestationDate = (DateTime) value;

            return new ValidationResult(manifestationDate > DateTime.Now, "Datum ne smije biti stariji od današnjeg.");
        }
    }
}
