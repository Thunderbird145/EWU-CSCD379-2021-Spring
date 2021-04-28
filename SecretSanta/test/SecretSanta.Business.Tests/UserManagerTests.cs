using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System;
using System.Collections.Generic;
using SecretSanta.Business;
using SecretSanta.Data;
using Microsoft.AspNetCore.Mvc;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class UserManagerTests
    {
        [TestMethod]
        public void Create_withUser_ReturnsUser()
        {
            //Arrange
            UserManager manager = new();
            User createdItem = new User() {Id = 6, FirstName = "Korone", LastName = "Inugami"};
            
            //Act
            User result = manager.Create(createdItem);

            //Assert
            Assert.AreEqual(result, createdItem);
        }

        [TestMethod]
        public void GetItem_WithValidId_ReturnsCorrectItem(){
            //Arrange
            UserManager manager = new();
            int id = 13;
            User createdItem = new User() {Id = id, FirstName = "Fubuki", LastName = "Shirikami"};
            User expectedItem = createdItem;
            
            //Act
            manager.Create(createdItem);
            User result = manager.GetItem(id);

            //Assert
            Assert.AreEqual(expectedItem, result);
        }

        [TestMethod]
        public void GetItem_WithInvalidId_ThrowsArgumentOutOfRangeException(){
            UserManager manager = new();
            try
            {
                manager.GetItem(-5);
            }
            catch(ArgumentOutOfRangeException e)
            {
                Assert.IsTrue(e is ArgumentOutOfRangeException);
                return;
            }
            Assert.Fail("No Exception Thrown");
        }

        [TestMethod]
        public void List_WithDefaultValues_ReturnsAllUsers()
        {
            //Arrange
            UserManager manager = new();
            ICollection<User> expectedItems = DeleteMe.Users;
            
            //Act
            ICollection<User> result = manager.List();

            //Assert
            Assert.AreEqual(expectedItems, result);
        }
        
        [TestMethod]
        public void Remove_WithValidId_ReturnsTrue()
        {
            //Arrange
            UserManager manager = new();
            User createdItem = new User() {Id = 53, FirstName = "Mio", LastName = "Ookami"};
            
            //Act
            manager.Create(createdItem);
            Boolean result = manager.Remove(53);

            //Assert
            Assert.IsTrue(result);
        }

        
        [TestMethod]
        public void Remove_WithNonExistantUser_ReturnsFalse()
        {
            //Arrange
            UserManager manager = new();
            User createdItem = new User() {Id = 68, FirstName = "Ayame", LastName = "Nakiri"};
            
            //Act
            manager.Create(createdItem);
            Boolean result = manager.Remove(69);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Save_WithValidUser_UpdatesUser()
        {
            //Arrange
            UserManager manager = new();
            User createdItem = new User() {Id = 67, FirstName = "Mio", LastName = "Ookami"};
            User changedItem = new User() {Id = 67, FirstName = "Korone", LastName = "Inugami"};
            
            //Act
            manager.Create(createdItem);
            manager.Save(changedItem);

            //Assert
            Assert.AreEqual("Korone", manager.GetItem(67).FirstName);
            Assert.AreEqual("Inugami", manager.GetItem(67).LastName);
        }

    }
}
