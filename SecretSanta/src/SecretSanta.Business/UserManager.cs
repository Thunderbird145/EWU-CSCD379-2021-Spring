using System.Collections.Generic;
using SecretSanta.Data;

namespace SecretSanta.Business {
    public class UserManager : IUserRepository
    
    {
        public User Create(User item)
        {
            DeleteMe.Users.Add(item);
            return item;
        }

        public User? GetItem(int id)
        {
            throw new System.NotImplementedException();
        }

        public ICollection<User> List()
        {
            return DeleteMe.Users;
        }

        public bool Remove(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Save(User item)
        {
            throw new System.NotImplementedException();
        }

    }
}