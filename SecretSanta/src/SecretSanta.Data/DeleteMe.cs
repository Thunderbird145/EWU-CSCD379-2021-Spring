using System.Collections.Generic;
using SecretSanta.Data;

namespace SecretSanta.Data
{
    public static class DeleteMe
    {
        public static List<User> Users { get; } = new()
        {
            new User() {Id = 1, FirstName = "Kanata", LastName = "Amane"},
            new User() {Id = 1, FirstName = "Marine", LastName = "Houshou"},
            new User() {Id = 1, FirstName = "Coco", LastName = "Kiryu"}
        };
    }
}