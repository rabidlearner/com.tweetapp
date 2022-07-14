using com.tweetapp.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.tweetapp.services
{
    public interface IUsersService
    {
        void ShowAllUsers();
        void Register();        
        User Login();
        void ForgotPassword();
        void ResetPassword(User user);
    }
}
