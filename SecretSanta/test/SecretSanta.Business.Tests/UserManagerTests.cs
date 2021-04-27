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
        public void Create_WithValidItem_ReturnsItem()
        {
            //Arrange
            UserManager manager = new();
            User createdItem = new User() {Id = 6, FirstName = "Korone", LastName = "Inugami"};
            
            //Act
            User result = manager.Create(createdItem);

            //Assert
            Assert.AreEqual(result, createdItem);
        }

    }
}
