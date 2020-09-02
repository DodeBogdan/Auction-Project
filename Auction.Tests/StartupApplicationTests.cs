//-----------------------------------------------------------------------
// <copyright file="StartupApplicationTests.cs" company="Transilvania University of Brasov">
//     Copyright (c) Bogdan Gheorghe Nicolae. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Auction.Tests
{
    using System;
    using System.Linq;
    using AuctionLogic.Business;
    using AuctionLogic.Exceptions;
    using AuctionLogic.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Telerik.JustMock.EntityFramework;

    /// <summary>Startup application test class.</summary>
    [TestClass]
    public class StartupApplicationTests
    {
        /// <summary>The auction mock</summary>
        private AuctionDB auctionMock;

        /// <summary>The startup application</summary>
        private StartupApplication startupApplication;

        /// <summary>Sets up.</summary>
        [TestInitialize]
        public void SetUp()
        {
            auctionMock = EntityFrameworkMock.Create<AuctionDB>();
            startupApplication = new StartupApplication(auctionMock);

            auctionMock.Roles.Add(new Role { ID = 1, RoleName = "Bidder" });
            auctionMock.Roles.Add(new Role { ID = 2, RoleName = "Provider" });

            startupApplication.Register(
                new User
                {
                    FirstName = "Silvia",
                    LastName = "Brassoi",
                    Email = "Silvia.Brassoi@yahoo.com",
                    Password = "Silvia",
                    Age = 23,
                    Gender = "M",
                    CNP = "1123456789123",
                    Adress = "Mandra nr. 61",
                    Phone = "0762275335",
                    RoleStatus = 1,
                    Active = false
                });
        }

        /// <summary>Registers the register user insert in database.</summary>
        [TestMethod]
        public void Register_RegisterUser_InsertInDatabase()
        {
            Assert.IsTrue(auctionMock.Users.Count() == 1);
        }

        /// <summary>Logs the in pass valid user and password assert pass.</summary>
        [TestMethod]
        public void LogIn_PassValidUserAndPassword_AssertPass()
        {
            startupApplication.LogIn("Silvia.Brassoi@yahoo.com", "Silvia", 1);
        }

        /// <summary>Logs the in pass invalid user return invalid user exception.</summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidUserException))]
        public void LogIn_PassInvalidUser_ReturnInvalidUserException()
        {
            startupApplication.LogIn("x", "Silvia", 1);
        }

        /// <summary>Logs the in pass wrong password return invalid password exception.</summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidPasswordException))]
        public void LogIn_PassWrongPassword_ReturnInvalidPasswordException()
        {
            startupApplication.LogIn("Silvia.Brassoi@yahoo.com", "silvia", 1);
        }

        /// <summary>Logs the in log in twice return user logged in exception.</summary>
        [TestMethod]
       // [ExpectedException(typeof(UserLoggedInException))]
        public void LogIn_LogInTwice_ReturnUserLoggedInException()
        {
            try
            {
                startupApplication.LogIn("Silvia.Brassoi@yahoo.com", "Silvia", 1);
                startupApplication.LogIn("Silvia.Brassoi@yahoo.com", "Silvia", 1);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("The user is already logged in.", ex.Message);
            }
        }

        /// <summary>Logs the in when logged in user have bidder role status.</summary>
        [TestMethod]
        public void LogIn_WhenLoggedIn_UserHaveBidderRoleStatus()
        {
            startupApplication.LogIn("Silvia.Brassoi@yahoo.com", "Silvia", 1);

            var email = auctionMock.Users
                .Where(x => x.Active)
                .Select(x => x.Email).SingleOrDefault();

            string role = (from users in auctionMock.Users
                           join roles in auctionMock.Roles on users.RoleStatus equals roles.ID
                           where users.Email == email
                           select roles.RoleName).SingleOrDefault();

            Assert.IsTrue(role.Trim() == "Bidder", role);
        }

        /// <summary>Logs the in when logged in user have provider role status.</summary>
        [TestMethod]
        public void LogIn_WhenLoggedIn_UserHaveProviderRoleStatus()
        {
            startupApplication.Register(
                new User
                {
                    FirstName = "Silvia",
                    LastName = "Brassoi",
                    Email = "silvia.Brassoi@yahoo.com",
                    Password = "Silvia",
                    RoleStatus = 1,
                    Age = 23,
                    Gender = "M",
                    CNP = "1123456789123",
                    Adress = "Mandra nr. 61",
                    Phone = "0712345678",
                });

            startupApplication.LogIn("silvia.Brassoi@yahoo.com", "Silvia", 2);

            var email = auctionMock.Users
                .Where(x => x.Active)
                .Select(x => x.Email).SingleOrDefault();

            var role = (from users in auctionMock.Users
                           join roles in auctionMock.Roles on users.RoleStatus equals roles.ID
                           where users.Email == email
                           select roles.RoleName).SingleOrDefault();

            Assert.IsTrue(role != null && role.Trim() == "Provider", role);
        }

        /// <summary>Logs the in when logged in user give wrong role smaller than one.</summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidRoleStatusException))]
        public void LogIn_WhenLoggedIn_UserGiveWrongRoleSmallerThanOne()
        {
            startupApplication.Register(
                new User
                {
                    FirstName = "Silvia",
                    LastName = "Brassoi",
                    Email = "silvia.Brassoi@yahoo.com",
                    Password = "Silvia",
                    Age = 23,
                });

            startupApplication.LogIn("silvia.Brassoi@yahoo.com", "Silvia", 0);
        }

        /// <summary>Logs the in when logged in user give wrong role higher than two.</summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidRoleStatusException))]
        public void LogIn_WhenLoggedIn_UserGiveWrongRoleHigherThanTwo()
        {
            startupApplication.Register(
                new User
                {
                    FirstName = "Silvia",
                    LastName = "Brassoi",
                    Email = "silvia.Brassoi@yahoo.com",
                    Password = "Silvia",
                    Age = 23,
                });

            startupApplication.LogIn("silvia.Brassoi@yahoo.com", "Silvia", 3);
        }

        /// <summary>Refreshes the when application starts set all expired products to false.</summary>
        [TestMethod]
        public void Refresh_WhenApplicationStarts_SetAllExpiredProductsToFalse()
        {
            auctionMock.Products.Add(new Product
            {
                ID = 4,
                Name = "Paste",
                Description = "Can be found in different flavors.",
                IDState = 1,
                IDCategory = 2,
                IDUser = 2,
                StartDate = DateTime.Now.AddMinutes(-1),
                EndDate = DateTime.Now.AddMinutes(-10),
                StartPrice = 25f,
                Active = true,
                Coin = "RON"
            });

            var productCount = auctionMock.Products
                .Count(x => x.Active);

            Assert.IsTrue(productCount == 1);

            startupApplication.Refresh();

            productCount = auctionMock.Products
                .Count(x => x.Active);

            Assert.IsTrue(productCount == 0);
        }
    }
}
