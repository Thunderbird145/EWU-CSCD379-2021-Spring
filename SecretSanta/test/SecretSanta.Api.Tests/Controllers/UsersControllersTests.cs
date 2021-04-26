using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using SecretSanta.Api.Controllers;
using System.Collections.Generic;
using SecretSanta.Business;
using SecretSanta.Data;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class UsersControllerTests 
    {
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

        private class TestableUserManager : IUserRepository {
        public User Create(User item)
        {
            DeleteMe.Users.Add(item);
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
            throw new System.NotImplementedException();
        }

        public void Save(User item)
        {
            throw new System.NotImplementedException();
        }
        }
    }

}