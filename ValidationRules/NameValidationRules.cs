using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

using HCI_2016_Project.DataClasses;

namespace HCI_2016_Project.ValidationRules
{
    // Pravilo kojim provjeravamo da li je uneseno polje i duzinu unesenog polja
    public class NameLengthValidationRule : ValidationRule
    {
        private static int MAX_NAME_LENGTH = 30;

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            try
            {
                var text = value as string;
                if (0 == text.Length)
                {
                    return new ValidationResult(false, "Naziv manifestacije je obavezan.");
                }
                else if (text.Length > MAX_NAME_LENGTH)
                {
                    return new ValidationResult(false, "Maksimalna dužina polja je " + MAX_NAME_LENGTH + " karaktera.");
                }
                else
                {
                    return new ValidationResult(true, null);
                }
            }
            catch
            {
                return new ValidationResult(false, "Desila se nepredviđena greška.");
            }
        }
    }

    // Pravilo kojim provjeravamo da li je uneseno polje i duzinu unesenog polja za naziv tipa manifestacije
    public class MTNameLengthValidationRule : ValidationRule
    {
        private static int MAX_NAME_LENGTH = 30;

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            try
            {
                var text = value as string;
                if (0 == text.Length)
                {
                    return new ValidationResult(false, "Naziv tipa manifestacije je obavezan.");
                }
                else if (text.Length > MAX_NAME_LENGTH)
                {
                    return new ValidationResult(false, "Maksimalna dužina polja je " + MAX_NAME_LENGTH + " karaktera.");
                }
                else
                {
                    return new ValidationResult(true, null);
                }
            }
            catch
            {
                return new ValidationResult(false, "Desila se nepredviđena greška.");
            }
        }
    }
}
