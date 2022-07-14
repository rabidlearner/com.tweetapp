using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace com.tweetapp.services
{
    public class Validator
    {
        public bool ValidateEmail(string input)
        {
            try
            {
                var mail = new System.Net.Mail.MailAddress(input);
                return false;
            }
            catch
            {
                return true;
            }
        }
        public bool ValidateName(string input)
        {
            if(string.IsNullOrWhiteSpace(input))
            {
                return true;
            }
            return false;
        }
        public bool validatePhone(string input)
        {
            if(Regex.IsMatch(input, @"[0-9]{10}"))
            {
                return false;
            }
            return true;            
        }
        public bool ValidatePassword(string input)
        {
            if (Regex.IsMatch(input, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$"))
            {
                return false;
            }
            return true;
        }
    }
}
