﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SocialNetworkKata
{
    public class Message
    {
        private List<string> _mentionnedUserNames { get; set; } = new();
        public IReadOnlyCollection<string> MentionnedUserNames
        {
            get => _mentionnedUserNames.ToImmutableList();
            private set => _mentionnedUserNames = value.ToList();
        }
    
        private List<string> _links { get; set; } = new();
        public IReadOnlyCollection<string> Links
        {
            get => _links.ToImmutableList();
            private set => _links = value.ToList();
        }

        public User Owner { get; }
        public string Text { get; }
        public Message(User owner,string message)
        {
            Owner = owner;
            Text = message;
            GetMentionnedUsers();
            GetLinks();
        }
        private void GetMentionnedUsers()
        {
            string pattern = @"@?(@([a-zA-Z]+))";
            Regex rg = new Regex(pattern);

            MatchCollection mentionnedUsers = rg.Matches(Text);
            if (mentionnedUsers.Count > 0)
            {
                _mentionnedUserNames = mentionnedUsers.Select(m => m.Groups[2].Value).ToList();
            }
        }

        public void GetLinks()
        {
            string pattern = @"href=[\'""]?([^\'"" >]+)";
            Regex rg = new Regex(pattern);

            MatchCollection links = rg.Matches(Text);
            if (links.Count > 0)
            {
                _links = links.Select(m => m.Groups[1].Value).ToList();
            }
        }
    }
}
