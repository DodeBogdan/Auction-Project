//-----------------------------------------------------------------------
// <copyright file="CategoryTest.cs" company="Transilvania University of Brasov">
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

    /// <summary>Category test class.</summary>
    [TestClass]
    public class CategoryTest
    {
        /// <summary>The auction mock</summary>
        private AuctionDB auctionMock;

        /// <summary>The category repository</summary>
        private CategoryRepository categoryRepository;

        /// <summary>Sets up.</summary>
        [TestInitialize]
        public void SetUp()
        {
            auctionMock = EntityFrameworkMock.Create<AuctionDB>();
            categoryRepository = new CategoryRepository(auctionMock);
        }

        /// <summary>Adds the category valid category return true.</summary>
        [TestMethod]
        public void AddCategory_ValidCategory_ReturnTrue()
        {
            var category = new Category
            {
                Name = "Electronics"
            };

            Assert.IsTrue(categoryRepository.AddCategory(category));
        }

        /// <summary>Adds the category when category is null return false.</summary>
        [TestMethod]
        public void AddCategory_WhenCategoryIsNull_ReturnFalse()
        {
            Assert.IsFalse(categoryRepository.AddCategory(null));
        }

        #region Category_Object_Test

        /// <summary>Adds the category category have null name return false.</summary>
        [TestMethod]
        public void AddCategory_CategoryHaveNullName_ReturnFalse()
        {
            var category = new Category
            {
                Name = null
            };

            Assert.IsFalse(categoryRepository.AddCategory(category));
        }

        /// <summary>Adds the category category have empty name return false.</summary>
        [TestMethod]
        public void AddCategory_CategoryHaveEmptyName_ReturnFalse()
        {
            var category = new Category
            {
                Name = string.Empty
            };

            Assert.IsFalse(categoryRepository.AddCategory(category));
        }

        /// <summary>Adds the category category have smaller name return false.</summary>
        [TestMethod]
        public void AddCategory_CategoryHaveSmallerName_ReturnFalse()
        {
            var category = new Category
            {
                Name = "el"
            };

            Assert.IsFalse(categoryRepository.AddCategory(category));
        }

        /// <summary>Adds the category category have longer name return false.</summary>
        [TestMethod]
        public void AddCategory_CategoryHaveLongerName_ReturnFalse()
        {
            var category = new Category
            {
                Name = "Electronicsssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss"
            };

            Assert.IsFalse(categoryRepository.AddCategory(category));
        }

        /// <summary>Adds the category category have lower name return false.</summary>
        [TestMethod]
        public void AddCategory_CategoryHaveLowerName_ReturnFalse()
        {
            var category = new Category
            {
                Name = "electronics"
            };

            Assert.IsFalse(categoryRepository.AddCategory(category));
        }

        /// <summary>Adds the category category have digit name return false.</summary>
        [TestMethod]
        public void AddCategory_CategoryHaveDigitName_ReturnFalse()
        {
            var category = new Category
            {
                Name = "Electronics1"
            };

            Assert.IsFalse(categoryRepository.AddCategory(category));
        }

        /// <summary>Adds the category category have symbol name return false.</summary>
        [TestMethod]
        public void AddCategory_CategoryHaveSymbolName_ReturnFalse()
        {
            var category = new Category
            {
                Name = "Electronics@#"
            };

            Assert.IsFalse(categoryRepository.AddCategory(category));
        }

        #endregion

        /// <summary>Adds the parent to category category son identifier smaller than zero return false.</summary>
        [TestMethod]
        public void AddParentToCategory_CategorySonIdSmallerThanZero_ReturnFalse()
        {
            bool result = categoryRepository.AddParentToCategory(0, 0);

            Assert.IsTrue(result == false);
        }

        /// <summary>Adds the parent to category category parent identifier smaller than zero return false.</summary>
        [TestMethod]
        public void AddParentToCategory_CategoryParentIdSmallerThanZero_ReturnFalse()
        {
            bool result = categoryRepository.AddParentToCategory(1, 0);

            Assert.IsTrue(result == false);
        }

        /// <summary>Adds the parent to category category son is the same with parent return false.</summary>
        [TestMethod]
        public void AddParentToCategory_CategorySonIsTheSameWithParent_ReturnFalse()
        {
            bool result = categoryRepository.AddParentToCategory(1, 1);

            Assert.IsTrue(result == false);
        }

        /// <summary>Adds the parent to category category son is invalid return false.</summary>
        [TestMethod]
        public void AddParentToCategory_CategorySonIsInvalid_ReturnFalse()
        {
            bool result = categoryRepository.AddParentToCategory(1, 2);

            Assert.IsTrue(result == false);
        }

        /// <summary>Adds the parent to category category parent is invalid return false.</summary>
        [TestMethod]
        public void AddParentToCategory_CategoryParentIsInvalid_ReturnFalse()
        {
            auctionMock.Categories.Add(new Category
            {
                ID = 1,
                Name = "Electronics"
            });

            bool result = categoryRepository.AddParentToCategory(1, 2);

            Assert.IsTrue(result == false);
        }

        /// <summary>Adds the parent to category insert with success return true.</summary>
        [TestMethod]
        public void AddParentToCategory_InsertWithSuccess_ReturnTrue()
        {
            auctionMock.Categories.Add(new Category
            {
                ID = 1,
                Name = "Electronics"
            });

            auctionMock.Categories.Add(new Category
            {
                ID = 2,
                Name = "Fridge"
            });

            bool result = categoryRepository.AddParentToCategory(1, 2);

            Assert.IsTrue(result);
        }

        /// <summary>Gets the category by identifier when call the method return correct category.</summary>
        [TestMethod]
        public void GetCategoryByID_WhenCallTheMethod_ReturnCorrectCategory()
        {
            var category = new Category
            {
                ID = 1,
                Name = "Electronics"
            };

            auctionMock.Categories.Add(category);

            var result = categoryRepository.GetCategoryById(1);

            Assert.IsTrue(category == result);
        }

        /// <summary>Adds the parent to category cannot insert parent if the son have a bound with a child of parent return false.</summary>
        [TestMethod]
        public void AddParentToCategory_CannotInsertParentIfTheSonHaveABoundWithAChildOfParent_ReturnFalse()
        {
            auctionMock.Categories.Add(new Category
            {
                ID = 1,
                Name = "Electronics"
            });

            auctionMock.Categories.Add(new Category
            {
                ID = 2,
                Name = "Fridge"
            });

            categoryRepository.AddParentToCategory(2, 1);

            auctionMock.Categories.Add(new Category
            {
                ID = 3,
                Name = "Oven"
            });

            categoryRepository.AddParentToCategory(3, 2);

            bool result = categoryRepository.AddParentToCategory(3, 1);

            Assert.IsTrue(result == false);
        }

        /// <summary>Categories the get products by category return list of products.</summary>
        [TestMethod]
        public void Category_GetProductsByCategory_ReturnListOfProducts()
        {
            auctionMock.Categories.Add(new Category { ID = 1, Name = "Electronics" });

            Category category = auctionMock.Categories.Single(x => x.ID == 1);

            Assert.IsTrue(category.Products.Count == 0);
        }
    }
}
