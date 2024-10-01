using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkKata
{
    public class UserNotFoundInNetworkException : Exception
    {
        public UserNotFoundInNetworkException(User user) : base($"User {user.Name} not found in network")
        {
            
        }
    }
}
