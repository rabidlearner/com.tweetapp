using com.tweetapp.data.Context;
using com.tweetapp.models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace com.tweetapp.data.Dao
{
    public class TweetsDao
    {
        private readonly AppDbContext db = new AppDbContext();
        public List<Tweet> GetAllTweets()
        {
            var tweets = db.Tweets.Include("User").ToList();
            List<Tweet> tweetsDtos = new List<Tweet>();
            foreach (var tweet in tweets)
            {
                tweetsDtos.Add(AutoMapper.Mapper.Map<data.Context.Entities.Tweet, Tweet>(tweet));
            }
            return tweetsDtos;
        }

        public Tweet GetTweet(int tweetId)
        {
            var tweet = db.Tweets.FirstOrDefault(m => m.Id == tweetId);
            return AutoMapper.Mapper.Map<data.Context.Entities.Tweet, Tweet>(tweet);
        }

        public List<Tweet> GetTweetsForUser(int userId)
        {
            var tweets = db.Tweets.Where(m => m.UserId == userId).Include("User").ToList();
            List<Tweet> tweetsDtos = new List<Tweet>();
            foreach (var tweet in tweets)
            {
                tweetsDtos.Add(AutoMapper.Mapper.Map<data.Context.Entities.Tweet, Tweet>(tweet));
            }
            return tweetsDtos;
        }

        public void PostTweet(Tweet tweetDto)
        {
            var tweet = AutoMapper.Mapper.Map<Tweet, data.Context.Entities.Tweet>(tweetDto);
            db.Tweets.Add(tweet);
            db.SaveChanges();
        }
    }
}
