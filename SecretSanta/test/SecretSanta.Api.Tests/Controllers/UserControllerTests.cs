using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.DTO;
using SecretSanta.Api.Tests.Business;
using SecretSanta.Data;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class UserControllerTests
    {
        [TestMethod]
        public async Task GetAll_WithData_ReturnsUsers()
        {
            //Arrange
            WebApplicationFactory factory = new();
            TestableUserRepository repos = factory.Repository;
            User user1 = new()
            {
                Id = 23,
                FirstName = "Watame",
                LastName = "Tsunomaki"
            };
            User user2 = new()
            {
                Id = 42,
                FirstName = "Towa",
                LastName = "Tokoyomi"
            };

            repos.GetUserList.Add(user1);
            repos.GetUserList.Add(user2);

            HttpClient client = factory.CreateClient();

            //Act
            HttpResponseMessage response = await client.GetAsync("/api/users");

            //Assert
            response.EnsureSuccessStatusCode();
        }

        [TestMethod]
        public async Task Get_WithId_ReturnsUser()
        {
            //Arrange
            WebApplicationFactory factory = new();
            TestableUserRepository repos = factory.Repository;
            User user1 = new()
            {
                Id = 23,
                FirstName = "Watame",
                LastName = "Tsunomaki"
            };

            repos.GetItemUser = user1;
            repos.GetItemId = 23;

            HttpClient client = factory.CreateClient();

            //Act
            HttpResponseMessage response = await client.GetAsync("/api/users/23");

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual("Watame", repos.GetItemUser.FirstName);
            Assert.AreEqual("Tsunomaki", repos.GetItemUser.LastName);
        }

        [TestMethod]
        public async Task Delete_WithValidId_RemovesUser()
        {
            //Arrange
            WebApplicationFactory factory = new();
            TestableUserRepository repos = factory.Repository;
            User user1 = new()
            {
                Id = 23,
                FirstName = "Watame",
                LastName = "Tsunomaki"
            };

            repos.RemoveList.Add(23);

            HttpClient client = factory.CreateClient();

            //Act
            HttpResponseMessage response = await client.DeleteAsync("/api/users/23");

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.IsFalse(repos.RemoveList.Contains(23));
        }

        [TestMethod]
        public async Task Delete_WithInvalid_ReturnsNotFound()
        {
            //Arrange
            WebApplicationFactory factory = new();
            TestableUserRepository repos = factory.Repository;
            User user1 = new()
            {
                Id = 23,
                FirstName = "Watame",
                LastName = "Tsunomaki"
            };

            repos.RemoveList.Add(23);

            HttpClient client = factory.CreateClient();

            //Act
            HttpResponseMessage response = await client.DeleteAsync("/api/users/42");

            //Assert
            Assert.IsTrue(response.StatusCode == HttpStatusCode.NotFound);
            Assert.IsTrue(repos.RemoveList.Contains(23));
        }

        [TestMethod]
        public async Task Post_WithValidUser_CreatesUser()
        {
            //Arrange
            WebApplicationFactory factory = new();
            TestableUserRepository repos = factory.Repository;

            FullUser user1 = new() {
                Id = 1,
                FirstName = "Towa",
                LastName = "Tokoyami"
            };

            HttpClient client = factory.CreateClient();

            //Act
            HttpResponseMessage response = await client.PostAsJsonAsync("/api/users", user1);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.IsTrue(repos.CreatedUsers[1].FirstName == "Towa");
            Assert.IsTrue(repos.CreatedUsers[1].LastName == "Tokoyami");
        }

        [TestMethod]
        public async Task Post_WithInvalidUser_ReturnsBadRequest()
        {
            //Arrange
            WebApplicationFactory factory = new();
            TestableUserRepository repos = factory.Repository;

            FullUser user1 = null;

            HttpClient client = factory.CreateClient();

            //Act
            HttpResponseMessage response = await client.PostAsJsonAsync("/api/users", user1);

            //Assert
            Assert.IsTrue(response.StatusCode == HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public async Task Put_WithValidData_UpdatesUser()
        {
            //Arrange
            WebApplicationFactory factory = new();
            TestableUserRepository repos = factory.Repository;
            User foundUser = new User
            {
                Id = 42
            };
            repos.GetItemUser = foundUser;

            HttpClient client = factory.CreateClient();
            NameUser updateUser = new()
            {
                FirstName = "Watame",
                LastName = "Tsunomaki"
            };

            //Act
            HttpResponseMessage response = await client.PutAsJsonAsync("/api/users/42", updateUser);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual("Watame", repos.SavedUser?.FirstName);
            Assert.AreEqual("Tsunomaki", repos.SavedUser?.LastName);
        }

        [TestMethod]
        public async Task Put_WithNullData_ReturnsBadRequest()
        {
            //Arrange
            WebApplicationFactory factory = new();
            TestableUserRepository repos = factory.Repository;
            User foundUser = new User
            {
                Id = 42
            };
            repos.GetItemUser = foundUser;
            NameUser updateUser = null;

            HttpClient client = factory.CreateClient();

            //Act
            HttpResponseMessage response = await client.PutAsJsonAsync("/api/users/42", updateUser);

            //Assert
            Assert.IsTrue(response.StatusCode == HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public async Task Put_WithNullUserToUpdate_ReturnsNotFound()
        {
            //Arrange
            WebApplicationFactory factory = new();
            TestableUserRepository repos = factory.Repository;
            User foundUser = null;
            repos.GetItemUser = foundUser;

            HttpClient client = factory.CreateClient();
            NameUser updateUser = new()
            {
                FirstName = "Watame",
                LastName = "Tsunomaki"
            };

            //Act
            HttpResponseMessage response = await client.PutAsJsonAsync("/api/users/42", updateUser);

            //Assert
            Assert.IsTrue(response.StatusCode == HttpStatusCode.NotFound);
        }
    }
}
