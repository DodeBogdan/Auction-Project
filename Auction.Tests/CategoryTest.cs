//-----------------------------------------------------------------------
// <copyright file="CategoryTest.cs" company="Transilvania University of Brasov">
//     Copyright (c) Bogdan Gheorghe Nicolae. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

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

        [TestMethod]
        public void AddCategory_ValidCategory_InsertCategory()
        {
            var category = new Category
            {
                Name = "Electronics"
            };

            categoryRepository.AddCategory(category);
        }

        [TestMethod]
        public void AddCategory_WhenCategoryIsNull_ExpectedException()
        {
            try
            {
                categoryRepository.AddCategory(null);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestCategory - category can not be null.", ex.Message);
            }
        }

        [TestMethod]
        public void AddCategory_CategoryHaveNullName_ExpectedException()
        {
            var category = new Category
            {
                Name = null
            };

            try
            {
                categoryRepository.AddCategory(category);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestCategory - category name can not be null.", ex.Message);
            }
        }

        [TestMethod]
        public void AddCategory_CategoryHaveEmptyName_ExpectedException()
        {
            var category = new Category
            {
                Name = string.Empty
            };

            try
            {
                categoryRepository.AddCategory(category);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestCategory - category name can not pe empty.", ex.Message);
            }
        }

        [TestMethod]
        public void AddCategory_CategoryHaveSmallerName_ExpectedException()
        {
            var category = new Category
            {
                Name = "el"
            };

            try
            {
                categoryRepository.AddCategory(category);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestCategory - category name have invalid length.", ex.Message);
            }
        }

        [TestMethod]
        public void AddCategory_CategoryHaveLongerName_ExpectedException()
        {
            var category = new Category
            {
                Name = "Electronicsssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss"
            };

            try
            {
                categoryRepository.AddCategory(category);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestCategory - category name have invalid length.", ex.Message);
            }
        }

        [TestMethod]
        public void AddCategory_CategoryHaveLowerName_ExpectedException()
        {
            var category = new Category
            {
                Name = "electronics"
            };

            try
            {
                categoryRepository.AddCategory(category);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestCategory - category name can not start with lower character.", ex.Message);
            }
        }

        [TestMethod]
        public void AddCategory_CategoryHaveDigitName_ExpectedException()
        {
            var category = new Category
            {
                Name = "Electronics1"
            };

            try
            {
                categoryRepository.AddCategory(category);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestCategory - category name can not contain signs or digits.", ex.Message);
            }
        }

        [TestMethod]
        public void AddCategory_CategoryHaveSymbolName_ExpectedException()
        {
            var category = new Category
            {
                Name = "Electronics@#"
            };

            try
            {
                categoryRepository.AddCategory(category);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestCategory - category name can not contain signs or digits.", ex.Message);
            }
        }

        [TestMethod]
        public void AddParentToCategory_CategorySonIdSmallerThanZero_ExpectedException()
        {
            try
            {
                categoryRepository.AddParentToCategory(0, 1);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestCategoryParent - categorySon can not be smaller than 0.", ex.Message);
            }
        }

        [TestMethod]
        public void AddParentToCategory_CategoryParentIdSmallerThanZero_ExpectedException()
        {
            try
            {
                categoryRepository.AddParentToCategory(1, 0);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestCategoryParent - categoryParent can not be smaller than 0.", ex.Message);
            }
        }

        [TestMethod]
        public void AddParentToCategory_CategorySonIsTheSameWithParent_ExpectedException()
        {
            try
            {
                categoryRepository.AddParentToCategory(1, 1);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TestCategoryParent - categorySon can not be equal with categoryParent.", ex.Message);
            }
        }

        [TestMethod]
        public void AddParentToCategory_CategorySonIsInvalid_ExpectedException()
        {
            try
            {
                categoryRepository.AddParentToCategory(1, 2);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("AddParentToCategory - there are no items with categorySon id.", ex.Message);
            }
        }

        [TestMethod]
        public void AddParentToCategory_CategoryParentIsInvalid_ExpectedException()
        {
            auctionMock.Categories.Add(new Category
            {
                ID = 1,
                Name = "Electronics"
            });

            try
            {
                categoryRepository.AddParentToCategory(1, 2);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("AddParentToCategory - there are no items with categoryParent id.", ex.Message);
            }
        }

        [TestMethod]
        public void AddParentToCategory_InsertWithSuccess_WorkSuccessful()
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

        }

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

        [TestMethod]
        public void AddParentToCategory_CannotInsertParentIfTheSonHaveABoundWithAChildOfParent_ExpectedException()
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

            try
            {
                categoryRepository.AddParentToCategory(3, 1);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("AddParentToCategory - categorySon is already bound with a son of categoryParent.", ex.Message);
            }
        }
    }
}
