using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkKata
{
    public class TimeLine
    {
        public Person Owner { get => _owner; }

        private Person _owner;
        private List<Message> _messages { get; set; } = new();
        public IReadOnlyCollection<Message> Messages
        {
            get => _messages.ToImmutableList();
            private set => _messages = value.ToList();
        }

        private List<Person> _subscribers { get; set; } = new();
        public IReadOnlyCollection<Person> Subscribers
        {
            get => _subscribers.ToImmutableList();
            private set => _subscribers = value.ToList();
        }
        public TimeLine(Person owner)
        {
            _messages = new List<Message>();
            _owner = owner;
        }
        public bool AddMessage(string message)
        {
            _messages.Add(new Message( message));
            return true;
        }
        public void Subscribe(Person person)
        {
            if(!_subscribers.Any(p => p == person))
            {
                _subscribers.Add(new Person(person.Name));
            }
        }
        
    }
}
