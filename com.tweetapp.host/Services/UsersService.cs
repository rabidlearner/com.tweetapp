using com.tweetapp.data.DataAccessObject;
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
                Console.WriteLine("{0} || Name:{1} {2}  Email:{3}  Contact:{4}", user.Id, user.FirstName, user.LastName, user.Email, user.ContactNumber);
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
                    Console.WriteLine("Kindly enter a valid Email Address- Example: ramesh@yahoo.com");
                    email = Console.ReadLine();
                }
                user = usersDao.GetUser(email);

                if (user == null)
                {
                    Console.WriteLine("Entered email address doesn't exist in our records, kindly verify and try again");
                    email = "";
                    continue;
                }

                while (validator.ValidatePassword(password))
                {
                    Console.WriteLine("Kindly enter valid Password");
                    password = Console.ReadLine();
                }
                
                if(user.Password!=password)
                {
                    Console.WriteLine("Confirm the Password again. It should match your original Password");
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
                Console.WriteLine("Kindly enter valid First Name - Example: Ramesh");
                firstName = Console.ReadLine();
            }
            while(validator.ValidateName(lastName))
            {
                Console.WriteLine("Kindly enter valid Last Name - Example: Sharma");
                lastName = Console.ReadLine();
            }
            while (validator.ValidateEmail(email) || uservalidator != null)
            {
                if(uservalidator != null)
                {
                    Console.WriteLine("Email has already been registered");
                    email = "";
                    uservalidator = null;
                }
                else
                {
                    Console.WriteLine("Kindly enter valid Email Address - Example: ramesh@yahoo.com");
                    email = Console.ReadLine();
                    uservalidator = usersDao.GetUser(email);
                }                
            }
            
            while (validator.ValidatePassword(password))
            {
                Console.WriteLine("Kindly enter valid Password containing minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character");
                password = Console.ReadLine();
            }
            
            while (password != confirmPassword)
            {
                Console.WriteLine("Confirm the Password again. It should match your original Password");
                confirmPassword = Console.ReadLine();
            }
            while (validator.ValidatePhone(contactNumber))
            {
                Console.WriteLine("Kindly enter a valid 10 digit Mobile number");
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
                    Console.WriteLine("Kindly enter valid Email Address - Example: ramesh@yahoo.com");
                    email = Console.ReadLine();
                }

                while (validator.ValidatePhone(contactNumber))
                {
                    Console.WriteLine("Kindly enter a valid 10 digit Mobile number");
                    contactNumber = Console.ReadLine();
                }

                user = usersDao.GetUser(email);

                if (user == null)
                {
                    Console.WriteLine("Entered email does not exist in our records, kindly verify and try again");
                    continue;
                }
                else if (user.ContactNumber != contactNumber)
                {
                    Console.WriteLine("Entered contact number is incorrect, kindly verify and try again");
                    continue;
                }
                else
                {
                    isValid = false;
                }

            }
            while (validator.ValidatePassword(password))
            {
                Console.WriteLine("Kindly enter valid Password containing minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character");
                password = Console.ReadLine();
            }

            while (password != confirmPassword)
            {
                Console.WriteLine("Confirm the Password again. It should match your original Password");
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
                Console.WriteLine("Kindly enter valid Password for reset containing minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character");
                password = Console.ReadLine();
            }

            while (password != confirmPassword)
            {
                Console.WriteLine("Confirm the Password again. It should match your original Password");
                confirmPassword = Console.ReadLine();
            }
            user.Password = password;
            usersDao.PutUser(user);
        }
    }
}
