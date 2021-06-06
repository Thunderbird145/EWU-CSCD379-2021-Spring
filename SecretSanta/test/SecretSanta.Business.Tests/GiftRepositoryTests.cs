using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Data;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class GiftRepositoryTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_NullItem_ThrowsArgumentException()
        {
            GiftRepository sut = new();

            sut.Create(null!);
        }

        [TestMethod]
        public void Create_WithItem_CanGetItem()
        {
            GiftRepository sut = new();
            Gift Gift = new()
            {
                Id = 42
            };

            Gift createdGift = sut.Create(Gift);

            Gift? retrievedGift = sut.GetItem(createdGift.Id);
            Assert.AreEqual(Gift, retrievedGift);
        }

        [TestMethod]
        public void GetItem_WithBadId_ReturnsNull()
        {
            GiftRepository sut = new();

            Gift? Gift = sut.GetItem(-1);

            Assert.IsNull(Gift);
        }

        [TestMethod]
        public void GetItem_WithValidId_ReturnsGift()
        {
            GiftRepository sut = new();
            sut.Create(new() 
            { 
                Id = 42,
                Title = "First",
            });

            Gift? Gift = sut.GetItem(42);

            Assert.AreEqual(42, Gift?.Id);
            Assert.AreEqual("First", Gift!.Title);
        }

        // [TestMethod]
        // public void List_WithGifts_ReturnsAllGift()
        // {
        //     GiftRepository sut = new();
        //     sut.Create(new()
        //     {
        //         Id = 42,
        //         Title = "First",
        //     });

        //     ICollection<Gift> Gifts = sut.List();

        //     Assert.AreEqual(MockData.Gifts.Count, Gifts.Count);
        //     foreach(var mockGift in MockData.Gifts.Values)
        //     {
        //         Assert.IsNotNull(Gifts.SingleOrDefault(x => x.Title == mockGift.FirstName && x.LastName == mockGift.LastName));
        //     }
        // }

/*         [TestMethod]
        [DataRow(-1, false)]
        [DataRow(42, true)]
        public void Remove_WithInvalidId_ReturnsTrue(int id, bool expected)
        {
            GiftRepository sut = new();
            sut.Create(new()
            {
                Id = 42,
                FirstName = "First",
                LastName = "Last"
            });

            Assert.AreEqual(expected, sut.Remove(id));
        }
 */
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Save_NullItem_ThrowsArgumentException()
        {
            GiftRepository sut = new();

            sut.Save(null!);
        }

        [TestMethod]
        public void Save_WithValidItem_SavesItem()
        {
            GiftRepository sut = new();

            sut.Save(new Gift() { Id = 42 });

            Assert.AreEqual(42, sut.GetItem(42)?.Id);
        }
    }
}
