using System.Collections.Generic;
using SecretSanta.Data;

namespace SecretSanta.Data
{
    public static class DeleteMe
    {
        public static List<User> Users { get; } = new()
        {
            new User() {Id = 0, FirstName = "Kanata", LastName = "Amane"},
            new User() {Id = 1, FirstName = "Okayu", LastName = "Nekomata"},
            new User() {Id = 2, FirstName = "Coco", LastName = "Kiryu"}
        };
    }
}