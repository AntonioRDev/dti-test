using System;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace ProvaDTI.Util
{
    public static class Validator
    {
        // String vazia
        public static bool IsEmptyString(string value)
        {
            bool isEmpty = false;

            if (string.IsNullOrWhiteSpace(value))
            {
                isEmpty = true;
            }

            return isEmpty;
        }

        // Data válida
        public static bool IsValidDate(string date)
        {
            bool isValid = false;
            var regex = @"^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$";
            var match = Regex.Match(date, regex, RegexOptions.IgnoreCase);

            if (match.Success)
            {
                isValid = true;
            }

            return isValid;
        }

        // Email válido
        public static bool IsValidEmail(string email)
        {
            try
            {
                if (IsEmptyString(email))
                {
                    return false;
                }

                MailAddress m = new MailAddress(email);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        // Double parse
        public static bool IsValidDoubleConversion(string value)
        {
            return double.TryParse(value, out _);
        }

        // Int parse
        public static bool IsValidIntegerConversion(string value)
        {
            return int.TryParse(value, out _);
        }
    }
}
