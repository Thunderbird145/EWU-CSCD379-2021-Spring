using System.Collections.Generic;

namespace SecretSanta.Api.Dto
{
    public class User
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public List<Gift> Gifts {get; set;} = new();

        public static User? ToDto(Data.User? user, bool includeChildObjects = false)
        {
            if (user is null) return null;
            User tempUser = new User
            {
                FirstName = user.FirstName,
                Id = user.Id,
                LastName = user.LastName
            };
            foreach(Data.Gift? gift in user.Gifts)
            {
                if (Gift.ToDto(gift) is { } dtoGift)
                {
                    tempUser.Gifts.Add(dtoGift);
                }
            }
            return tempUser;
        }

        public static Data.User? FromDto(User? user)
        {
            if (user is null) return null;
            return new Data.User
            {
                Id = user.Id,
                FirstName = user.FirstName ?? "",
                LastName = user.LastName ?? ""
            };
        }
    }
}
