using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Application.Validator
{
    internal static class FluentValidator
    {
        public static bool BeValidUsername(this string input)
        {
            try
            {
                input = input.Replace(" ", string.Empty);
                return input.All(char.IsLetterOrDigit);
            }
            catch (System.Exception)
            {
                return false;
            }
        }
        public static bool BeValidName(this string input)
        {
            try
            {
                input = input.Replace(" ", string.Empty);
                return input.All(char.IsLetter);
            }
            catch (System.Exception)
            {
                return false;
            }
        }
        public static bool BeValidPassword(string password)
        {
            //lower case, Upper case and number
            //var reg = new Regex(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{6,32}$");
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMiniMaxChars = new Regex(@".{6,32}");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");
            return hasUpperChar.IsMatch(password) && hasLowerChar.IsMatch(password) && hasNumber.IsMatch(password);
        }

        public static bool BeAValidName(string input)
        {
            try
            {
                input = input.Replace(" ", string.Empty);
                return input.All(char.IsLetter);
            }
            catch (System.Exception)
            {
                return false;
            }
        }
        public static bool BeAValidAddress(string input)
        {
            try
            {
                input = System.Text.RegularExpressions.Regex.Replace(input, "[*'\"., _&^@]", string.Empty);
                return input.All(char.IsLetterOrDigit);
            }
            catch (System.Exception)
            {
                return false;
            }
        }
        public static bool BeAValidCoordinate(string input)
        {
            try
            {
                input = input.Replace(".", string.Empty);
                input = input.Replace(",", string.Empty);
                return input.All(char.IsDigit);
            }
            catch (System.Exception)
            {
                return false;
            }
        }
        public static bool BeAValidDigit(string input)
        {
            try
            {
                return input.All(char.IsDigit);
            }
            catch (System.Exception)
            {
                return false;
            }
        }
        public static bool BeAValidAge(DateTime date)
        {
            int currentYear = DateTime.Now.Year;
            int dobYear = date.Year;

            if (dobYear <= currentYear && dobYear > (currentYear - 120))
            {
                return true;
            }

            return false;
        }
        public static bool BeABiggerThanZero(int input)
        {
            return input > 0;
        }
    }
}
