using System;
using System.Globalization;
using System.Net.Mail;
using System.Windows.Controls;

namespace TourManagementSystem.Global.Ruler
{
    public class MiniMumCharacterRuler : ValidationRule
    {
        public int MinimumCharacters { get; set; }

        public int MaximumCharacters { get; set; }

        public string NameFill { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string charString = value as string;

            if (string.IsNullOrEmpty(charString))
            {
                return new ValidationResult(false, $"{NameFill} can't empty!");
            }
            else if (charString.Length < MinimumCharacters)
            {
                return new ValidationResult(false, $"{NameFill} Char can't less than {MinimumCharacters} characters");
            }
            else if (MaximumCharacters > 0)
            {
                if (charString.Length > MaximumCharacters)
                {
                    return new ValidationResult(false, $"{NameFill} Char can't more than {MaximumCharacters} characters");
                }
                else
                {
                    return new ValidationResult(true, null);
                }
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }

    public class EmailRuler : ValidationRule
    {
        public string NameFill { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string charString = value as String;

            if (string.IsNullOrEmpty(charString))
            {
                return new ValidationResult(false, $"{NameFill} can't empty!");
            }
            else if (charString.Trim().EndsWith("."))
            {
                return new ValidationResult(false, $"{NameFill} has wrong email format.");
            }
            else
            {
                try
                {
                    var addr = new MailAddress(charString);
                    if (addr.Address == charString)
                    {
                        return new ValidationResult(true, null);
                    }
                    else
                    {
                        return new ValidationResult(false, $"{NameFill} has wrong email format.");
                    }
                }
                catch
                {
                    return new ValidationResult(false, $"{NameFill} has wrong email format.");
                }
            }
        }
    }
}