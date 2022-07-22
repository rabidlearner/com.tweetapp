using com.tweetapp.data.Context;
using com.tweetapp.models;
using System.Collections.Generic;
using System.Linq;

namespace com.tweetapp.data.DataAccessObject
{
    public class UsersDao
    {
        private readonly AppDbContext db = new AppDbContext();
        public List<User> GetAllUsers()
        {
            var users = db.Users.ToList();
            List<User> usersDto = new List<User>();
            foreach (var user in users)
            {
                usersDto.Add(AutoMapper.Mapper.Map<data.Context.Entities.User, User>(user));
            }
            return usersDto;
        }

        public User GetUser(string email)
        {
            var user = db.Users.FirstOrDefault(m => m.Email == email);
            return AutoMapper.Mapper.Map<data.Context.Entities.User, User>(user);
        }

        public void PostUser(User userDto)
        {
            var user = AutoMapper.Mapper.Map<User, data.Context.Entities.User>(userDto);
            db.Users.Add(user);
            db.SaveChanges();
        }

        public void PutUser(User userDto)
        {
            var user = AutoMapper.Mapper.Map<User, data.Context.Entities.User>(userDto);
            var existingUser = db.Users.Find(user.Id);
            db.Entry(existingUser).CurrentValues.SetValues(user);
            db.SaveChanges();
        }
    }
}
