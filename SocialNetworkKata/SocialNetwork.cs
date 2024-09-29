using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkKata
{
    public class SocialNetwork
    {
        private List<User> _users { get; set; } = new();
        public IReadOnlyCollection<User> Users
        {
            get => _users.ToImmutableList();
            private set => _users = value.ToList();
        }
        
        public SocialNetwork()
        {
        }

        public void AddUser(User user)
        {
            _users.Add(user);
        }

        public TimeLine Read(User person)
        {
            User? user = GetUser(person);
            if (user is null)
            {
                return new TimeLine(person);
            }
            return user.TimeLine;
        }

        public bool Post(User user, string message)
        {
            User? userToPost = GetUser(user);
            if (userToPost is null)
            {
                return false;
            }
            userToPost.Post(message);
            return true;
        }

        public bool Follow(User follower, User followed)
        {
            User? user = GetUser(followed);
            if (user is null)
            {
                return false;
            }
            follower.AddSubscription(followed);
            return true;
        }

        public List<TimeLine> ListSubscriptions(User user)
        {
            User? userInNetwork = GetUser(user);
            if (userInNetwork is null)
            {
                return new();
            }
            return userInNetwork.FollowedUsers.Select(f => f.TimeLine).ToList();
        }
        private User? GetUser(User user)
        {
            return _users.FirstOrDefault(u => u.Name == user.Name);
        }
    }
}
