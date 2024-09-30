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
            TimeLine = new(this);
        }
        public TimeLine TimeLine { get;} 

        private List<User> _followedUsers { get; set; } = new();
        public IReadOnlyCollection<User> FollowedUsers
        {
            get => _followedUsers.ToImmutableList();
            private set => _followedUsers = value.ToList();
        }

        private List<PrivateMessage> _privateMessages { get; set; } = new();
        public IReadOnlyCollection<PrivateMessage> PrivateMessages
        {
            get => _privateMessages.ToImmutableList();
            private set => _privateMessages = value.ToList();
        }

        private List<Mention> _mentions { get; set; } = new();
        public IReadOnlyCollection<Mention> Mentions
        {
            get => _mentions.ToImmutableList();
            private set => _mentions = value.ToList();
        }


        public void AddSubscription(User person)
        {
            if (!_followedUsers.Any(p => p.Name == person.Name))
            {
                _followedUsers.Add(person);
            }
        }

        public void AddPrivateMessage(User person,Message message)
        {
            _privateMessages.Add(new(person, message));
        }
        public bool Post(Message message)
        {
            return TimeLine.AddMessage(message);
        }
        public void AddMention(User user,Message message)
        {
            _mentions.Add(new(user, message));
        }
    }
}
