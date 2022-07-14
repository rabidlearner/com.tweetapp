using com.tweetapp.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.tweetapp.services
{
    public interface ITweetsService
    {
        void GetAllTweets();
        void PostTweet(User user);        
        void GettweetsForUser(int userId);
    }
}
