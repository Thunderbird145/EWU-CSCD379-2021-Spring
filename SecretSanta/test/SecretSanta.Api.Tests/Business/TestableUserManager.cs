using System.Collections.Generic;
using SecretSanta.Business;
using SecretSanta.Data;

namespace SecretSanta.Api.Tests.Business
{
    public class TestableUserRepository : IUserRepository
    {

        public Dictionary<int, User> CreatedUsers {get; set;} = new();
        public User Create(User item)
        {
            CreatedUsers[item.Id] = item;
            return item;
        }

        public User? GetItemUser { get; set; }
        public int GetItemId { get; set; }
        public User? GetItem(int id)
        {
            GetItemId = id;
            return GetItemUser;
        }

        public List<User>? GetUserList { get; set; } = new();
        public ICollection<User> List()
        {
            return GetUserList;
        }

        public List<int> RemoveList {get; set;} = new();

        public bool Remove(int id)
        {
            return RemoveList.Remove(id);
        }

        public User? SavedUser {get; set;}
        public void Save(User item)
        {
            SavedUser = item;
        }
    }
}