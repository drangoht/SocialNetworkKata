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
        private List<User> _users ;
        public IReadOnlyCollection<User> Users
        {
            get => _users.ToImmutableList();
            private set => _users = value.ToList();
        }
        
        public SocialNetwork()
        {
            _users = new List<User>();
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
                return new TimeLine();
            }
            return user.TimeLine;
        }

        public Message Post( Message message)
        {
            User? userToPost = GetUser(message.Owner);
            if (userToPost is null)
            {
                return message;
            }
            Mention( message);
            userToPost.Post(message);
            return message;
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

        public void DirectMessage(User receiver, Message message)
        {
            User? userInNetwork = GetUser(receiver);
            if (userInNetwork is null)
            {
                return;
            }
            receiver.AddPrivateMessage(message);
        }

        private void Mention(Message message)
        {
            foreach (var mentionnedUserName in message.MentionnedUserNames)
            {
                User? mentionnedUser = GetUser(new(mentionnedUserName));
                if (mentionnedUser is not null)
                {
                    mentionnedUser.AddMention(message);
                }
            }
        }
        private User? GetUser(User user)
        {
            return _users.FirstOrDefault(u => u.Name == user.Name);
        }
    }
}
