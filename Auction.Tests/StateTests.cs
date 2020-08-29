//-----------------------------------------------------------------------
// <copyright file="StateTests.cs" company="Transilvania University of Brasov">
//     Copyright (c) Brassoi Silvia Maria. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Auction.Tests
{
    using System.Linq;
    using AuctionLogic.Models;
    using AuctionLogic.Repositories;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Telerik.JustMock.EntityFramework;

    /// <summary>State test class</summary>
    [TestClass]
    public class StateTests
    {
        /// <summary>The auction mock</summary>
        private AuctionDB auctionMock;

        /// <summary>The state repository</summary>
        private StateRepository stateRepository;

        /// <summary>Sets up.</summary>
        [TestInitialize]
        public void SetUp()
        {
            auctionMock = EntityFrameworkMock.Create<AuctionDB>();
            stateRepository = new StateRepository(auctionMock);
        }

        /// <summary>Adds the state valid state return true.</summary>
        [TestMethod]
        public void AddState_ValidState_ReturnTrue()
        {
            var state = new State
            {
                Name = "Active"
            };

            Assert.IsTrue(stateRepository.AddState(state));
        }

        /// <summary>Adds the state when state is null return false.</summary>
        [TestMethod]
        public void AddState_WhenStateIsNull_ReturnFalse()
        {
            Assert.IsFalse(stateRepository.AddState(null));
        }

        /// <summary>Adds the state state have null name return false.</summary>
        [TestMethod]
        public void AddState_StateHaveNullName_ReturnFalse()
        {
            var state = new State
            {
                Name = null
            };

            Assert.IsFalse(stateRepository.AddState(state));
        }

        /// <summary>Adds the state state have empty name return false.</summary>
        [TestMethod]
        public void AddState_StateHaveEmptyName_ReturnFalse()
        {
            var state = new State
            {
                Name = string.Empty
            };

            Assert.IsFalse(stateRepository.AddState(state));
        }

        /// <summary>Adds the state state have smaller name return false.</summary>
        [TestMethod]
        public void AddState_StateHaveSmallerName_ReturnFalse()
        {
            var state = new State
            {
                Name = "Ac"
            };

            Assert.IsFalse(stateRepository.AddState(state));
        }

        /// <summary>Adds the state state have longer name return false.</summary>
        [TestMethod]
        public void AddState_StateHaveLongerName_ReturnFalse()
        {
            var state = new State
            {
                Name = "Activeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee"
            };

            Assert.IsFalse(stateRepository.AddState(state));
        }

        /// <summary>Adds the state state have lower name return false.</summary>
        [TestMethod]
        public void AddState_StateHaveLowerName_ReturnFalse()
        {
            var state = new State
            {
                Name = "active"
            };

            Assert.IsFalse(stateRepository.AddState(state));
        }

        /// <summary>Adds the state state have digit name return false.</summary>
        [TestMethod]
        public void AddState_StateHaveDigitName_ReturnFalse()
        {
            var state = new State
            {
                Name = "Active1"
            };

            Assert.IsFalse(stateRepository.AddState(state));
        }

        /// <summary>Adds the state state have symbol name return false.</summary>
        [TestMethod]
        public void AddState_StateHaveSymbolName_ReturnFalse()
        {
            var state = new State
            {
                Name = "Active@#"
            };

            Assert.IsFalse(stateRepository.AddState(state));
        }

        /// <summary>States the get products get list of products.</summary>
        [TestMethod]
        public void State_GetProducts_GetListOfProducts()
        {
            auctionMock.States.Add(new State { ID = 1, Name = "New" });

            State state = auctionMock.States.Single(x => x.ID == 1);

            Assert.IsTrue(state.Products.Count == 0);
        }
    }
}
