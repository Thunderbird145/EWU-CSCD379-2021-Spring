using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using SecretSanta.Web.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Web.Tests.Api;
using SecretSanta.Web.ViewModels;

namespace SecretSanta.Web.Tests.Controller
{
    [TestClass]
    public class UsersControllerTests
    {
        private WebApplicationFactory Factory {get; } = new();


        [TestMethod]
        public async Task Index_WithUsers_InvokesGetAllAsync()
        {
            //Arrange
            FullUser user1 = new() {Id = 1, FirstName = "Kanata", LastName = "Amane"};
            FullUser user2 = new() {Id = 1, FirstName = "Coco", LastName = "Kiryu"};


            TestableUsersClient usersClient = Factory.Client;

            usersClient.GetAllUsersReturnValue = new List<FullUser>() {
                user1, user2
            };

            HttpClient client = Factory.CreateClient();

            //Act
            HttpResponseMessage response = await client.GetAsync("/Users");

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(1, usersClient.GetAllAsyncInvocationCount);
        }

        [TestMethod]
        public async Task Create_WithValidModel_InvokesPostAsync() {
            //Arrange
            TestableUsersClient usersClient = Factory.Client;
            HttpClient client = Factory.CreateClient();

            Dictionary<string, string?> values = new()
            {
                { nameof(UserViewModel.Id), "12"},
                { nameof(UserViewModel.FirstName),  "Watame"},
                { nameof(UserViewModel.LastName), "Tsunomaki"}
            };
            FormUrlEncodedContent content = new(values!);

            //Act
            HttpResponseMessage response = await client.PostAsync("/Users/Create", content);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(1, usersClient.PostAsyncInvocationCount);
            Assert.AreEqual(1, usersClient.PostAsyncInvokedParameters.Count);
            Assert.AreEqual("Watame", usersClient.PostAsyncInvokedParameters[0].FirstName);

        }
    }
}
