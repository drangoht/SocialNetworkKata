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
        private List<Message> _messages;

        public IReadOnlyCollection<Message> Messages
        {
            get => _messages.ToImmutableList();
            private set => _messages = value.ToList();
        }


        public TimeLine()
        {
            _messages = new List<Message>();
        }
        public bool AddMessage(Message message)
        {
            _messages.Add( message);
            return true;
        }
        
    }
}
