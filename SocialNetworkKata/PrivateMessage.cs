using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkKata
{
    public class PrivateMessage
    {
        public Message Message { get; }
        public User MessageSender { get; }
        public PrivateMessage(User messageSender, Message message)
        {
            MessageSender = messageSender;
            Message = message;
        }
    }
}
