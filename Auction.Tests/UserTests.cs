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

        /// <summary>Adds the user valid user insert user.</summary>
        [TestMethod]
        public void AddUser_ValidUser_InsertUser()
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

        /// <summary>Adds the user when user is null expected exception.</summary>
        [TestMethod]
        public void AddUser_WhenUserIsNull_ExpectedException()
        {
            try
            {
                userRepositoryMock.AddUser(null);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestUser - user can not be null.", ex.Message);
            }
        }

        /// <summary>Adds the user user have null first name expected exception.</summary>
        [TestMethod]
        public void AddUser_UserHaveNullFirstName_ExpectedException()
        {
            var user = new User
            {
                FirstName = null,
            };

            try
            {
                userRepositoryMock.AddUser(user);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestUser - user first name can not be null.", ex.Message);
            }
        }

        /// <summary>Adds the user user have empty first name expected exception.</summary>
        [TestMethod]
        public void AddUser_UserHaveEmptyFirstName_ExpectedException()
        {
            var user = new User
            {
                FirstName = string.Empty,
            };

            try
            {
                userRepositoryMock.AddUser(user);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestUser - user first name can not be empty.", ex.Message);
            }
        }

        /// <summary>Adds the user user have smaller first name expected exception.</summary>
        [TestMethod]
        public void AddUser_UserHaveSmallerFirstName_ExpectedException()
        {
            var user = new User
            {
                FirstName = "Ss",
            };

            try
            {
                userRepositoryMock.AddUser(user);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestUser - user first name have invalid length.", ex.Message);
            }
        }

        /// <summary>Adds the user user have longer first name expected exception.</summary>
        [TestMethod]
        public void AddUser_UserHaveLongerFirstName_ExpectedException()
        {
            var user = new User
            {
                FirstName = "Ssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss",
            };

            try
            {
                userRepositoryMock.AddUser(user);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestUser - user first name have invalid length.", ex.Message);
            }
        }

        /// <summary>Adds the user user have lower first name expected exception.</summary>
        [TestMethod]
        public void AddUser_UserHaveLowerFirstName_ExpectedException()
        {
            var user = new User
            {
                FirstName = "silvia",
            };

            try
            {
                userRepositoryMock.AddUser(user);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestUser - user first name can not start with lower character.", ex.Message);
            }
        }

        /// <summary>Adds the user user have digit first name expected exception.</summary>
        [TestMethod]
        public void AddUser_UserHaveDigitFirstName_ExpectedException()
        {
            var user = new User
            {
                FirstName = "Silvia1",
            };

            try
            {
                userRepositoryMock.AddUser(user);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestUser - user first name can not contain signs or digits.", ex.Message);
            }
        }

        /// <summary>Adds the user user have symbol first name expected exception.</summary>
        [TestMethod]
        public void AddUser_UserHaveSymbolFirstName_ExpectedException()
        {
            var user = new User
            {
                FirstName = "Silvia@&^",
            };

            try
            {
                userRepositoryMock.AddUser(user);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestUser - user first name can not contain signs or digits.", ex.Message);
            }
        }

        /// <summary>Adds the user user have null last name expected exception.</summary>
        [TestMethod]
        public void AddUser_UserHaveNullLastName_ExpectedException()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = null,
            };

            try
            {
                userRepositoryMock.AddUser(user);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestUser - user last name can not be null.", ex.Message);
            }
        }

        /// <summary>Adds the user user have empty last name expected exception.</summary>
        [TestMethod]
        public void AddUser_UserHaveEmptyLastName_ExpectedException()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = string.Empty,
            };

            try
            {
                userRepositoryMock.AddUser(user);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestUser - user first last can not be empty.", ex.Message);
            }
        }

        /// <summary>Adds the user user have smaller last name expected exception.</summary>
        [TestMethod]
        public void AddUser_UserHaveSmallerLastName_ExpectedException()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Br",
            };

            try
            {
                userRepositoryMock.AddUser(user);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestUser - user last name have invalid length.", ex.Message);
            }
        }

        /// <summary>Adds the user user have longer last name expected exception.</summary>
        [TestMethod]
        public void AddUser_UserHaveLongerLastName_ExpectedException()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrassoi",
            };

            try
            {
                userRepositoryMock.AddUser(user);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestUser - user last name have invalid length.", ex.Message);
            }
        }

        /// <summary>Adds the user user have lower last name expected exception.</summary>
        [TestMethod]
        public void AddUser_UserHaveLowerLastName_ExpectedException()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "brassoi",
            };

            try
            {
                userRepositoryMock.AddUser(user);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestUser - user last name can not start with lower character.", ex.Message);
            }
        }

        /// <summary>Adds the user user have digit last name expected exception.</summary>
        [TestMethod]
        public void AddUser_UserHaveDigitLastName_ExpectedException()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi1",
            };

            try
            {
                userRepositoryMock.AddUser(user);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestUser - user last name can not contain signs or digits.", ex.Message);
            }
        }

        /// <summary>Adds the user user have symbol last name expected exception.</summary>
        [TestMethod]
        public void AddUser_UserHaveSymbolLastName_ExpectedException()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi@&^",
            };

            try
            {
                userRepositoryMock.AddUser(user);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestUser - user last name can not contain signs or digits.", ex.Message);
            }
        }

        /// <summary>Adds the user user have smaller age expected exception.</summary>
        [TestMethod]
        public void AddUser_UserHaveSmallerAge_ExpectedException()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi",
                Age = 17,
            };

            try
            {
                userRepositoryMock.AddUser(user);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestUser - user have invalid age.", ex.Message);
            }
        }

        /// <summary>Adds the user user have higher age expected exception.</summary>
        [TestMethod]
        public void AddUser_UserHaveHigherAge_ExpectedException()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi",
                Age = 127,
            };

            try
            {
                userRepositoryMock.AddUser(user);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestUser - user have invalid age.", ex.Message);
            }
        }

        /// <summary>Adds the user user have smaller email expected exception.</summary>
        [TestMethod]
        public void AddUser_UserHaveSmallerEmail_ExpectedException()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi",
                Age = 23,
                Email = "si",
            };

            try
            {
                userRepositoryMock.AddUser(user);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestUser - user email is invalid.", ex.Message);
            }
        }

        /// <summary>Adds the user user have longer email expected exception.</summary>
        [TestMethod]
        public void AddUser_UserHaveLongerEmail_ExpectedException()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi",
                Age = 23,
                // ReSharper disable once StringLiteralTypo
                Email = "sidsasdasdasdasdasdasdasdasdasdasdasdasdasddddddddddddddddddddddddddssssssssssssssssssssssssssssss",
            };

            try
            {
                userRepositoryMock.AddUser(user);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestUser - user email is invalid.", ex.Message);
            }
        }

        /// <summary>Adds the user user have invalid email expected exception.</summary>
        [TestMethod]
        public void AddUser_UserHaveInvalidEmail_ExpectedException()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi",
                Age = 23,
                Email = "silvia.brassoi",
            };

            try
            {
                userRepositoryMock.AddUser(user);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestUser - user email is invalid.", ex.Message);
            }
        }

        /// <summary>Adds the user user have null role status expected exception.</summary>
        [TestMethod]
        public void AddUser_UserHaveNullRoleStatus_ExpectedException()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi",
                Age = 23,
                Email = "Silvia.Brassoi@yahoo.com",
                Password = "Silvia"
            };

            try
            {
                userRepositoryMock.AddUser(user);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestUser - user role can not be null.", ex.Message);
            }
        }

        /// <summary>Adds the user user have wrong role status expected exception.</summary>
        [TestMethod]
        public void AddUser_UserHaveWrongRoleStatus_ExpectedException()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi",
                Age = 23,
                Email = "Silvia.Brassoi@yahoo.com",
                Password = "Silvia",
                RoleStatus = 3,
            };

            try
            {
                userRepositoryMock.AddUser(user);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestUser - user have invalid role.", ex.Message);
            }
        }

        /// <summary>Adds the user user have null password expected exception.</summary>
        [TestMethod]
        public void AddUser_UserHaveNullPassword_ExpectedException()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi",
                Age = 23,
                Email = "Silvia.Brassoi@yahoo.com",
            };

            try
            {
                userRepositoryMock.AddUser(user);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestUser - user password can not be null.", ex.Message);
            }
        }

        /// <summary>Adds the user user have empty password expected exception.</summary>
        [TestMethod]
        public void AddUser_UserHaveEmptyPassword_ExpectedException()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi",
                Age = 23,
                Email = "Silvia.Brassoi@yahoo.com",
                Password = string.Empty,
            };

            try
            {
                userRepositoryMock.AddUser(user);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestUser - user password can not be empty.", ex.Message);
            }
        }

        /// <summary>Adds the user user have smaller password expected exception.</summary>
        [TestMethod]
        public void AddUser_UserHaveSmallerPassword_ExpectedException()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi",
                Age = 23,
                Email = "Silvia.Brassoi@yahoo.com",
                Password = "p",
            };

            try
            {
                userRepositoryMock.AddUser(user);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestUser - user password have invalid length.", ex.Message);
            }
        }

        /// <summary>Adds the user user have longer password expected exception.</summary>
        [TestMethod]
        public void AddUser_UserHaveLongerPassword_ExpectedException()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi",
                Age = 23,
                Email = "Silvia.Brassoi@yahoo.com",
                // ReSharper disable once StringLiteralTypo
                Password = "ppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppp",
            };

            try
            {
                userRepositoryMock.AddUser(user);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestUser - user password have invalid length.", ex.Message);
            }
        }

        /// <summary>Adds the user user have lower password expected exception.</summary>
        [TestMethod]
        public void AddUser_UserHaveLowerPassword_ExpectedException()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi",
                Age = 23,
                Email = "Silvia.Brassoi@yahoo.com",
                Password = "silvia",
            };

            try
            {
                userRepositoryMock.AddUser(user);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestUser - user password can not start with lower character.", ex.Message);
            }
        }

        /// <summary>Adds the user user have null gender expected exception.</summary>
        [TestMethod]
        public void AddUser_UserHaveNullGender_ExpectedException()
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

            try
            {
                userRepositoryMock.AddUser(user);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestUser - user gender can not be null.", ex.Message);
            }
        }

        /// <summary>Adds the user user have empty gender expected exception.</summary>
        [TestMethod]
        public void AddUser_UserHaveEmptyGender_ExpectedException()
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

            try
            {
                userRepositoryMock.AddUser(user);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestUser - user gender can not be empty.", ex.Message);
            }
        }

        /// <summary>Adds the user user have smaller gender expected exception.</summary>
        [TestMethod]
        public void AddUser_UserHaveSmallerGender_ExpectedException()
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

            try
            {
                userRepositoryMock.AddUser(user);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestUser - user gender can not be empty.", ex.Message);
            }
        }

        /// <summary>Adds the user user have longer first name expected exception.</summary>
        [TestMethod]
        public void AddUser_UserHaveLongerGender_ExpectedException()
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

            try
            {
                userRepositoryMock.AddUser(user);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestUser - user gender have invalid length.", ex.Message);
            }
        }

        /// <summary>Adds the user user have lower gender expected exception.</summary>
        [TestMethod]
        public void AddUser_UserHaveLowerGender_ExpectedException()
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

            try
            {
                userRepositoryMock.AddUser(user);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestUser - user gender can not start with lower character.", ex.Message);
            }
        }

        /// <summary>Adds the user user have digit gender expected exception.</summary>
        [TestMethod]
        public void AddUser_UserHaveDigitGender_ExpectedException()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi",
                Age = 23,
                Email = "Silvia.Brassoi@yahoo.com",
                Password = "Silvia",
                RoleStatus = 1,
                Gender = "1",
            };

            try
            {
                userRepositoryMock.AddUser(user);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestUser - user have invalid gender.", ex.Message);
            }
        }

        /// <summary>Adds the user user have symbol gender expected exception.</summary>
        [TestMethod]
        public void AddUser_UserHaveSymbolGender_ExpectedException()
        {
            var user = new User
            {
                FirstName = "Silvia",
                LastName = "Brassoi",
                Age = 23,
                Email = "Silvia.Brassoi@yahoo.com",
                Password = "Silvia",
                RoleStatus = 1,
                Gender = "$",
            };

            try
            {
                userRepositoryMock.AddUser(user);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestUser - user have invalid gender.", ex.Message);
            }
        }

        /// <summary>Adds the user user have null CNP expected exception.</summary>
        [TestMethod]
        public void AddUser_UserHaveNullCNP_ExpectedException()
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

            try
            {
                userRepositoryMock.AddUser(user);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestUser - user cnp can not be null.", ex.Message);
            }
        }

        /// <summary>Adds the user user have empty CNP expected exception.</summary>
        [TestMethod]
        public void AddUser_UserHaveEmptyCNP_ExpectedException()
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

            try
            {
                userRepositoryMock.AddUser(user);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestUser - user cnp have invalid length.", ex.Message);
            }
        }

        /// <summary>Adds the user user have smaller CNP expected exception.</summary>
        [TestMethod]
        public void AddUser_UserHaveSmallerCNP_ExpectedException()
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

            try
            {
                userRepositoryMock.AddUser(user);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestUser - user cnp have invalid length.", ex.Message);
            }
        }

        /// <summary>Adds the user user have longer CNP expected exception.</summary>
        [TestMethod]
        public void AddUser_UserHaveLongerCNP_ExpectedException()
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

            try
            {
                userRepositoryMock.AddUser(user);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestUser - user cnp have invalid length.", ex.Message);
            }
        }

        /// <summary>Adds the user user have not digit CNP expected exception.</summary>
        [TestMethod]
        public void AddUser_UserHaveNotDigitCNP_ExpectedException()
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
                // ReSharper disable once StringLiteralTypo
                CNP = "abecedardaswd",
            };

            try
            {
                userRepositoryMock.AddUser(user);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestUser - user have invalid cnp.", ex.Message);
            }
        }

        /// <summary>Adds the user user have digit and chars gender expected exception.</summary>
        [TestMethod]
        public void AddUser_UserHaveDigitAndCharsGender_ExpectedException()
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

            try
            {
                userRepositoryMock.AddUser(user);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestUser - user have invalid cnp.", ex.Message);
            }
        }

        /// <summary>Adds the user user have symbol CNP expected exception.</summary>
        [TestMethod]
        public void AddUser_UserHaveSymbolCNP_ExpectedException()
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

            try
            {
                userRepositoryMock.AddUser(user);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestUser - user have invalid cnp.", ex.Message);
            }
        }

        /// <summary>Adds the user user have male gender and wrong CNP expected exception.</summary>
        [TestMethod]
        public void AddUser_UserHaveMaleGenderAndWrongCNP_ExpectedException()
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

            try
            {
                userRepositoryMock.AddUser(user);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestUser - user have invalid cnp.", ex.Message);
            }
        }

        /// <summary>Adds the user user have female gender and wrong CNP expected exception.</summary>
        [TestMethod]
        public void AddUser_UserHaveFemaleGenderAndWrongCNP_ExpectedException()
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

            try
            {
                userRepositoryMock.AddUser(user);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestUser - user have invalid cnp.", ex.Message);
            }
        }

        /// <summary>Adds the user user have null address expected exception.</summary>
        [TestMethod]
        public void AddUser_UserHaveNullAddress_ExpectedException()
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

            try
            {
                userRepositoryMock.AddUser(user);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestUser - user address can not be null.", ex.Message);
            }
        }

        /// <summary>Adds the user user have empty address expected exception.</summary>
        [TestMethod]
        public void AddUser_UserHaveEmptyAddress_ExpectedException()
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

            try
            {
                userRepositoryMock.AddUser(user);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestUser - user address can not be empty.", ex.Message);
            }
        }

        /// <summary>Adds the user user have smaller address expected exception.</summary>
        [TestMethod]
        public void AddUser_UserHaveSmallerAddress_ExpectedException()
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

            try
            {
                userRepositoryMock.AddUser(user);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestUser - user address have invalid length.", ex.Message);
            }
        }

        /// <summary>Adds the user user have longer address expected exception.</summary>
        [TestMethod]
        public void AddUser_UserHaveLongerAddress_ExpectedException()
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
                // ReSharper disable once StringLiteralTypo
                Adress = "abbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb",
            };

            try
            {
                userRepositoryMock.AddUser(user);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestUser - user address have invalid length.", ex.Message);
            }
        }

        /// <summary>Adds the user user have lower address expected exception.</summary>
        [TestMethod]
        public void AddUser_UserHaveLowerAddress_ExpectedException()
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

            try
            {
                userRepositoryMock.AddUser(user);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestUser - user address can not start with lower character.", ex.Message);
            }
        }

        /// <summary>Adds the user user have symbol address expected exception.</summary>
        [TestMethod]
        public void AddUser_UserHaveSymbolAddress_ExpectedException()
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

            try
            {
                userRepositoryMock.AddUser(user);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestUser - user have invalid address.", ex.Message);
            }
        }

        /// <summary>Adds the user user have null phone expected exception.</summary>
        [TestMethod]
        public void AddUser_UserHaveNullPhone_ExpectedException()
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

            try
            {
                userRepositoryMock.AddUser(user);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestUser - user phone can not be null.", ex.Message);
            }
        }

        /// <summary>Adds the user user have empty phone expected exception.</summary>
        [TestMethod]
        public void AddUser_UserHaveEmptyPhone_ExpectedException()
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

            try
            {
                userRepositoryMock.AddUser(user);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestUser - user phone have invalid length.", ex.Message);
            }
        }

        /// <summary>Adds the user user have smaller phone expected exception.</summary>
        [TestMethod]
        public void AddUser_UserHaveSmallerPhone_ExpectedException()
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

            try
            {
                userRepositoryMock.AddUser(user);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestUser - user phone have invalid length.", ex.Message);
            }
        }

        /// <summary>Adds the user user have longer phone expected exception.</summary>
        [TestMethod]
        public void AddUser_UserHaveLongerPhone_ExpectedException()
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

            try
            {
                userRepositoryMock.AddUser(user);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestUser - user phone have invalid length.", ex.Message);
            }
        }

        /// <summary>Adds the user user have not digit phone expected exception.</summary>
        [TestMethod]
        public void AddUser_UserHaveNotDigitPhone_ExpectedException()
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
                // ReSharper disable once StringLiteralTypo
                Phone = "abscdefert",
            };

            try
            {
                userRepositoryMock.AddUser(user);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestUser - user have invalid phone.", ex.Message);
            }
        }

        /// <summary>Adds the user user have digit and chars phone expected exception.</summary>
        [TestMethod]
        public void AddUser_UserHaveDigitAndCharsPhone_ExpectedException()
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

            try
            {
                userRepositoryMock.AddUser(user);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestUser - user have invalid phone.", ex.Message);
            }
        }

        /// <summary>Adds the user user have symbol phone expected exception.</summary>
        [TestMethod]
        public void AddUser_UserHaveSymbolPhone_ExpectedException()
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

            try
            {
                userRepositoryMock.AddUser(user);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestUser - user have invalid phone.", ex.Message);
            }
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

            Assert.IsTrue(currentUser != null && currentUser.Active);

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

            Assert.IsTrue(user != null);

            Assert.IsTrue(user.BannedTime == null);

            userRepositoryMock.ChangeUserScore(1);

            user = auctionMock.Users
                .SingleOrDefault(x => x.ID == 1);

            Assert.IsTrue(user?.BannedTime != null);
        }
    }
}
