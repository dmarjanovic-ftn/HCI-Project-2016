using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Text.RegularExpressions;

using HCI_2016_Project.DataClasses;

namespace HCI_2016_Project.ValidationRules
{
    // Pravilo kojim provjeravamo da li je uneseno polje i duzinu unesenog polja
    public class LengthValidationRule : ValidationRule
    {
        private static int MIN_LENGTH = 8;
        private static int MAX_LENGTH = 20;

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            try
            {
                var text = value as string;
                if (0 == text.Length)
                {
                    return new ValidationResult(false, "Oznaka manifestacije je obavezna.");
                }
                else if (text.Length < MIN_LENGTH)
                {
                    return new ValidationResult(false, "Minimalna dužina polja je " + MIN_LENGTH + " karaktera.");
                }
                else if (text.Length > MAX_LENGTH)
                {
                    return new ValidationResult(false, "Maksimalna dužina polja je " + MAX_LENGTH + " karaktera.");
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

    // Pravilo kojim provjeravamo sadrzaj koji je unesen u polje
    public class ContentValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            try
            {
                var text = value as string;

                Regex r = new Regex(@"[^0-9a-zA-Z]+", RegexOptions.IgnoreCase);

                Match m = r.Match(text);

                if (m.Success)
                {
                    return new ValidationResult(false, "Labela može da sadrži samo cifre i slova.");
                }

                return new ValidationResult(true, null);
            }
            catch
            {
                return new ValidationResult(false, "Desila se nepredviđena greška.");
            }
        }
    }

    // Pravilo kojim provjeravamo da li je uneseno polje jedinstveno
    public class UniqueValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            try
            {
                var text = value as string;

                foreach (Manifestation manifestation in AppData.GetInstance().Manifestations)
                {
                    if (manifestation.Label == text)
                    {
                        return new ValidationResult(false, "Unesena oznaka već postoji. Polje mora biti jedinstveno.");
                    }
                }

                return new ValidationResult(true, null);
            }
            catch
            {
                return new ValidationResult(false, "Desila se nepredviđena greška.");
            }
        }
    }
}
