using com.tweetapp.data.Dao;
using com.tweetapp.data.IDao;
using com.tweetapp.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.tweetapp.services.Implementation
{
    public class TweetsService : ITweetsService
    {
        private readonly ITweetsDao tweetsDao;

        public TweetsService(ITweetsDao tweetsDao)
        {
            this.tweetsDao = tweetsDao;
        }
        public TweetsService()
        {
            this.tweetsDao = new TweetsDao();
        }
        public void GetAllTweets()
        {
            var tweets = tweetsDao.GetAllTweets();
            foreach(var tweet in tweets)
            {
                Console.WriteLine();
                Console.WriteLine("{0} - {1}",tweet.User.FirstName,tweet.User.LastName);
                Console.WriteLine("{0}", tweet.Message);
                Console.WriteLine();                
            }
        }

       

        public void GettweetsForUser(int userId)
        {
            var tweets = tweetsDao.GetTweetsForUser(userId);
            foreach (var tweet in tweets)
            {
                Console.WriteLine();
                Console.WriteLine("{0} - {1}", tweet.User.FirstName, tweet.User.LastName);
                Console.WriteLine("{0}", tweet.Message);
                Console.WriteLine();
            }            
        }

        public void PostTweet(User user)
        {
            string message = "";
           
            while(string.IsNullOrWhiteSpace(message))
            {
                Console.WriteLine("Please enter your tweet here..");
                message = Console.ReadLine();
            }
            Tweet tweet = new Tweet()
            {
                UserId = user.Id,
                Message = message
            };
            tweetsDao.PostTweet(tweet);
        }
    }
}
