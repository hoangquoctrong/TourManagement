using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace TourManagementSystem.Global.Ruler
{
    public class DateRuler : ValidationRule
    {
        public string NameFill { get; set; }
        public string TimeDay { get; set; }

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
                int day;
                if (int.TryParse(charString, out day))
                {
                    int timeDay = 0;
                    if (string.IsNullOrEmpty(TimeDay))
                    {
                        timeDay = 0;
                    }
                    else
                    {
                        timeDay = int.Parse(TimeDay);
                    }
                    if (Math.Abs(day - timeDay) > 1)
                    {
                        return new ValidationResult(false, $"{NameFill}'s value is invalid");
                    }
                    else
                    {
                        return new ValidationResult(true, null);
                    }
                }
                else
                {
                    return new ValidationResult(false, $"{NameFill} can't not have any characters except number");
                }
            }
        }
    }

    public class DateTimeAfterRuler : ValidationRule
    {
        public int TimeDay { get; set; }

        public int TimeNight { get; set; }

        public DateTime DateTimeBefore { get; set; }

        public int GetSpanTime(int day, int night)
        {
            return Math.Min(day, night);
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value != null)
            {
                DateTime charDateTime = (DateTime)value;
                if (charDateTime > DateTimeBefore)
                {
                    int timeSpan = (charDateTime - DateTimeBefore).Days;
                    if (timeSpan == GetSpanTime(TimeDay, TimeNight))
                    {
                        return new ValidationResult(true, null);
                    }
                    else
                    {
                        return new ValidationResult(false, "Time Span between Date Start and Date End must the same");
                    }
                }
                else
                {
                    return new ValidationResult(false, "Date After must more than Date Before");
                }
            }
            else
            {
                return new ValidationResult(false, "Please choose Date");
            }
        }
    }
}