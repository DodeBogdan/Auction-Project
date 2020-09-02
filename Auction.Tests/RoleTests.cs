//-----------------------------------------------------------------------
// <copyright file="RoleTests.cs" company="Transilvania University of Brasov">
//     Copyright (c) Bogdan Gheorghe Nicolae. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Auction.Tests
{
    using System.Linq;
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

        /// <summary>Adds the role valid role return true.</summary>
        [TestMethod]
        public void AddRole_ValidRole_ReturnTrue()
        {
            var role = new Role
            {
                RoleName = "Bidder"
            };

            Assert.IsTrue(roleRepository.AddRole(role));
        }

        /// <summary>Adds the role when role is null return false.</summary>
        [TestMethod]
        public void AddRole_WhenRoleIsNull_ReturnFalse()
        {
            Assert.IsFalse(roleRepository.AddRole(null));
        }

        /// <summary>Adds the role role have null name return false.</summary>
        [TestMethod]
        public void AddRole_RoleHaveNullName_ReturnFalse()
        {
            var role = new Role
            {
                RoleName = null
            };

            Assert.IsFalse(roleRepository.AddRole(role));
        }

        /// <summary>Adds the role role have empty name return false.</summary>
        [TestMethod]
        public void AddRole_RoleHaveEmptyName_ReturnFalse()
        {
            var role = new Role
            {
                RoleName = string.Empty
            };

            Assert.IsFalse(roleRepository.AddRole(role));
        }

        /// <summary>Adds the role role have smaller name return false.</summary>
        [TestMethod]
        public void AddRole_RoleHaveSmallerName_ReturnFalse()
        {
            var role = new Role
            {
                RoleName = "bi"
            };

            Assert.IsFalse(roleRepository.AddRole(role));
        }

        /// <summary>Adds the role role have longer name return false.</summary>
        [TestMethod]
        public void AddRole_RoleHaveLongerName_ReturnFalse()
        {
            var role = new Role
            {
                RoleName = "Bideeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee"
            };

            Assert.IsFalse(roleRepository.AddRole(role));
        }

        /// <summary>Adds the role role have lower name return false.</summary>
        [TestMethod]
        public void AddRole_RoleHaveLowerName_ReturnFalse()
        {
            var role = new Role
            {
                RoleName = "bidder"
            };

            Assert.IsFalse(roleRepository.AddRole(role));
        }

        /// <summary>Adds the role role have digit name return false.</summary>
        [TestMethod]
        public void AddRole_RoleHaveDigitName_ReturnFalse()
        {
            var role = new Role
            {
                RoleName = "Bidder1"
            };

            Assert.IsFalse(roleRepository.AddRole(role));
        }

        /// <summary>Adds the role role have symbol name return false.</summary>
        [TestMethod]
        public void AddRole_RoleHaveSymbolName_ReturnFalse()
        {
            var role = new Role
            {
                RoleName = "Bidder@#"
            };

            Assert.IsFalse(roleRepository.AddRole(role));
        }
    }
}
