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
            if (!UserExistInNetwork(person))
            {
                throw new UserNotFoundInNetworkException(person);
            }

            User user = GetNetworkUser(person);
            return user.TimeLine;
        }

        public Message Post( Message message)
        {
            if (!UserExistInNetwork(message.Owner))
            {
                throw new UserNotFoundInNetworkException(message.Owner);
            }
            
            User user = GetNetworkUser(message.Owner);
            Mention( message);
            user.Post(message);
            return message;
        }
        

        public bool Follow(User follower, User followed)
        {
            if (!UserExistInNetwork(followed))
            {
                throw new UserNotFoundInNetworkException(followed);
            }
            if (!UserExistInNetwork(follower))
            {
                throw new UserNotFoundInNetworkException(follower);
            }

            User user = GetNetworkUser(followed);
            follower.AddSubscription(followed);
            return true;
        }

        public List<TimeLine> ListSubscriptions(User user)
        {
            if (!UserExistInNetwork(user))
            {
                throw new UserNotFoundInNetworkException(user);
            }

            User userInNetwork = GetNetworkUser(user);
            return userInNetwork.FollowedUsers.Select(f => f.TimeLine).ToList();
        }

        public void DirectMessage(User receiver, Message message)
        {
            if (!UserExistInNetwork(receiver))
            {
                throw new UserNotFoundInNetworkException(receiver);
            }
            if (!UserExistInNetwork(message.Owner))
            {
                throw new UserNotFoundInNetworkException(message.Owner);
            }
            receiver.AddPrivateMessage(message);
        }

        private void Mention(Message message)
        {
            foreach (var mentionnedUserName in message.MentionnedUserNames)
            {
                var mentionnedUser = new User(mentionnedUserName);
                if (UserExistInNetwork(mentionnedUser))
                {
                    User mentionnedUserInNetwork = GetNetworkUser(new(mentionnedUserName));
                    mentionnedUserInNetwork.AddMention(message);
                }
            }
        }
        private bool UserExistInNetwork(User user) =>
            _users.Any(u => u.Name == user.Name) ;
        private User GetNetworkUser(User user) => 
            _users.First(u => u.Name == user.Name);
        
    }
}
