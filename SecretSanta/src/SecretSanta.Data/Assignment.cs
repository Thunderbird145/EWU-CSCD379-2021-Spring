using System;
using System.Collections.Generic;

namespace SecretSanta.Data
{
    public class Assignment
    {
        public int Id {get; set;}
        public User Giver { get; set;}
        public User Receiver { get; set;}
        public List<Group> Groups { get; } = new();
        
        public Assignment(User giver, User recipient)
        {
            Giver = giver ?? throw new ArgumentNullException(nameof(giver));
            Receiver = recipient ?? throw new ArgumentNullException(nameof(recipient));
        }

        public Assignment()
        {
            Giver = new User();
            Receiver = new User();
        }
    }
}
