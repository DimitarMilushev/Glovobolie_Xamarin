using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace GlovobolieApp.Services.ValidationService
{
    public static class ValidationService
    {
        public static String ValidateEmail(String email)
        {
            Regex rx = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            if (string.IsNullOrEmpty(email) || !rx.IsMatch(email)) 
            {
                return "Email is invalid";
            }
            return null;
        }
        public static String ValidatePassword(String password)
        {
            if (string.IsNullOrEmpty(password)) {
                return "Password is invalid";
            }
            Regex rx = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$");
            if (!rx.IsMatch(password))
            {
                return "Password needs to have at least 8 characters, at least one letter, at least one number";
            }
            return null;
        }
        public static String ValidatePasswordsMatch(String password, String confirmPassword)
        {
            if (password != confirmPassword)
            {
                return "Passwords do not match";
            }
            return null;
        }
        public static String ValidateNonNullable(String name, String value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return $"{name} is required!";
            }
            return null;
        }
    }
}
