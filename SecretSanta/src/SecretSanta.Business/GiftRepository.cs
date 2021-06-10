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
            return item;
        }

        public Gift? GetItem(int id)
        {
            if (MockData.Gifts.TryGetValue(id, out Gift? gift))
            {
                return gift;
            }
            return null;
        }

        public ICollection<Gift> List()
        {
            return MockData.Gifts.Values;
        }

        public bool Remove(int id)
        {
            return MockData.Gifts.Remove(id);
        }

        public void Save(Gift item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            MockData.Gifts[item.Id] = item;
        }
    }
}
