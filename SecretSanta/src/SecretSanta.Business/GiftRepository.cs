using System;
using System.Collections.Generic;
using SecretSanta.Data;

namespace SecretSanta.Business
{
    public class GiftRepository : IGiftRepository
    {
        public Gift Create(Gift item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            using var DatabaseContext = new DatabaseContext();
            DatabaseContext.Gifts.Add(item);
            DatabaseContext.SaveChangesAsync();

            return item;
        }

        public Gift? GetItem(int id)
        {
            using var DatabaseContext = new DatabaseContext();

            Gift gift = DatabaseContext.Gifts.Find(id);

            return gift;
        }

        public ICollection<Gift> List()
        {
            using DatabaseContext DatabaseContext = new DatabaseContext();
            List<Gift> giftList = new List<Gift>();
            foreach (var gift in DatabaseContext.Gifts)
            {
                giftList.Add(gift);
            }
            return giftList;
        }

        public bool Remove(int id)
        {
            try {
                using var DatabaseContext = new DatabaseContext();
                Gift gift = DatabaseContext.Gifts.Find(id);
                DatabaseContext.Gifts.Remove(gift);
                DatabaseContext.SaveChangesAsync();
                return true;
            } catch {
                return false;
            }
        }

        public void Save(Gift item)
        {
            if (item is null)
            {
                throw new System.ArgumentNullException(nameof(item));
            }

            using var DatabaseContext = new DatabaseContext();

            Gift temp = DatabaseContext.Gifts.Find(item.Id);
            if (temp is null)
            {
                Create(item);
            }
            else
            {
                DatabaseContext.Gifts.Remove(DatabaseContext.Gifts.Find(item.Id));
                DatabaseContext.Gifts.Add(item);
            }
            DatabaseContext.SaveChangesAsync();
        }
    }
}
