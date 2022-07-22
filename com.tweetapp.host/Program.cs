using AutoMapper;
using com.tweetapp.data.DataAccessObject;
using com.tweetapp.host.Services;
using com.tweetapp.models;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace com.tweetapp.host
{
    class Program
    {
        private static ServiceProvider serviceProvider;
        private static User user = null;
        private static UsersService usersService = new UsersService();
        private static TweetsService tweetsService = new TweetsService();
    static void Main(string[] args)
        {
            AppStart();
            while(true)
            {
                try
                {
                    Menu();
                    Navigation();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
                
            }
            
            
        }
        private static void AppStart()
        {
            Mapper.CreateMap<User, data.Context.Entities.User>();
            Mapper.CreateMap<data.Context.Entities.User,User>();
            Mapper.CreateMap<Tweet, data.Context.Entities.Tweet>();
            Mapper.CreateMap<data.Context.Entities.Tweet,Tweet>();           
            
        }
        private static void Menu()
        {
            if(user == null)
            {
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Forgot Password");
            }
            else
            {
                Console.WriteLine("1. Post a tweet");
                Console.WriteLine("2. View my tweets");
                Console.WriteLine("3. View all tweets");
                Console.WriteLine("4. View all users");
                Console.WriteLine("5. Reset Password");
                Console.WriteLine("6. Logout");
            }
        }
        private static void Navigation()
        {
            var input = Console.ReadLine();
            int option;
            try
            {
                option = int.Parse(input);
            }
            catch(Exception)
            {
                throw new Exception("Kindly enter a valid navigational input as a numeric value");                
            }
            if(user == null)
            {
                NavigationUnlogged(option);
            }
            else
            {
                NavigationLogged(option);
            }
            
            
        }
        private static void NavigationLogged(int option)
        {
            switch (option)
            {
                case 1:
                    tweetsService.PostTweet(user);
                    break;
                case 2:
                    tweetsService.ShowtweetsForUser(user.Id);                    
                    break;
                case 3:
                    tweetsService.ShowAllTweets();
                    break;
                case 4:
                    usersService.ShowAllUsers();
                    break;
                case 5:
                    usersService.ResetPassword(user);
                    break;
                case 6:
                    user = null;
                    break;
                default:
                    Console.WriteLine("Something went wrong please try again later");
                    break;
            }
        }
        private static void NavigationUnlogged(int option)
        {
            switch (option)
            {
                case 1:
                    usersService.Register();
                    break;
                case 2:
                    user = usersService.Login();
                    break;
                case 3:
                    usersService.ForgotPassword();
                    break;
                default:
                    Console.WriteLine("Something went wrong please try again later");
                    break;
            }
        }
    }
}
