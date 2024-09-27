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
        private List<TimeLine> _TimeLines { get; set; } = new();
        public IReadOnlyCollection<TimeLine> TimeLines
        {
            get => _TimeLines.ToImmutableList();
            private set => _TimeLines = value.ToList();
        }
        
        public SocialNetwork()
        {
        }

        public bool Post(Person person, string message)
        {
            TimeLine timeLine = Read(person);
            if (timeLine == null)
            {
                timeLine = new TimeLine(person);
            }
            timeLine.AddMessage(message);
            _TimeLines.Add(timeLine);
            return true;
        }

        public TimeLine Read(Person person)
        {
            if (!_TimeLines.Any(t => t.Owner == person))
            {
                return null;
            }
            return _TimeLines.First(t => t.Owner == person);
        }
    }
}
