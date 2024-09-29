using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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



        public void AddSubscription(User person)
        {
            if (!_followedUsers.Any(p => p.Name == person.Name))
            {
                _followedUsers.Add(new User(person.Name));
            }
        }

        public bool Post(string message)
        {
            return TimeLine.AddMessage(message);
        }
    }
}
