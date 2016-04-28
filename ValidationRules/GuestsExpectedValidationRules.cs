using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HCI_2016_Project.ValidationRules
{
    public class IsIntegerValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            try
            {
                var text = value as string;
                try
                {
                    int guests = Int32.Parse(text);

                    if (guests < 0)
                    {
                        return new ValidationResult(false, "Uneseno polje treba biti nenegativan broj.");
                    }
                    else
                    {
                        return new ValidationResult(true, null);
                    }
                }
                catch (FormatException e)
                {
                    return new ValidationResult(false, "Očekivani broj gostiju mora biti cio broj.");
                }
            }
            catch
            {
                return new ValidationResult(false, "Desila se nepredviđena greška.");
            }
        }
    }
}
