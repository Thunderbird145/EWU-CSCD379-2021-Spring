using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SecretSanta.Data;

namespace SecretSanta.Business
{
    public class UserRepository : IUserRepository
    {
        public User Create(User item)
        {
            if (item is null)
            {
                throw new System.ArgumentNullException(nameof(item));
            }
            using var Context = new Context();
            Context.Users.Add(item);
            Context.SaveChangesAsync();

            return item;
        }

        public User? GetItem(int id)
        {
            using var Context = new Context();

            User user = Context.Users.Include(blog => blog.Posts).Find(id);

            return user;
        }

        public ICollection<User> List()
        {
            using Context Context = new Context();
            List<User> userList = new List<User>();
            foreach (var user in Context.Users)
            {
                userList.Add(user);
            }
            return userList;
        }

        public bool Remove(int id)
        {

            try {
                using var Context = new Context();
                User user = Context.Users.Find(id);
                foreach(Gift gift in user.Gifts) {
                    IGiftRepository giftrepo = new GiftRepository();
                    giftrepo.Remove(gift.Id);
                }
                Context.Users.Remove(user);
                Context.SaveChangesAsync();
                return true;
            } catch {
                return false;
            }
        }

        public void Save(User item)
        {
            if (item is null)
            {
                throw new System.ArgumentNullException(nameof(item));
            }

            using var Context = new Context();

            User temp = Context.Users.Find(item.Id);
            if (temp is null)
            {
                Create(item);
            }
            else
            {
                Context.Users.Remove(Context.Users.Find(item.Id));
                Context.Users.Add(item);
            }
            Context.SaveChangesAsync();
        }
    }
}
