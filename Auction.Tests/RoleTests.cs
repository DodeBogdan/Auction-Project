//-----------------------------------------------------------------------
// <copyright file="RoleTests.cs" company="Transilvania University of Brasov">
//     Copyright (c) Bogdan Gheorghe Nicolae. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Auction.Tests
{
    using System;
    using AuctionLogic.Models;
    using AuctionLogic.Repositories;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Telerik.JustMock.EntityFramework;

    /// <summary>Role test class</summary>
    [TestClass]
    public class RoleTests
    {
        /// <summary>The auction mock</summary>
        private AuctionDB auctionMock;

        /// <summary>The role repository</summary>
        private RoleRepository roleRepository;

        /// <summary>Sets up.</summary>
        [TestInitialize]
        public void SetUp()
        {
            auctionMock = EntityFrameworkMock.Create<AuctionDB>();
            roleRepository = new RoleRepository(auctionMock);
        }

        /// <summary>Adds the role valid role insert role.</summary>
        [TestMethod]
        public void AddRole_ValidRole_InsertRole()
        {
            var role = new Role
            {
                RoleName = "Bidder"
            };

            roleRepository.AddRole(role);
        }

        /// <summary>Adds the role when role is null expected exception.</summary>
        [TestMethod]
        public void AddRole_WhenRoleIsNull_ExpectedException()
        {
            try
            {
                roleRepository.AddRole(null);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestRole - role can not be null.", ex.Message);
            }
        }

        /// <summary>Adds the role role have null name expected exception.</summary>
        [TestMethod]
        public void AddRole_RoleHaveNullName_ExpectedException()
        {
            var role = new Role
            {
                RoleName = null
            };

            try
            {
                roleRepository.AddRole(role);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestRole - role name can not be null.", ex.Message);
            }
        }

        /// <summary>Adds the role role have empty name expected exception.</summary>
        [TestMethod]
        public void AddRole_RoleHaveEmptyName_ExpectedException()
        {
            var role = new Role
            {
                RoleName = string.Empty
            };

            try
            {
                roleRepository.AddRole(role);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestRole - role name can not be empty.", ex.Message);
            }
        }

        /// <summary>Adds the role role have smaller name expected exception.</summary>
        [TestMethod]
        public void AddRole_RoleHaveSmallerName_ExpectedException()
        {
            var role = new Role
            {
                RoleName = "bi"
            };

            try
            {
                roleRepository.AddRole(role);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestRole - role name have invalid length.", ex.Message);
            }
        }

        /// <summary>Adds the role role have longer name expected exception.</summary>
        [TestMethod]
        public void AddRole_RoleHaveLongerName_ExpectedException()
        {
            var role = new Role
            {
                // ReSharper disable once StringLiteralTypo
                RoleName = "Bideeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee"
            };

            try
            {
                roleRepository.AddRole(role);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestRole - role name have invalid length.", ex.Message);
            }
        }

        /// <summary>Adds the role role have lower name expected exception.</summary>
        [TestMethod]
        public void AddRole_RoleHaveLowerName_ExpectedException()
        {
            var role = new Role
            {
                RoleName = "bidder"
            };

            try
            {
                roleRepository.AddRole(role);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestRole - role name can not start with lower character.", ex.Message);
            }
        }

        /// <summary>Adds the role role have digit name expected exception.</summary>
        [TestMethod]
        public void AddRole_RoleHaveDigitName_ExpectedException()
        {
            var role = new Role
            {
                RoleName = "Bidder1"
            };

            try
            {
                roleRepository.AddRole(role);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestRole - role name can not contain signs or digits.", ex.Message);
            }
        }

        /// <summary>Adds the role role have symbol name expected exception.</summary>
        [TestMethod]
        public void AddRole_RoleHaveSymbolName_ExpectedException()
        {
            var role = new Role
            {
                RoleName = "Bidder@#"
            };

            try
            {
                roleRepository.AddRole(role);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestRole - role name can not contain signs or digits.", ex.Message);
            }
        }
    }
}
