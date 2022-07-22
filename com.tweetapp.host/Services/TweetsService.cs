using com.tweetapp.data.DataAccessObject;
using com.tweetapp.models;
using System;

namespace com.tweetapp.host.Services
{
    public class TweetsService 
    {
        private readonly TweetsDao tweetsDao;

        public TweetsService(TweetsDao tweetsDao)
        {
            this.tweetsDao = tweetsDao;
        }
        public TweetsService()
        {
            this.tweetsDao = new TweetsDao();
        }
        public void ShowAllTweets()
        {
            var tweets = tweetsDao.GetAllTweets();
            foreach(var tweet in tweets)
            {
                Console.WriteLine();
                Console.WriteLine("{0}_{1}",tweet.User.FirstName,tweet.User.LastName);
                Console.WriteLine("{0}", tweet.Message);
                Console.WriteLine();                
            }
        }

       

        public void ShowtweetsForUser(int userId)
        {
            var tweets = tweetsDao.GetTweetsForUser(userId);
            foreach (var tweet in tweets)
            {
                Console.WriteLine();
                Console.WriteLine("{0}_{1}", tweet.User.FirstName, tweet.User.LastName);
                Console.WriteLine("{0}", tweet.Message);
                Console.WriteLine();
            }            
        }

        public void PostTweet(User user)
        {
            string message = "";
           
            while(string.IsNullOrWhiteSpace(message))
            {
                Console.WriteLine("Kindly type your tweet here..");
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
