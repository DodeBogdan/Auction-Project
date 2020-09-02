//-----------------------------------------------------------------------
// <copyright file="UserTests.cs" company="Transilvania University of Brasov">
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
    using AuctionLogic.Repositories;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Telerik.JustMock.EntityFramework;

    /// <summary>User tests class.</summary>
    [TestClass]
    public class UserTests
    {
        /// <summary>The auction mock</summary>
        private AuctionDB auctionMock;

        /// <summary>The startup application</summary>
        private StartupApplication startupApplication;

        /// <summary>The user repository mock</summary>
        private UserRepository userRepositoryMock;

        /// <summary>Sets up.</summary>
        [TestInitialize]
        public void SetUp()
        {
            auctionMock = EntityFrameworkMock.Create<AuctionDB>();
            userRepositoryMock = new UserRepository(auctionMock);
            startupApplication = new StartupApplication(auctionMock);

            auctionMock.Roles.Add(new Role { ID = 1, RoleName = "Bidder" });
            auctionMock.Roles.Add(new Role { ID = 2, RoleName = "Provider" });
        }

        /// <summary>Adds the user valid user return true.</summary>
        [TestMethod]
        public void AddUser_ValidUser_ReturnTrue()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi",
                Email = "Silvia.Brassoi@yahoo.com",
                Password = "Silvia",
                Age = 23,
                Gender = "M",
                CNP = "1123456789123",
                Adress = "Mandra nr. 61",
                Phone = "0712345678",
                RoleStatus = 1,
            };

            userRepositoryMock.AddUser(user);

            Assert.IsTrue(auctionMock.Users.Count() == 1);
        }

        /// <summary>Adds the user when user is null return false.</summary>
        [TestMethod]
        public void AddUser_WhenUserIsNull_ReturnFalse()
        {
            userRepositoryMock.AddUser(null);

            Assert.IsTrue(!auctionMock.Users.Any());
        }

        /// <summary>Adds the user user have null first name return false.</summary>
        [TestMethod]
        public void AddUser_UserHaveNullFirstName_ReturnFalse()
        {
            var user = new User
            {
                FirstName = null,
            };

            Assert.IsFalse(userRepositoryMock.AddUser(user));
        }

        /// <summary>Adds the user user have empty first name return false.</summary>
        [TestMethod]
        public void AddUser_UserHaveEmptyFirstName_ReturnFalse()
        {
            var user = new User
            {
                FirstName = string.Empty,
            };

            Assert.IsFalse(userRepositoryMock.AddUser(user));
        }

        /// <summary>Adds the user user have smaller first name return false.</summary>
        [TestMethod]
        public void AddUser_UserHaveSmallerFirstName_ReturnFalse()
        {
            var user = new User
            {
                FirstName = "Ss",
            };

            Assert.IsFalse(userRepositoryMock.AddUser(user));
        }

        /// <summary>Adds the user user have longer first name return false.</summary>
        [TestMethod]
        public void AddUser_UserHaveLongerFirstName_ReturnFalse()
        {
            var user = new User
            {
                FirstName = "Ssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss",
            };

            Assert.IsFalse(userRepositoryMock.AddUser(user));
        }

        /// <summary>Adds the user user have lower first name return false.</summary>
        [TestMethod]
        public void AddUser_UserHaveLowerFirstName_ReturnFalse()
        {
            var user = new User
            {
                FirstName = "silvia",
            };

            Assert.IsFalse(userRepositoryMock.AddUser(user));
        }

        /// <summary>Adds the user user have digit first name return false.</summary>
        [TestMethod]
        public void AddUser_UserHaveDigitFirstName_ReturnFalse()
        {
            var user = new User
            {
                FirstName = "Silvia1",
            };

            Assert.IsFalse(userRepositoryMock.AddUser(user));
        }

        /// <summary>Adds the user user have symbol first name return false.</summary>
        [TestMethod]
        public void AddUser_UserHaveSymbolFirstName_ReturnFalse()
        {
            var user = new User
            {
                FirstName = "Silvia@&^",
            };

            Assert.IsFalse(userRepositoryMock.AddUser(user));
        }

        /// <summary>Adds the user user have null last name return false.</summary>
        [TestMethod]
        public void AddUser_UserHaveNullLastName_ReturnFalse()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = null,
            };

            Assert.IsFalse(userRepositoryMock.AddUser(user));
        }

        /// <summary>Adds the user user have empty last name return false.</summary>
        [TestMethod]
        public void AddUser_UserHaveEmptyLastName_ReturnFalse()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = string.Empty,
            };

            Assert.IsFalse(userRepositoryMock.AddUser(user));
        }

        /// <summary>Adds the user user have smaller last name return false.</summary>
        [TestMethod]
        public void AddUser_UserHaveSmallerLastName_ReturnFalse()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Br",
            };

            Assert.IsFalse(userRepositoryMock.AddUser(user));
        }

        /// <summary>Adds the user user have longer last name return false.</summary>
        [TestMethod]
        public void AddUser_UserHaveLongerLastName_ReturnFalse()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrassoi",
            };

            Assert.IsFalse(userRepositoryMock.AddUser(user));
        }

        /// <summary>Adds the user user have lower last name return false.</summary>
        [TestMethod]
        public void AddUser_UserHaveLowerLastName_ReturnFalse()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "brassoi",
            };

            Assert.IsFalse(userRepositoryMock.AddUser(user));
        }

        /// <summary>Adds the user user have digit last name return false.</summary>
        [TestMethod]
        public void AddUser_UserHaveDigitLastName_ReturnFalse()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi1",
            };

            Assert.IsFalse(userRepositoryMock.AddUser(user));
        }

        /// <summary>Adds the user user have symbol last name return false.</summary>
        [TestMethod]
        public void AddUser_UserHaveSymbolLastName_ReturnFalse()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi@&^",
            };

            Assert.IsFalse(userRepositoryMock.AddUser(user));
        }

        /// <summary>Adds the user user have smaller age return false.</summary>
        [TestMethod]
        public void AddUser_UserHaveSmallerAge_ReturnFalse()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi",
                Age = 17,
            };

            Assert.IsFalse(userRepositoryMock.AddUser(user));
        }

        /// <summary>Adds the user user have higher age return false.</summary>
        [TestMethod]
        public void AddUser_UserHaveHigherAge_ReturnFalse()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi",
                Age = 127,
            };

            Assert.IsFalse(userRepositoryMock.AddUser(user));
        }

        /// <summary>Adds the user user have smaller email return false.</summary>
        [TestMethod]
        public void AddUser_UserHaveSmallerEmail_ReturnFalse()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi",
                Age = 23,
                Email = "si",
            };

            Assert.IsFalse(userRepositoryMock.AddUser(user));
        }

        /// <summary>Adds the user user have longer email return false.</summary>
        [TestMethod]
        public void AddUser_UserHaveLongerEmail_ReturnFalse()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi",
                Age = 23,
                Email = "sidsasdasdasdasdasdasdasdasdasdasdasdasdasddddddddddddddddddddddddddssssssssssssssssssssssssssssss",
            };

            Assert.IsFalse(userRepositoryMock.AddUser(user));
        }

        /// <summary>Adds the user user have invalid email return false.</summary>
        [TestMethod]
        public void AddUser_UserHaveInvalidEmail_ReturnFalse()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi",
                Age = 23,
                Email = "silvia.brassoi",
            };

            Assert.IsFalse(userRepositoryMock.AddUser(user));
        }

        /// <summary>Adds the user user have null role status return false.</summary>
        [TestMethod]
        public void AddUser_UserHaveNullRoleStatus_ReturnFalse()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi",
                Age = 127,
                Email = "Silvia.Brassoi@yahoo.com",
            };

            Assert.IsFalse(userRepositoryMock.AddUser(user));
        }

        /// <summary>Adds the user user have wrong role status return false.</summary>
        [TestMethod]
        public void AddUser_UserHaveWrongRoleStatus_ReturnFalse()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi",
                Age = 127,
                Email = "Silvia.Brassoi@yahoo.com",
                Password = "Silvia",
            };
            Assert.IsFalse(userRepositoryMock.AddUser(user));
        }

        /// <summary>Adds the user user have null password return false.</summary>
        [TestMethod]
        public void AddUser_UserHaveNullPassword_ReturnFalse()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi",
                Age = 23,
                Email = "Silvia.Brassoi@yahoo.com",
            };

            Assert.IsFalse(userRepositoryMock.AddUser(user));
        }

        /// <summary>Adds the user user have empty password return false.</summary>
        [TestMethod]
        public void AddUser_UserHaveEmptyPassword_ReturnFalse()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi",
                Age = 23,
                Email = "Silvia.Brassoi@yahoo.com",
                Password = string.Empty,
            };

            Assert.IsFalse(userRepositoryMock.AddUser(user));
        }

        /// <summary>Adds the user user have smaller password return false.</summary>
        [TestMethod]
        public void AddUser_UserHaveSmallerPassword_ReturnFalse()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi",
                Age = 23,
                Email = "Silvia.Brassoi@yahoo.com",
                Password = "p",
            };

            Assert.IsFalse(userRepositoryMock.AddUser(user));
        }

        /// <summary>Adds the user user have longer password return false.</summary>
        [TestMethod]
        public void AddUser_UserHaveLongerPassword_ReturnFalse()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi",
                Age = 23,
                Email = "Silvia.Brassoi@yahoo.com",
                Password = "ppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppp",
            };

            Assert.IsFalse(userRepositoryMock.AddUser(user));
        }

        /// <summary>Adds the user user have lower password return false.</summary>
        [TestMethod]
        public void AddUser_UserHaveLowerPassword_ReturnFalse()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi",
                Age = 23,
                Email = "Silvia.Brassoi@yahoo.com",
                Password = "silvia",
            };

            Assert.IsFalse(userRepositoryMock.AddUser(user));
        }

        /// <summary>Adds the user user have null gender return false.</summary>
        [TestMethod]
        public void AddUser_UserHaveNullGender_ReturnFalse()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi",
                Age = 23,
                Email = "Silvia.Brassoi@yahoo.com",
                Password = "Silvia",
                RoleStatus = 1,
            };

            Assert.IsFalse(userRepositoryMock.AddUser(user));
        }

        /// <summary>Adds the user user have empty gender return false.</summary>
        [TestMethod]
        public void AddUser_UserHaveEmptyGender_ReturnFalse()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi",
                Age = 23,
                Email = "Silvia.Brassoi@yahoo.com",
                Password = "Silvia",
                RoleStatus = 1,
                Gender = string.Empty,
            };

            Assert.IsFalse(userRepositoryMock.AddUser(user));
        }

        /// <summary>Adds the user user have smaller gender return false.</summary>
        [TestMethod]
        public void AddUser_UserHaveSmallerGender_ReturnFalse()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi",
                Age = 23,
                Email = "Silvia.Brassoi@yahoo.com",
                Password = "Silvia",
                RoleStatus = 1,
                Gender = string.Empty,
            };

            Assert.IsFalse(userRepositoryMock.AddUser(user));
        }

        /// <summary>Adds the user user have longer first name return false.</summary>
        [TestMethod]
        public void AddUser_UserHaveLongerGender_ReturnFalse()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi",
                Age = 23,
                Email = "Silvia.Brassoi@yahoo.com",
                Password = "Silvia",
                RoleStatus = 1,
                Gender = "MM",
            };

            Assert.IsFalse(userRepositoryMock.AddUser(user));
        }

        /// <summary>Adds the user user have lower gender return false.</summary>
        [TestMethod]
        public void AddUser_UserHaveLowerGender_ReturnFalse()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi",
                Age = 23,
                Email = "Silvia.Brassoi@yahoo.com",
                Password = "Silvia",
                RoleStatus = 1,
                Gender = "m",
            };

            Assert.IsFalse(userRepositoryMock.AddUser(user));
        }

        /// <summary>Adds the user user have digit gender return false.</summary>
        [TestMethod]
        public void AddUser_UserHaveDigitGender_ReturnFalse()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi",
                Age = 23,
                Email = "Silvia.Brassoi@yahoo.com",
                Password = "Silvia",
                RoleStatus = 1,
                Gender = "M1",
            };

            Assert.IsFalse(userRepositoryMock.AddUser(user));
        }

        /// <summary>Adds the user user have symbol gender return false.</summary>
        [TestMethod]
        public void AddUser_UserHaveSymbolGender_ReturnFalse()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi",
                Age = 23,
                Email = "Silvia.Brassoi@yahoo.com",
                Password = "Silvia",
                RoleStatus = 1,
                Gender = "M$",
            };

            Assert.IsFalse(userRepositoryMock.AddUser(user));
        }

        /// <summary>Adds the user user have null CNP return false.</summary>
        [TestMethod]
        public void AddUser_UserHaveNullCNP_ReturnFalse()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi",
                Age = 23,
                Email = "Silvia.Brassoi@yahoo.com",
                Password = "Silvia",
                RoleStatus = 1,
                Gender = "M",
            };

            Assert.IsFalse(userRepositoryMock.AddUser(user));
        }

        /// <summary>Adds the user user have empty CNP return false.</summary>
        [TestMethod]
        public void AddUser_UserHaveEmptyCNP_ReturnFalse()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi",
                Age = 23,
                Email = "Silvia.Brassoi@yahoo.com",
                Password = "Silvia",
                RoleStatus = 1,
                Gender = "M",
                CNP = string.Empty,
            };

            Assert.IsFalse(userRepositoryMock.AddUser(user));
        }

        /// <summary>Adds the user user have smaller CNP return false.</summary>
        [TestMethod]
        public void AddUser_UserHaveSmallerCNP_ReturnFalse()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi",
                Age = 23,
                Email = "Silvia.Brassoi@yahoo.com",
                Password = "Silvia",
                RoleStatus = 1,
                Gender = "M",
                CNP = "123",
            };

            Assert.IsFalse(userRepositoryMock.AddUser(user));
        }

        /// <summary>Adds the user user have longer CNP return false.</summary>
        [TestMethod]
        public void AddUser_UserHaveLongerCNP_ReturnFalse()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi",
                Age = 23,
                Email = "Silvia.Brassoi@yahoo.com",
                Password = "Silvia",
                RoleStatus = 1,
                Gender = "M",
                CNP = "123123123123123123",
            };

            Assert.IsFalse(userRepositoryMock.AddUser(user));
        }

        /// <summary>Adds the user user have not digit CNP return false.</summary>
        [TestMethod]
        public void AddUser_UserHaveNotDigitCNP_ReturnFalse()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi",
                Age = 23,
                Email = "Silvia.Brassoi@yahoo.com",
                Password = "Silvia",
                RoleStatus = 1,
                Gender = "M",
                CNP = "abecedardaswd",
            };

            Assert.IsFalse(userRepositoryMock.AddUser(user));
        }

        /// <summary>Adds the user user have digit and chars gender return false.</summary>
        [TestMethod]
        public void AddUser_UserHaveDigitAndCharsGender_ReturnFalse()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi",
                Age = 23,
                Email = "Silvia.Brassoi@yahoo.com",
                Password = "Silvia",
                RoleStatus = 1,
                Gender = "M",
                CNP = "199060208232d",
            };

            Assert.IsFalse(userRepositoryMock.AddUser(user));
        }

        /// <summary>Adds the user user have symbol CNP return false.</summary>
        [TestMethod]
        public void AddUser_UserHaveSymbolCNP_ReturnFalse()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi",
                Age = 23,
                Email = "Silvia.Brassoi@yahoo.com",
                Password = "Silvia",
                RoleStatus = 1,
                Gender = "M",
                CNP = "199060208232%",
            };

            Assert.IsFalse(userRepositoryMock.AddUser(user));
        }

        /// <summary>Adds the user user have male gender and wrong CNP return false.</summary>
        [TestMethod]
        public void AddUser_UserHaveMaleGenderAndWrongCNP_ReturnFalse()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi",
                Age = 23,
                Email = "Silvia.Brassoi@yahoo.com",
                Password = "Silvia",
                RoleStatus = 1,
                Gender = "M",
                CNP = "2990602082231",
            };

            Assert.IsFalse(userRepositoryMock.AddUser(user));
        }

        /// <summary>Adds the user user have female gender and wrong CNP return false.</summary>
        [TestMethod]
        public void AddUser_UserHaveFemaleGenderAndWrongCNP_ReturnFalse()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi",
                Age = 23,
                Email = "Silvia.Brassoi@yahoo.com",
                Password = "Silvia",
                RoleStatus = 1,
                Gender = "F",
                CNP = "1990602082323",
            };

            Assert.IsFalse(userRepositoryMock.AddUser(user));
        }

        /// <summary>Adds the user user have null address return false.</summary>
        [TestMethod]
        public void AddUser_UserHaveNullAddress_ReturnFalse()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi",
                Age = 23,
                Email = "Silvia.Brassoi@yahoo.com",
                Password = "Silvia",
                RoleStatus = 1,
                Gender = "M",
                CNP = "1990602082223",
            };

            Assert.IsFalse(userRepositoryMock.AddUser(user));
        }

        /// <summary>Adds the user user have empty address return false.</summary>
        [TestMethod]
        public void AddUser_UserHaveEmptyAddress_ReturnFalse()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi",
                Age = 23,
                Email = "Silvia.Brassoi@yahoo.com",
                Password = "Silvia",
                RoleStatus = 1,
                Gender = "M",
                CNP = "1990602082223",
                Adress = string.Empty,
            };

            Assert.IsFalse(userRepositoryMock.AddUser(user));
        }

        /// <summary>Adds the user user have smaller address return false.</summary>
        [TestMethod]
        public void AddUser_UserHaveSmallerAddress_ReturnFalse()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi",
                Age = 23,
                Email = "Silvia.Brassoi@yahoo.com",
                Password = "Silvia",
                RoleStatus = 1,
                Gender = "M",
                CNP = "1990602082223",
                Adress = "ab",
            };

            Assert.IsFalse(userRepositoryMock.AddUser(user));
        }

        /// <summary>Adds the user user have longer address return false.</summary>
        [TestMethod]
        public void AddUser_UserHaveLongerAddress_ReturnFalse()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi",
                Age = 23,
                Email = "Silvia.Brassoi@yahoo.com",
                Password = "Silvia",
                RoleStatus = 1,
                Gender = "M",
                CNP = "1990602082223",
                Adress = "abbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb",
            };

            Assert.IsFalse(userRepositoryMock.AddUser(user));
        }

        /// <summary>Adds the user user have lower address return false.</summary>
        [TestMethod]
        public void AddUser_UserHaveLowerAddress_ReturnFalse()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi",
                Age = 23,
                Email = "Silvia.Brassoi@yahoo.com",
                Password = "Silvia",
                RoleStatus = 1,
                Gender = "M",
                CNP = "1990602082223",
                Adress = "mandra nr 61",
            };

            Assert.IsFalse(userRepositoryMock.AddUser(user));
        }

        /// <summary>Adds the user user have symbol address return false.</summary>
        [TestMethod]
        public void AddUser_UserHaveSymbolAddress_ReturnFalse()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi",
                Age = 23,
                Email = "Silvia.Brassoi@yahoo.com",
                Password = "Silvia",
                RoleStatus = 1,
                Gender = "M",
                CNP = "1990602082223",
                Adress = "Mandra nr. 61 @2",
            };

            Assert.IsFalse(userRepositoryMock.AddUser(user));
        }

        /// <summary>Adds the user user have null phone return false.</summary>
        [TestMethod]
        public void AddUser_UserHaveNullPhone_ReturnFalse()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi",
                Age = 23,
                Email = "Silvia.Brassoi@yahoo.com",
                Password = "Silvia",
                RoleStatus = 1,
                Gender = "M",
                CNP = "1990602082223",
                Adress = "Mandra nr. 61",
            };

            Assert.IsFalse(userRepositoryMock.AddUser(user));
        }

        /// <summary>Adds the user user have empty phone return false.</summary>
        [TestMethod]
        public void AddUser_UserHaveEmptyPhone_ReturnFalse()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi",
                Age = 23,
                Email = "Silvia.Brassoi@yahoo.com",
                Password = "Silvia",
                RoleStatus = 1,
                Gender = "M",
                CNP = "1990602082223",
                Adress = "Mandra nr. 61",
                Phone = string.Empty,
            };

            Assert.IsFalse(userRepositoryMock.AddUser(user));
        }

        /// <summary>Adds the user user have smaller phone return false.</summary>
        [TestMethod]
        public void AddUser_UserHaveSmallerPhone_ReturnFalse()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi",
                Age = 23,
                Email = "Silvia.Brassoi@yahoo.com",
                Password = "Silvia",
                RoleStatus = 1,
                Gender = "M",
                CNP = "1990602082223",
                Adress = "Mandra nr. 61",
                Phone = "0731",
            };

            Assert.IsFalse(userRepositoryMock.AddUser(user));
        }

        /// <summary>Adds the user user have longer phone return false.</summary>
        [TestMethod]
        public void AddUser_UserHaveLongerPhone_ReturnFalse()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi",
                Age = 23,
                Email = "Silvia.Brassoi@yahoo.com",
                Password = "Silvia",
                RoleStatus = 1,
                Gender = "M",
                CNP = "1990602082223",
                Adress = "Mandra nr. 61",
                Phone = "0731231231231231",
            };

            Assert.IsFalse(userRepositoryMock.AddUser(user));
        }

        /// <summary>Adds the user user have not digit phone return false.</summary>
        [TestMethod]
        public void AddUser_UserHaveNotDigitPhone_ReturnFalse()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi",
                Age = 23,
                Email = "Silvia.Brassoi@yahoo.com",
                Password = "Silvia",
                RoleStatus = 1,
                Gender = "M",
                CNP = "1990602082223",
                Adress = "Mandra nr. 61",
                Phone = "abscdefert",
            };

            Assert.IsFalse(userRepositoryMock.AddUser(user));
        }

        /// <summary>Adds the user user have digit and chars phone return false.</summary>
        [TestMethod]
        public void AddUser_UserHaveDigitAndCharsPhone_ReturnFalse()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi",
                Age = 23,
                Email = "Silvia.Brassoi@yahoo.com",
                Password = "Silvia",
                RoleStatus = 1,
                Gender = "M",
                CNP = "1990602082223",
                Adress = "Mandra nr. 61",
                Phone = "074123adsd",
            };

            Assert.IsFalse(userRepositoryMock.AddUser(user));
        }

        /// <summary>Adds the user user have symbol phone return false.</summary>
        [TestMethod]
        public void AddUser_UserHaveSymbolPhone_ReturnFalse()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi",
                Age = 23,
                Email = "Silvia.Brassoi@yahoo.com",
                Password = "Silvia",
                RoleStatus = 1,
                Gender = "M",
                CNP = "1990602082223",
                Adress = "Mandra nr. 61",
                Phone = "076195631@",
            };

            Assert.IsFalse(userRepositoryMock.AddUser(user));
        }

        /// <summary>Logs the out user is logged in set user active to false.</summary>
        [TestMethod]
        public void LogOut_UserIsLoggedIn_SetUserActiveToFalse()
        {
            auctionMock.Roles.Add(new Role { ID = 1, RoleName = "Bidder" });

            startupApplication.Register(
                new User
                {
                    FirstName = "Silvia",
                    LastName = "Brassoi",
                    Age = 23,
                    Email = "Silvia.Brassoi@yahoo.com",
                    Password = "Silvia",
                    RoleStatus = 1,
                    Gender = "M",
                    CNP = "1123456789123",
                    Adress = "Mandra nr. 61",
                    Phone = "0712345678",
                    Active = false
                });

            startupApplication.LogIn("Silvia.Brassoi@yahoo.com", "Silvia", 1);

            var currentUser = auctionMock.Users
                .FirstOrDefault(x => x.Email == "Silvia.Brassoi@yahoo.com");

            Assert.IsTrue(currentUser.Active);

            userRepositoryMock.LogOut();

            Assert.IsTrue(currentUser.Active == false);
        }

        /// <summary>Logs the out user is logged out expected exception.</summary>
        [TestMethod]
        [ExpectedException(typeof(UserIsLoggedOutException))]
        public void LogOut_UserIsLoggedOut_ExpectedException()
        {
            userRepositoryMock.LogOut();
        }

        /// <summary>Gets the active user user is logged in get user.</summary>
        [TestMethod]
        public void GetActiveUser_UserIsLoggedIn_GetUser()
        {
            auctionMock.Roles.Add(new Role { ID = 1, RoleName = "Bidder" });

            User user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi",
                Age = 23,
                Email = "Silvia.Brassoi@yahoo.com",
                Password = "Silvia",
                RoleStatus = 2,
                Gender = "M",
                CNP = "1123456789123",
                Adress = "Mandra nr. 61",
                Phone = "0712345678",
                Active = false
            };

            startupApplication.Register(user);

            startupApplication.LogIn("Silvia.Brassoi@yahoo.com", "Silvia", 2);

            Assert.IsTrue(user == userRepositoryMock.GetActiveUser());
        }

        /// <summary>Changes the user score when change score expected exception.</summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidProductException))]
        public void ChangeUserScore_WhenChangeScore_ExpectedException()
        {
            auctionMock.Roles.Add(new Role { ID = 1, RoleName = "Bidder" });

            auctionMock.Users.Add(new User
            {
                ID = 1,
                FirstName = "Silvia",
                LastName = "Brassoi",
                Age = 23,
                Email = "Silvia.Brassoi@yahoo.com",
                Password = "Silvia",
                RoleStatus = 2,
                Active = true
            });

            userRepositoryMock.ChangeUserScore(1);
        }

        /// <summary>Changes the user score when score changed user get banned.</summary>
        [TestMethod]
        public void ChangeUserScore_WhenScoreChanged_UserGetBanned()
        {
            auctionMock.Roles.Add(new Role { ID = 1, RoleName = "Bidder" });

            startupApplication.Register(new User
            {
                ID = 1,
                FirstName = "Silvia",
                LastName = "Brassoi",
                Age = 23,
                Email = "Silvia.Brassoi@yahoo.com",
                Password = "Silvia",
                RoleStatus = 2,
                Gender = "M",
                CNP = "1123456789123",
                Adress = "Mandra nr. 61",
                Phone = "0762275533",
                Score = 4.53,
                Active = false
            });

            startupApplication.Register(new User
            {
                ID = 2,
                FirstName = "Cata",
                LastName = "Brassoi",
                Email = "Cata.Brassoi@yahoo.com",
                Password = "Silvia",
                Age = 23,
                Gender = "M",
                CNP = "1123456789123",
                Adress = "Mandra nr. 61",
                Phone = "0762275533",
                RoleStatus = 2,
                Active = true
            });

            auctionMock.Products.Add(new Product
            {
                ID = 1,
                Name = "Photo Camera",
                Description = "Very good quality.",
                IDState = 1,
                IDUser = 1,
                IDCategory = 1,
                StartDate = DateTime.Now.AddDays(-6),
                EndDate = DateTime.Now.AddDays(-5),
                StartPrice = 25.34,
                EndPrice = 27.56,
                AuctedUser = 2,
                Score = 4.23,
                Active = false,
                Specification = "Works nice outside.",
                Coin = "RON"
            });

            var user = auctionMock.Users
                .SingleOrDefault(x => x.ID == 1);

            Assert.IsTrue(user.BannedTime == null);

            userRepositoryMock.ChangeUserScore(1);

            user = auctionMock.Users
                .SingleOrDefault(x => x.ID == 1);

            Assert.IsTrue(user?.BannedTime != null);
        }
    }
}
