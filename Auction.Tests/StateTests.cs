//-----------------------------------------------------------------------
// <copyright file="StateTests.cs" company="Transilvania University of Brasov">
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

            stateRepository.AddState(state);
        }

        /// <summary>Adds the state when state is null return false.</summary>
        [TestMethod]
        public void AddState_WhenStateIsNull_ReturnFalse()
        {
            try
            {
                stateRepository.AddState(null);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestState - state can not be null.", ex.Message);
            }
        }

        /// <summary>Adds the state state have null name return false.</summary>
        [TestMethod]
        public void AddState_StateHaveNullName_ReturnFalse()
        {
            var state = new State
            {
                Name = null
            };

            try
            {
                stateRepository.AddState(state);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestState - state name can not be null.", ex.Message);
            }
        }

        /// <summary>Adds the state state have empty name return false.</summary>
        [TestMethod]
        public void AddState_StateHaveEmptyName_ReturnFalse()
        {
            var state = new State
            {
                Name = string.Empty
            };

            try
            {
                stateRepository.AddState(state);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestState - state name can not be empty.", ex.Message);
            }
        }

        /// <summary>Adds the state state have smaller name return false.</summary>
        [TestMethod]
        public void AddState_StateHaveSmallerName_ReturnFalse()
        {
            var state = new State
            {
                Name = "Ac"
            };

            try
            {
                stateRepository.AddState(state);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestState - state name have invalid length.", ex.Message);
            }
        }

        /// <summary>Adds the state state have longer name return false.</summary>
        [TestMethod]
        public void AddState_StateHaveLongerName_ReturnFalse()
        {
            var state = new State
            {
                Name = "Activeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee"
            };

            try
            {
                stateRepository.AddState(state);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestState - state name have invalid length.", ex.Message);
            }
        }

        /// <summary>Adds the state state have lower name return false.</summary>
        [TestMethod]
        public void AddState_StateHaveLowerName_ReturnFalse()
        {
            var state = new State
            {
                Name = "active"
            };

            try
            {
                stateRepository.AddState(state);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestState - state name can not start with lower character.", ex.Message);
            }
        }

        /// <summary>Adds the state state have digit name return false.</summary>
        [TestMethod]
        public void AddState_StateHaveDigitName_ReturnFalse()
        {
            var state = new State
            {
                Name = "Active1"
            };

            try
            {
                stateRepository.AddState(state);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestState - state name can not contain signs or digits.", ex.Message);
            }
        }

        /// <summary>Adds the state state have symbol name return false.</summary>
        [TestMethod]
        public void AddState_StateHaveSymbolName_ReturnFalse()
        {
            var state = new State
            {
                Name = "Active@#"
            };

            try
            {
                stateRepository.AddState(state);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestState - state name can not contain signs or digits.", ex.Message);
            }
        }
    }
}
