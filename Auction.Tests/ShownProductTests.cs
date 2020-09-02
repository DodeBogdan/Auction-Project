//-----------------------------------------------------------------------
// <copyright file="ShownProductTests.cs" company="Transilvania University of Brasov">
//     Copyright (c) Bogdan Gheorghe Nicolae. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Auction.Tests
{
    using AuctionLogic.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>Shown product test class</summary>
    [TestClass]
    public class ShownProductTests
    {
        /// <summary>The shown product</summary>
        private readonly ShownProduct shownProduct = new ShownProduct
        {
            Id = 1,
            Description = "Best served hot.",
            Name = "Photo camera CANON",
            Price = 5.99
        };

        /// <summary>Shown the product get shown product name get properly.</summary>
        [TestMethod]
        public void ShownProduct_GetShownProductName_GetProperly()
        {
            Assert.IsTrue(shownProduct.Name == "Photo camera CANON");
        }

        /// <summary>Shown the product get shown product identifier get properly.</summary>
        [TestMethod]
        public void ShownProduct_GetShownProductId_GetProperly()
        {
            Assert.IsTrue(shownProduct.Id == 1);
        }

        /// <summary>Shown the product get shown product description get properly.</summary>
        [TestMethod]
        public void ShownProduct_GetShownProductDescription_GetProperly()
        {
            Assert.IsTrue(shownProduct.Description == "Best served hot.");
        }

        /// <summary>Shown the product get shown product price get properly.</summary>
        [TestMethod]
        public void ShownProduct_GetShownProductPrice_GetProperly()
        {
            Assert.IsTrue(shownProduct.Price == 5.99);
        }
    }
}
