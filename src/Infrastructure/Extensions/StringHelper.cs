using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Infrastructure.Extensions
{
    public static class StringHelper
    {
        //for email
        const string _pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
        //
        //for invalid chars
        public static readonly string _invalidFileNameChars = Regex.Escape(new string(System.IO.Path.GetInvalidFileNameChars()));
        public static readonly string _invalidPathChars = Regex.Escape(new string(System.IO.Path.GetInvalidPathChars()));

        public static bool IsValidEmail(string email)
        {
            try
            {
                Regex regex = new Regex(_pattern, RegexOptions.IgnoreCase);
                return !string.IsNullOrEmpty(email) && regex.IsMatch(email);
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidUrl(this string input)
        {
            return Uri.TryCreate(input, UriKind.Absolute, out Uri uri)
                && (uri.Scheme == Uri.UriSchemeHttp
                 || uri.Scheme == Uri.UriSchemeHttps
                 || uri.Scheme == Uri.UriSchemeFtp
                 || uri.Scheme == Uri.UriSchemeMailto);
        }

        public static string CleanSpecialChars(this string input)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(input))
                    return "";

                input = input.Trim();
                input = Regex.Replace(input, "[*'\",_&#*!%^$@]", "");
                input = Regex.Replace(input, $@"([{_invalidFileNameChars}]*\.+$)|([{_invalidPathChars}]+)", "");
                return input;
            }
            catch
            {
                return input;
            }
        }
        public static bool IsValidName(this string input)
        {
            try
            {
                input = input.Replace(" ", string.Empty);
                return input.All(char.IsLetter);
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool IsValidUsername(this string input)
        {
            try
            {
                input = input.Replace(" ", string.Empty);
                return input.All(char.IsLetterOrDigit);
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool IsValidPassword(this string password)
        {
            Regex passwordPolicyExpression = new Regex(@"((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#!$%]).{6,32})");
            return passwordPolicyExpression.IsMatch(password);
        }
    }
}
