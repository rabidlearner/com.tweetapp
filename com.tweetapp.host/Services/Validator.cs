using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace com.tweetapp.host.Services
{
    public class Validator
    {
        public bool ValidateEmail(string input)                            
        {
            try
            {
                var mail = new System.Net.Mail.MailAddress(input);             //checks the email address using the Dotnet SMTP class library if the given address is of the type email
                return false;
            }
            catch
            {
                return true;
            }
        }
        public bool ValidateName(string input)
        {
            if(string.IsNullOrWhiteSpace(input))                               //checks if name is a null space or white space
            {
                return true;
            }
            return false;
        }
        public bool ValidatePhone(string input)
        {
            if(Regex.IsMatch(input, @"[0-9]{10}"))                             //regex expression to check if phone number is a numeric value with 10 digits
            {
                return false;
            }
            return true;            
        }
        public bool ValidatePassword(string input)
        {
            if (Regex.IsMatch(input, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$"))                      //regex expression to check if the password contains minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character
            {
                return false;
            }
            return true;
        }
    }
}
