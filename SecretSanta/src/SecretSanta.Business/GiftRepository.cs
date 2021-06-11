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
            using var dbContext = new DbContext();
            dbContext.Gifts.Add(item);
            dbContext.SaveChangesAsync();

            return item;
        }

        public Gift? GetItem(int id)
        {
            using var dbContext = new DbContext();

            Gift gift = dbContext.Gifts.Find(id);

            return gift;
        }

        public ICollection<Gift> List()
        {
            using DbContext dbContext = new DbContext();
            List<Gift> giftList = new List<Gift>();
            foreach (var gift in dbContext.Gifts)
            {
                giftList.Add(gift);
            }
            return giftList;
        }

        public bool Remove(int id)
        {
            try {
                using var dbContext = new DbContext();
                Gift gift = dbContext.Gifts.Find(id);
                dbContext.Gifts.Remove(gift);
                dbContext.SaveChangesAsync();
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

            using var dbContext = new DbContext();

            Gift temp = dbContext.Gifts.Find(item.Id);
            if (temp is null)
            {
                Create(item);
            }
            else
            {
                dbContext.Gifts.Remove(dbContext.Gifts.Find(item.Id));
                dbContext.Gifts.Add(item);
            }
            dbContext.SaveChangesAsync();
        }
    }
}
