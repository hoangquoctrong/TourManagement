using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TourManagementSystem.Global.Ruler
{
    public class MoneyRuler : ValidationRule
    {
        public string NameFill { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string charString = value as string;

            double num;
            if (string.IsNullOrEmpty(charString))
            {
                return new ValidationResult(false, $"{NameFill} can't empty!");
            }
            else if (!double.TryParse(charString, out num))
            {
                return new ValidationResult(false, $"{NameFill}'s value must be a number");
            }
            else
            {
                if (num < 1000)
                {
                    return new ValidationResult(false, $"{NameFill}'s value must be a Vietnamese price number");
                }
                else if (num % 1000 != 0)
                {
                    return new ValidationResult(false, $"{NameFill}'s value must be a Vietnamese price number");
                }
                else
                {
                    return new ValidationResult(true, null);
                }
            }
        }
    }

    public class NumberRuler : ValidationRule
    {
        public string NameFill { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string charString = value as string;
            Regex regex = new Regex(@"^[0-9]*$");

            if (string.IsNullOrEmpty(charString))
            {
                return new ValidationResult(false, $"{NameFill} can't empty!");
            }
            else if (!regex.IsMatch(charString))
            {
                return new ValidationResult(false, $"{NameFill} can't not have any characters except number");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }

    public class PhoneNumberRuler : ValidationRule
    {
        public int MinimumCharacters { get; set; }

        public int MaximumCharacters { get; set; }

        public string NameFill { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string charString = value as string;
            Regex regex = new Regex(@"^[0-9]*$");

            if (string.IsNullOrEmpty(charString))
            {
                return new ValidationResult(false, $"{NameFill} can't empty!");
            }
            else if (!regex.IsMatch(charString))
            {
                return new ValidationResult(false, $"{NameFill} can't not have any characters except number");
            }
            else
            {
                if (charString.Length < MinimumCharacters)
                {
                    return new ValidationResult(false, $"{NameFill} can't not less than {MinimumCharacters} characters");
                }
                else if (charString.Length > MaximumCharacters)
                {
                    return new ValidationResult(false, $"{NameFill} can't not more than {MaximumCharacters} characters");
                }
                else
                {
                    return new ValidationResult(true, null);
                }
            }
        }
    }
}