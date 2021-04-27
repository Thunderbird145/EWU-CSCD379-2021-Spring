using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System;
using SecretSanta.Api.Controllers;
using System.Collections.Generic;
using SecretSanta.Business;
using SecretSanta.Data;
using Microsoft.AspNetCore.Mvc;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class UsersControllerTests 
    {
       [TestMethod]
        //[ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WithNullUserManager_ThrowsAppropriateException()
        {
            try
            {
                new UsersController(null!);
            }
            catch(ArgumentNullException e)
            {
                Assert.AreEqual("userManager", e.ParamName);
                return;
            }
            Assert.Fail("No exception thrown");
        }

        [TestMethod]
        public void Get_WithData_ReturnsUsers() {
            //Arrange
            UsersController controller = new(new UserManager());

            //Act
            IEnumerable<User> users = controller.Get();

            //Assert
            Assert.IsTrue(users.Any());
        }

        [TestMethod]
        [DataRow(42)]
        [DataRow(98)]
        public void Get_WithId_ReturnsUserManagerUser(int id) {
            //Arrange
            TestableUserManager manager = new();
            UsersController controller = new(manager);
            User expectedUser = new();
            manager.GetItemUser = expectedUser;

            //Act
            ActionResult<User?> result = controller.Get(id);

            //Assert
            Assert.AreEqual(id, manager.GetItemId);
            Assert.AreEqual(expectedUser, result.Value);
        }

        
        [TestMethod]
        public void Get_WithNegativeId_ReturnsNotFound() {
            //Arrange
            TestableUserManager manager = new();
            UsersController controller = new(manager);
            User expectedUser = new();

            //Act
            ActionResult<User?> result = controller.Get(-1);

            //Assert
            Assert.IsTrue(result.Result is NotFoundResult);
        }

        [TestMethod]
        public void Delete_WithValidId_ReturnsOk() {
            //Arrange
            TestableUserManager manager = new();
            UsersController controller = new(manager);
            User expectedUser = new();

            //Act
            ActionResult<User?> result = controller.Delete(2);

            //Assert
            Assert.IsTrue(result.Result is OkResult);
        }

        [TestMethod]
        public void Delete_WithNonexistantId_ReturnsNotFound() { //Includes negatives, since negatives can never be found.
            //Arrange
            TestableUserManager manager = new();
            UsersController controller = new(manager);
            User expectedUser = new();

            //Act
            ActionResult<User?> result = controller.Delete(980);

            //Assert
            Assert.IsTrue(result.Result is NotFoundResult);
        }

        [TestMethod]
        public void Post_WithNullUser_ReturnsBadRequest() {
            //Arrange
            TestableUserManager manager = new();
            UsersController controller = new(manager);
            User testUser = null;
            //Act
            ActionResult<User?> result = controller.Post(testUser);

            //Assert
            Assert.IsTrue(result.Result is BadRequestResult);
        }

        [TestMethod]
        public void Post_WithValidId_ReturnsUser() {
            //Arrange
            TestableUserManager manager = new();
            UsersController controller = new(manager);
            User testUser = new User() {Id = 15, FirstName = "Calliope", LastName = "Mori"};

            //Act
            ActionResult<User?> result = controller.Post(testUser);

            //Assert
            Assert.AreEqual(testUser, result.Value);
        }

        public void Put_WithValidId_ReturnsOk() {
            //Arrange
            TestableUserManager manager = new();
            UsersController controller = new(manager);
            User oldUser = new User() {Id = 1, FirstName = "K", LastName = "T"};
            User updatedUser = new User() {Id = 1, FirstName = "Kiara", LastName = "Takanashi"};

            manager.GetItemUser = updatedUser;

            //Act
            ActionResult<User?> result = controller.Put(oldUser.Id, updatedUser);

            //Assert
            Assert.IsTrue(result.Result is OkResult);
        }

        [TestMethod]
        public void Put_WithNullUser_ReturnsBadRequest() {
            //Arrange
            TestableUserManager manager = new();
            UsersController controller = new(manager);
            User testUser = null;
            int id = 1;
            
            //Act
            ActionResult<User?> result = controller.Put(id, testUser);

            //Assert
            Assert.IsTrue(result.Result is BadRequestResult);
        }

        [TestMethod]
        [DataRow(52)]
        [DataRow(-5)]
        public void Put_WithInvalidId_ReturnsNotFound(int id) {
            //Arrange
            TestableUserManager manager = new();
            UsersController controller = new(manager);
            User testUser = new User() {Id = id, FirstName = "Gawr", LastName = "Gura"};

            //Act
            ActionResult<User?> result = controller.Put(id, testUser);

            //Assert
            Assert.IsTrue(result.Result is NotFoundResult);
        }

        private class TestableUserManager : IUserRepository {

            public User Create(User item)
            {
                return item;
            }

            public User? GetItemUser { get; set;}
            public int GetItemId { get; set;}
            public User? GetItem(int id)
            {
                GetItemId = id;
                return GetItemUser;
            }

            public ICollection<User> List()
            {
                throw new System.NotImplementedException();
            }

            public bool Remove(int id)
            {
                User? removedData = DeleteMe.Users.FirstOrDefault(x => x.Id == id);
                if (removedData is not null) {
                    return true;
                }
                return false;
            }

            public void Save(User item)
            {
                Remove(item.Id);
                Create(item);
            }
        }   
    }
}