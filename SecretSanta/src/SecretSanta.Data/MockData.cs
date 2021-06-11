using System.Collections.Generic;

namespace SecretSanta.Data
{
    public static class MockData
    {
        static MockData()
        {
            PutUserInGroup(Users[1], Groups[1]);
            PutUserInGroup(Users[2], Groups[1]);
            PutUserInGroup(Users[3], Groups[2]);
            PutUserInGroup(Users[4], Groups[2]);
            PutUserInGroup(Users[5], Groups[1]);
            GiftToUser(Users[1], Gifts[1]);
            GiftToUser(Users[2], Gifts[2]);
            GiftToUser(Users[3], Gifts[3]);
            GiftToUser(Users[4], Gifts[4]);
            GiftToUser(Users[5], Gifts[5]);
            GiftToUser(Users[1], Gifts[6]);

            static void PutUserInGroup(User user, Group group)
            {
                user.Groups.Add(group);
                group.Users.Add(user);
            }

            static void GiftToUser(User user, Gift gift) {
                user.Gifts.Add(gift);
                gift.UserId = user.Id;
            }
        }


        public static Dictionary<int, User> Users { get; } = new()
        {
            {
                1,
                new User
                {
                    Id = 1,
                    FirstName = "Inigo",
                    LastName = "Montoya"
                }
            },
            {
                2,
                new User
                {
                    Id = 2,
                    FirstName = "Princess",
                    LastName = "Buttercup"
                }
            },
            {
                3,
                new User
                {
                    Id = 3,
                    FirstName = "Prince",
                    LastName = "Humperdink"
                }
            },
            {
                4,
                new User
                {
                    Id = 4,
                    FirstName = "Count",
                    LastName = "Rugen"
                }
            },
            {
                5,
                new User
                {
                    Id = 5,
                    FirstName = "Miracle",
                    LastName = "Max"
                }
            }
        };

        public static Dictionary<int, Group> Groups { get; } = new()
        {
            {
                1,
                new Group
                {
                    Id = 1,
                    Name = "IntelliTect Christmas Party"
                }
            },
            {
                2,
                new Group
                {
                    Id = 2,
                    Name = "Friends"
                }
            }
        };

        public static Dictionary<int, Gift> Gifts {get;} = new()
        {
            {
                1,
                new Gift
                {
                    Id = 1,
                    Title = "Rat Poison",
                    Desc = "Used to kill rats",
                    Url = "google.com",
                    Priority = 1
                }
            },
            {
                2,
                new Gift
                {
                    Id = 2,
                    Title = "Rat Poison",
                    Desc = "Used to kill rats",
                    Url = "google.com",
                    Priority = 1,
                    UserId = 0
                }
            },
            {
                3,
                new Gift
                {
                    Id = 3,
                    Title = "Rat Poison",
                    Desc = "Used to kill rats",
                    Url = "google.com",
                    Priority = 1,
                    UserId = 0
                }
            },
            {
                4,
                new Gift
                {
                    Id = 4,
                    Title = "Rat Poison",
                    Desc = "Used to kill rats",
                    Url = "google.com",
                    Priority = 1,
                    UserId = 0
                }
            },
            {
                5,
                new Gift
                {
                    Id = 5,
                    Title = "Rat Poison",
                    Desc = "Used to kill rats",
                    Url = "google.com",
                    Priority = 1,
                    UserId = 0
                }
            },
            {
                6,
                new Gift
                {
                    Id = 6,
                    Title = "R-rat",
                    Desc = "Is a rat",
                    Url = "google.com",
                    Priority = 1,
                    UserId = 0
                }
            }
        };
    }
}
