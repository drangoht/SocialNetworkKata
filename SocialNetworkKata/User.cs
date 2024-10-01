using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkKata
{
    public class User
    {
        public string Name { get; }
        public User(string name)
        {
            Name = name;
            TimeLine = new();
            _followedUsers = new List<User>();
            _privateMessages = new List<Message>();
            _mentions = new List<Message>();
        }
        public TimeLine TimeLine { get;}

        private List<User> _followedUsers;
        public IReadOnlyCollection<User> FollowedUsers
        {
            get => _followedUsers.ToImmutableList();
        }

        private List<Message> _privateMessages;
        public IReadOnlyCollection<Message> PrivateMessages
        {
            get => _privateMessages.ToImmutableList();
        }

        private List<Message> _mentions;
        public IReadOnlyCollection<Message> Mentions
        {
            get => _mentions.ToImmutableList();
        }


        public void AddSubscription(User person)
        {
            if (!_followedUsers.Any(p => p.Name == person.Name))
            {
                _followedUsers.Add(person);
            }
        }

        public void AddPrivateMessage(Message message)
        {
            _privateMessages.Add(message);
        }
        public bool Post(Message message)
        {
            return TimeLine.AddMessage(message);
        }
        public void AddMention(Message message)
        {
            _mentions.Add(message);
        }

    }
}
