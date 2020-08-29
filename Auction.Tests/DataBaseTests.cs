//-----------------------------------------------------------------------
// <copyright file="DataBaseTests.cs" company="Transilvania University of Brasov">
//     Copyright (c) Brassoi Silvia Maria. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Auction.Tests
{
    using System;
    using System.Linq;
    using AuctionLogic.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>Database tests</summary>
    [TestClass]
    public class DataBaseTests
    {
        /// <summary>Auctions the database insert user local insert user.</summary>
        [TestMethod]
        public void AuctionDB_InsertUserLocal_InsertUser()
        {
            AuctionDB auction = new AuctionDB();

            auction.Users.Local.Add(new User());

            Assert.IsTrue(auction.Users.Local.Count() == 1);

            auction.Dispose();
        }

        /// <summary>Auctions the database insert user local get inserted user.</summary>
        [TestMethod]
        public void AuctionDB_InsertUserLocal_GetInsertedUser()
        {
            AuctionDB auction = new AuctionDB();

            User user = new User
            {
                ID = 1,
                FirstName = "Dode",
                LastName = "Bogdan",
                Active = false,
                Age = 21,
                Gender = "M",
                CNP = "1123456789123",
                Adress = "Mandra nr. 61",
                Email = "dodebogdan@yahoo.com",
                Password = "dodeBogdan",
                RoleStatus = 1,
            };

            auction.Users.Local.Add(user);

            User newUser = auction.Users.Local.Single(x => x.ID == 1);

            Assert.IsTrue(user == newUser);

            auction.Dispose();
        }

        /// <summary>Auctions the state of the database insert state local insert.</summary>
        [TestMethod]
        public void AuctionDB_InsertStateLocal_InsertState()
        {
            AuctionDB auction = new AuctionDB();

            auction.States.Local.Add(new State());

            Assert.IsTrue(auction.States.Local.Count() == 1);

            auction.Dispose();
        }

        /// <summary>Auctions the state of the database insert state local get inserted.</summary>
        [TestMethod]
        public void AuctionDB_InsertStateLocal_GetInsertedState()
        {
            var auction = new AuctionDB();

            var state = new State
            {
                ID = 1,
                Name = "Good"
            };

            auction.States.Local.Add(state);

            var newState = auction.States.Local.Single(x => x.ID == 1);

            Assert.IsTrue(state == newState);

            auction.Dispose();
        }

        /// <summary>Auctions the database insert role local insert role.</summary>
        [TestMethod]
        public void AuctionDB_InsertRoleLocal_InsertRole()
        {
            var auction = new AuctionDB();

            auction.Roles.Local.Add(new Role());

            Assert.IsTrue(auction.Roles.Local.Count() == 1);

            auction.Dispose();
        }

        /// <summary>Auctions the database insert role local get inserted role.</summary>
        [TestMethod]
        public void AuctionDB_InsertRoleLocal_GetInsertedRole()
        {
            var auction = new AuctionDB();

            var role = new Role
            {
                ID = 1,
                RoleName = "Bidder"
            };

            auction.Roles.Local.Add(role);

            var newRole = auction.Roles.Local.Single(x => x.ID == 1);

            Assert.IsTrue(role == newRole);

            auction.Dispose();
        }

        /// <summary>Auctions the database insert product local insert product.</summary>
        [TestMethod]
        public void AuctionDB_InsertProductLocal_InsertProduct()
        {
            var auction = new AuctionDB();

            auction.Products.Local.Add(new Product());

            Assert.IsTrue(auction.Products.Local.Count() == 1);

            auction.Dispose();
        }

        /// <summary>Auctions the database insert product local get inserted product.</summary>
        [TestMethod]
        public void AuctionDB_InsertProductLocal_GetInsertedProduct()
        {
            AuctionDB auction = new AuctionDB();

            Product product = new Product
            {
                ID = 1,
                Name = "Popcorn",
                Description = "Can be found in different flavors.",
                IDState = 1,
                IDCategory = 1,
                IDUser = 1,
                StartDate = DateTime.Now.AddMinutes(1),
                EndDate = DateTime.Now.AddMinutes(10),
                StartPrice = 25f,
                Active = true,
                Coin = "RON"
            };

            auction.Products.Local.Add(product);

            var newProduct = auction.Products.Local.Single(x => x.ID == 1);

            Assert.IsTrue(product == newProduct);

            auction.Dispose();
        }

        /// <summary>Auctions the database insert category local insert category.</summary>
        [TestMethod]
        public void AuctionDB_InsertCategoryLocal_InsertCategory()
        {
            var auction = new AuctionDB();

            auction.Categories.Local.Add(new Category());

            Assert.IsTrue(auction.Categories.Local.Count() == 1);

            auction.Dispose();
        }

        /// <summary>Auctions the database insert category local get inserted category.</summary>
        [TestMethod]
        public void AuctionDB_InsertCategoryLocal_GetInsertedCategory()
        {
            var auction = new AuctionDB();

            var category = new Category
            {
                ID = 1,
                Name = "Electronics"
            };

            auction.Categories.Local.Add(category);

            Category newCategory = auction.Categories.Local.Single(x => x.ID == 1);

            Assert.IsTrue(category == newCategory);

            auction.Dispose();
        }
    }
}
