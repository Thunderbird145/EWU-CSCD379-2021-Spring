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
            FullUser user2 = new() {Id = 2, FirstName = "Coco", LastName = "Kiryu"};


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

        
        [TestMethod]
        public async Task Delete_WithValidDelete_InvokesDeleteAsync()
        {
            //Arrange
            int user1 = 0; 
            int user2 = 1;

            TestableUsersClient usersClient = Factory.Client;
            HttpClient client = Factory.CreateClient();

            usersClient.DeleteUserReturnValue.Add(user1);
            usersClient.DeleteUserReturnValue.Add(user2);

            Dictionary<string, string?> values = new()
            {
                { nameof(UserViewModel.Id), "1"},
            };
            FormUrlEncodedContent content = new(values!);

            //Act
            HttpResponseMessage response = await client.PostAsync("/Users/Delete", content);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(1, usersClient.DeleteInvocationCount);
            Assert.IsTrue(usersClient.DeleteUserReturnValue.Contains(user1));
            Assert.IsFalse(usersClient.DeleteUserReturnValue.Contains(user2));
        }

        [TestMethod]
        public async Task Edit_WithValidModel_InvokesPostAsync() {
            //Arrange
            TestableUsersClient usersClient = Factory.Client;
            HttpClient client = Factory.CreateClient();

            NameUser user1 = new() {FirstName = "Kanata", LastName = "Amane"};
            NameUser user2 = new() {FirstName = "Coco", LastName = "Kiryu"};

            usersClient.PutAsyncInvokedParameters.Add(user1);
            usersClient.PutAsyncInvokedParameters.Add(user2);

            Dictionary<string, string?> values = new()
            {
                { nameof(UserViewModel.Id), "1"},
                { nameof(UserViewModel.FirstName),  "Watame"},
                { nameof(UserViewModel.LastName), "Tsunomaki"}
            };
            FormUrlEncodedContent content = new(values!);

            //Act
            HttpResponseMessage response = await client.PostAsync("/Users/Edit", content);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(1, usersClient.PutAsyncInvocationCount);
            Assert.AreEqual("Watame", usersClient.PutAsyncInvokedParameters[1].FirstName);
            Assert.AreEqual("Tsunomaki", usersClient.PutAsyncInvokedParameters[1].LastName);
        }
    }
}
