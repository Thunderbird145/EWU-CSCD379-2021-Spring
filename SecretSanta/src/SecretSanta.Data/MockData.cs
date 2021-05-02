using System.Collections.Generic;

namespace SecretSanta.Data
{
    public static class MockData
    {
        public static Dictionary<int, User> Users { get; } = new()
        {
            {
                1,
                new User
                {
                    Id = 1,
                    FirstName = "Sora",
                    LastName = "Tokino"
                }
            },
            {
                2, 
                new User
                {
                    Id = 2,
                    FirstName = "Robocco",
                    LastName = "5452432"
                }
            },
            {
                3,
                new User
                {
                    Id = 3,
                    FirstName = "Miko",
                    LastName = "Sakura"
                }
            },
            {
                4,
                new User
                {
                    Id = 4,
                    FirstName = "Suisei",
                    LastName = "Hoshimachi"
                }
            },
            {
                5,
                new User
                {
                    Id = 5,
                    FirstName = "Yozora",
                    LastName = "Mel"
                }
            }
        };
    }
}
