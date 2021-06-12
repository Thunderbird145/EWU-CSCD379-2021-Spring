using System.Collections.Generic;

namespace SecretSanta.Data
{
    public class GroupUser
    {
        public int GroupId { get; set; }
        public int UserId { get; set; }
        public List<Group> Groups = new();
        public List<User> Gifts = new();
    }
}
