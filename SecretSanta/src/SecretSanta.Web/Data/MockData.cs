using System;
using System.Collections.Generic;
using SecretSanta.Web.ViewModels;

namespace SecretSanta.Web.Data
{
    public static class MockData
    {
        public static List<GroupViewModel> Groups = new List<GroupViewModel>{
            new GroupViewModel {GroupName = "Storms Roost"},
            new GroupViewModel {GroupName = "Swords of Home"},
        };

        public static List<UserViewModel> Users = new List<UserViewModel>{
            new UserViewModel {Id = 0, FirstName = "Grady", LastName = "Chiamulon"},
            new UserViewModel {Id = 1, FirstName = "Secret", LastName = "Santino"},
        };

        public static List<GiftViewModel> Gifts = new List<GiftViewModel>{
            new GiftViewModel {Id = 0, Title = "Grady", Description = "Chiamulon", Url = "blah blah", Priority = 1, UserId = 0},
            new GiftViewModel {Id = 0, Title = "Grady", Description = "Chiamulon", Url = "blah blah", Priority = 1, UserId = 0},
        };
    }
}