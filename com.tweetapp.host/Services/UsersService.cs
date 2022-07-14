using com.tweetapp.data.Dao;
using com.tweetapp.models;
using System;

namespace com.tweetapp.host.Services
{
    public class UsersService 
    {
        private readonly UsersDao usersDao;
        private readonly Validator validator = new Validator();
        public UsersService(UsersDao usersDao)
        {
            this.usersDao = usersDao;
        }
        public UsersService()
        {
            this.usersDao = new UsersDao();
        }
        public void ShowAllUsers()
        {
            var users = usersDao.GetAllUsers();
            foreach(var user in users)
            {
                Console.WriteLine();
                Console.WriteLine("{0} . Name:{1} {2}  Email:{3}  Contact:{4}", user.Id, user.FirstName, user.LastName, user.Email, user.ContactNumber);
                Console.WriteLine();
            }
        }

        public User Login()
        {
            string email = "";
            string password = "";
            bool isValid = true;
            User user = new User();
            while(isValid)
            {
                while (validator.ValidateEmail(email))
                {
                    Console.WriteLine("Please enter valid Email - Ex: virat@gmail.com");
                    email = Console.ReadLine();
                }
                user = usersDao.GetUser(email);

                if (user == null)
                {
                    Console.WriteLine("Entered email doesn't exist in our records, please verify and try again");
                    email = "";
                    continue;
                }

                while (validator.ValidatePassword(password))
                {
                    Console.WriteLine("Please enter valid Password");
                    password = Console.ReadLine();
                }
                
                if(user.Password!=password)
                {
                    Console.WriteLine("Password is incorrect, please verify and try again");
                    password = "";                    
                    continue;
                }
                else
                {
                    isValid = false;
                }
                
            }
            return user;
            
        }

        public void Register()
        {            
            string firstName = "";
            string lastName = "";
            string email = "";
            string contactNumber = "";
            string password = "";
            string confirmPassword = "";
            User uservalidator = null;
            while (validator.ValidateName(firstName))
            {
                Console.WriteLine("Please enter Valid First Name - Ex: Virat");
                firstName = Console.ReadLine();
            }
            while(validator.ValidateName(lastName))
            {
                Console.WriteLine("Please enter Valid Last Name - Ex:Kohli");
                lastName = Console.ReadLine();
            }
            while (validator.ValidateEmail(email) || uservalidator != null)
            {
                if(uservalidator != null)
                {
                    Console.WriteLine("Email already registered");
                    email = "";
                    uservalidator = null;
                }
                else
                {
                    Console.WriteLine("Please enter valid Email - Ex: virat@gmail.com");
                    email = Console.ReadLine();
                    uservalidator = usersDao.GetUser(email);
                }                
            }
            
            while (validator.ValidatePassword(password))
            {
                Console.WriteLine("Please enter valid Password min 8 digits, a number, Uppercase and lowercase");
                password = Console.ReadLine();
            }
            
            while (password != confirmPassword)
            {
                Console.WriteLine("Please enter valid confirmPassword");
                confirmPassword = Console.ReadLine();
            }
            while (validator.validatePhone(contactNumber))
            {
                Console.WriteLine("Please enter valid 10 digit Mobile number ");
                contactNumber = Console.ReadLine();
            }
            User user = new User()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password,
                ContactNumber = contactNumber
            };

            usersDao.PostUser(user);
        }

        public void ForgotPassword()
        {
            string email = "";
            string contactNumber = "";
            string password = "";
            string confirmPassword = "";
            bool isValid = true;
            User user = new User();
            while (isValid)
            {
                while (validator.ValidateEmail(email))
                {
                    Console.WriteLine("Please enter valid Email - Ex: virat@gmail.com");
                    email = Console.ReadLine();
                }

                while (validator.validatePhone(contactNumber))
                {
                    Console.WriteLine("Please enter valid 10 digit Mobile number ");
                    contactNumber = Console.ReadLine();
                }

                user = usersDao.GetUser(email);

                if (user == null)
                {
                    Console.WriteLine("Entered email doesn't exist in our records, please verify and try again");
                    continue;
                }
                else if (user.ContactNumber != contactNumber)
                {
                    Console.WriteLine("Entered contact number is incorrect, please verify and try again");
                    continue;
                }
                else
                {
                    isValid = false;
                }

            }
            while (validator.ValidatePassword(password))
            {
                Console.WriteLine("Please enter valid reset Password - min 8 digits, a number, Uppercase and lowercase");
                password = Console.ReadLine();
            }

            while (password != confirmPassword)
            {
                Console.WriteLine("Please enter valid confirmPassword");
                confirmPassword = Console.ReadLine();
            }
            user.Password = password;
            usersDao.PutUser(user);
        }

        public void ResetPassword(User user)
        {
            string password = "";
            string confirmPassword = "";           
            
            while (validator.ValidatePassword(password))
            {
                Console.WriteLine("Please enter valid reset Password - min 8 digits, a number, Uppercase and lowercase");
                password = Console.ReadLine();
            }

            while (password != confirmPassword)
            {
                Console.WriteLine("Please enter valid confirmPassword");
                confirmPassword = Console.ReadLine();
            }
            user.Password = password;
            usersDao.PutUser(user);
        }
    }
}
