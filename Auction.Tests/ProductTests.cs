//-----------------------------------------------------------------------
// <copyright file="ProductTests.cs" company="Transilvania University of Brasov">
//     Copyright (c) Bogdan Gheorghe Nicolae. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Auction.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AuctionLogic.Business;
    using AuctionLogic.Models;
    using AuctionLogic.Repositories;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Telerik.JustMock.EntityFramework;

    /// <summary>Product tests class.</summary>
    [TestClass]
    public class ProductTests
    {
        /// <summary>The auction mock</summary>
        private AuctionDB auctionMock;

        /// <summary>The product repository</summary>
        private ProductRepository productRepository;

        /// <summary>The bidder menu</summary>
        private BidderMenu bidderMenu;

        /// <summary>The user repository</summary>
        private UserRepository userRepository;

        /// <summary>The startup application</summary>
        private StartupApplication startupApplication;

        /// <summary>The provider menu</summary>
        private ProviderMenu providerMenu;

        /// <summary>Sets up.</summary>
        [TestInitialize]
        public void SetUp()
        {
            auctionMock = EntityFrameworkMock.Create<AuctionDB>();
            productRepository = new ProductRepository(auctionMock);
            userRepository = new UserRepository(auctionMock);
            bidderMenu = new BidderMenu(userRepository, productRepository);
            startupApplication = new StartupApplication(auctionMock);
            productRepository = new ProductRepository(auctionMock);
            userRepository = new UserRepository(auctionMock);
            providerMenu = new ProviderMenu(productRepository, userRepository);

            startupApplication.Register(
                new User
                {
                    ID = 1,
                    FirstName = "Silvia",
                    LastName = "Brassoi",
                    Email = "Silvia.Brassoi@yahoo.com",
                    Password = "Silvia",
                    Age = 23,
                    Gender = "F",
                    CNP = "2123456789123",
                    Adress = "Mandra nr. 61",
                    Phone = "0762253553",
                    RoleStatus = 2,
                    Active = false
                });

            startupApplication.Register(
                new User
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
                    Phone = "0762273552",
                    RoleStatus = 2,
                    Active = false
                });
        }

        /// <summary>Adds the product valid product return true.</summary>
        [TestMethod]
        public void AddProduct_ValidProduct_ReturnTrue()
        {
            var product = new Product
            {
                Name = "Photo Camera",
                Description = "Very good quality.",
                IDState = 1,
                IDCategory = 1,
                IDUser = 1,
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(23),
                StartPrice = 15.34,
                Specification = "Quality pictures. Good colors.",
                Coin = "RON",
                Active = true,
            };

            Assert.IsTrue(productRepository.AddProduct(product));
        }

        /// <summary>Adds the product when product is null return false.</summary>
        [TestMethod]
        public void AddProduct_WhenProductIsNull_ReturnFalse()
        {
            Assert.IsFalse(productRepository.AddProduct(null));
        }

        /// <summary>Adds the product product have null name return false.</summary>
        [TestMethod]
        public void AddProduct_ProductHaveNullName_ReturnFalse()
        {
            var product = new Product
            {
                Name = null,
                Active = true
            };

            Assert.IsFalse(productRepository.AddProduct(product));
        }

        /// <summary>Adds the product product have empty name return false.</summary>
        [TestMethod]
        public void AddProduct_ProductHaveEmptyName_ReturnFalse()
        {
            var product = new Product
            {
                Name = string.Empty,
                Active = true
            };

            Assert.IsFalse(productRepository.AddProduct(product));
        }

        /// <summary>Adds the product product have smaller name return false.</summary>
        [TestMethod]
        public void AddProduct_ProductHaveSmallerName_ReturnFalse()
        {
            var product = new Product
            {
                Name = "bi",
                Active = true
            };

            Assert.IsFalse(productRepository.AddProduct(product));
        }

        /// <summary>Adds the product product have longer name return false.</summary>
        [TestMethod]
        public void AddProduct_ProductHaveLongerName_ReturnFalse()
        {
            var product = new Product
            {
                Name = "Bideeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee",
                Active = true
            };

            Assert.IsFalse(productRepository.AddProduct(product));
        }

        /// <summary>Adds the product product have lower name return false.</summary>
        [TestMethod]
        public void AddProduct_ProductHaveLowerName_ReturnFalse()
        {
            var product = new Product
            {
                Name = "bidder",
            };

            Assert.IsFalse(productRepository.AddProduct(product));
        }

        /// <summary>Adds the product product have digit name return false.</summary>
        [TestMethod]
        public void AddProduct_ProductHaveDigitName_ReturnFalse()
        {
            var product = new Product
            {
                Name = "Bidder1",
            };

            Assert.IsFalse(productRepository.AddProduct(product));
        }

        /// <summary>Adds the product product have symbol name return false.</summary>
        [TestMethod]
        public void AddProduct_ProductHaveSymbolName_ReturnFalse()
        {
            var product = new Product
            {
                Name = "Bidder@#",
            };

            Assert.IsFalse(productRepository.AddProduct(product));
        }

        /// <summary>Adds the product product have null description return false.</summary>
        [TestMethod]
        public void AddProduct_ProductHaveNullDescription_ReturnFalse()
        {
            var product = new Product
            {
                Name = "Video camera",
                Description = null,
            };

            Assert.IsFalse(productRepository.AddProduct(product));
        }

        /// <summary>Adds the product product have empty description return false.</summary>
        [TestMethod]
        public void AddProduct_ProductHaveEmptyDescription_ReturnFalse()
        {
            var product = new Product
            {
                Name = "Video camera",
                Description = string.Empty,
            };

            Assert.IsFalse(productRepository.AddProduct(product));
        }

        /// <summary>Adds the product product have smaller description return false.</summary>
        [TestMethod]
        public void AddProduct_ProductHaveSmallerDescription_ReturnFalse()
        {
            var product = new Product
            {
                Name = "Video camera",
                Description = "bddi",
            };

            Assert.IsFalse(productRepository.AddProduct(product));
        }

        /// <summary>Adds the product product have longer description return false.</summary>
        [TestMethod]
        public void AddProduct_ProductHaveLongerDescription_ReturnFalse()
        {
            var product = new Product
            {
                Name = "Video camera",
                Description = "Bideeeeeeeeeeeeeeeasddddddddddddddddddddddddddddddddddddddddddddddddddddeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee",
            };

            Assert.IsFalse(productRepository.AddProduct(product));
        }

        /// <summary>Adds the product product have lower description return false.</summary>
        [TestMethod]
        public void AddProduct_ProductHaveLowerDescription_ReturnFalse()
        {
            var product = new Product
            {
                Name = "Video camera",
                Description = "bidderasdasd dasdas",
            };

            Assert.IsFalse(productRepository.AddProduct(product));
        }

        /// <summary>Adds the product product have symbol description return false.</summary>
        [TestMethod]
        public void AddProduct_ProductHaveSymbolDescription_ReturnFalse()
        {
            var product = new Product
            {
                Name = "Video camera",
                Description = "Biddasdasd dasder@#",
            };

            Assert.IsFalse(productRepository.AddProduct(product));
        }

        /// <summary>Adds the product product have state null return false.</summary>
        [TestMethod]
        public void AddProduct_ProductHaveStateNull_ReturnFalse()
        {
            var product = new Product
            {
                Name = "Photo Camera",
                Description = "Very good quality.",
            };

            Assert.IsFalse(productRepository.AddProduct(product));
        }

        /// <summary>Adds the product product have state smaller than zero return false.</summary>
        [TestMethod]
        public void AddProduct_ProductHaveStateSmallerThanZero_ReturnFalse()
        {
            var product = new Product
            {
                Name = "Photo Camera",
                Description = "Very good quality.",
                IDState = -1,
            };

            Assert.IsFalse(productRepository.AddProduct(product));
        }

        /// <summary>Adds the product product have invalid state return false.</summary>
        [TestMethod]
        public void AddProduct_ProductHaveInvalidState_ReturnFalse()
        {
            var product = new Product
            {
                Name = "Photo Camera",
                Description = "Very good quality.",
                IDState = 7,
            };

            Assert.IsFalse(productRepository.AddProduct(product));
        }

        /// <summary>Adds the product product have null category return false.</summary>
        [TestMethod]
        public void AddProduct_ProductHaveNullCategory_ReturnFalse()
        {
            var product = new Product
            {
                Name = "Photo Camera",
                Description = "Very good quality.",
                IDState = 1,
            };

            Assert.IsFalse(productRepository.AddProduct(product));
        }

        /// <summary>Adds the product product have invalid category return false.</summary>
        [TestMethod]
        public void AddProduct_ProductHaveInvalidCategory_ReturnFalse()
        {
            var product = new Product
            {
                Name = "Photo Camera",
                Description = "Very good quality.",
                IDState = 1,
                IDCategory = -1,
            };

            Assert.IsFalse(productRepository.AddProduct(product));
        }

        /// <summary>Adds the product product have null user return false.</summary>
        [TestMethod]
        public void AddProduct_ProductHaveNullUser_ReturnFalse()
        {
            var product = new Product
            {
                Name = "Photo Camera",
                Description = "Very good quality.",
                IDState = 1,
                IDCategory = 1,
            };

            Assert.IsFalse(productRepository.AddProduct(product));
        }

        /// <summary>Adds the product product have invalid user return false.</summary>
        [TestMethod]
        public void AddProduct_ProductHaveInvalidUser_ReturnFalse()
        {
            var product = new Product
            {
                Name = "Photo Camera",
                Description = "Very good quality.",
                IDState = 1,
                IDCategory = 1,
            };

            Assert.IsFalse(productRepository.AddProduct(product));
        }

        /// <summary>Adds the product product have null start date return false.</summary>
        [TestMethod]
        public void AddProduct_ProductHaveNullStartDate_ReturnFalse()
        {
            var product = new Product
            {
                Name = "Photo Camera",
                Description = "Very good quality.",
                IDState = 1,
                IDUser = 1,
                IDCategory = 1,
            };

            Assert.IsFalse(productRepository.AddProduct(product));
        }

        /// <summary>Adds the product product have invalid start date return false.</summary>
        [TestMethod]
        public void AddProduct_ProductHaveInvalidStartDate_ReturnFalse()
        {
            var product = new Product
            {
                Name = "Photo Camera",
                Description = "Very good quality.",
                IDState = 1,
                IDUser = 1,
                IDCategory = 1,
                StartDate = DateTime.Now.AddDays(-1),
            };

            Assert.IsFalse(productRepository.AddProduct(product));
        }

        /// <summary>Adds the product product have over due start date return false.</summary>
        [TestMethod]
        public void AddProduct_ProductHaveOverDueStartDate_ReturnFalse()
        {
            var product = new Product
            {
                Name = "Photo Camera",
                Description = "Very good quality.",
                IDState = 1,
                IDUser = 1,
                IDCategory = 1,
                StartDate = DateTime.Now.AddMonths(5),
            };

            Assert.IsFalse(productRepository.AddProduct(product));
        }

        /// <summary>Adds the product product have null end date return false.</summary>
        [TestMethod]
        public void AddProduct_ProductHaveNullEndDate_ReturnFalse()
        {
            var product = new Product
            {
                Name = "Photo Camera",
                Description = "Very good quality.",
                IDState = 1,
                IDUser = 1,
                IDCategory = 1,
                StartDate = DateTime.Now.AddDays(1),
            };

            Assert.IsFalse(productRepository.AddProduct(product));
        }

        /// <summary>Adds the product product have invalid end date return false.</summary>
        [TestMethod]
        public void AddProduct_ProductHaveInvalidEndDate_ReturnFalse()
        {
            var product = new Product
            {
                Name = "Photo Camera",
                Description = "Very good quality.",
                IDState = 1,
                IDUser = 1,
                IDCategory = 1,
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(-23),
            };

            Assert.IsFalse(productRepository.AddProduct(product));
        }

        /// <summary>Adds the product product have over due end date return false.</summary>
        [TestMethod]
        public void AddProduct_ProductHaveOverDueEndDate_ReturnFalse()
        {
            var startDate = DateTime.Now.AddDays(1);

            var product = new Product
            {
                Name = "Photo Camera",
                Description = "Very good quality.",
                IDState = 1,
                IDUser = 1,
                IDCategory = 1,
                StartDate = startDate,
                EndDate = startDate.AddMonths(5),

                StartPrice = 15.34,
                Specification = "Quality pictures. Good colors.",
                Coin = "RON",
                Active = true,
            };

            Assert.IsFalse(productRepository.AddProduct(product));
        }

        /// <summary>Adds the product product have null price return false.</summary>
        [TestMethod]
        public void AddProduct_ProductHaveNullPrice_ReturnFalse()
        {
            var startDate = DateTime.Now.AddDays(1);

            var product = new Product
            {
                Name = "Photo Camera",
                Description = "Very good quality.",
                IDState = 1,
                IDUser = 1,
                IDCategory = 1,
                StartDate = startDate,
                EndDate = startDate.AddMonths(1),
            };

            Assert.IsFalse(productRepository.AddProduct(product));
        }

        /// <summary>Adds the product product have smaller price return false.</summary>
        [TestMethod]
        public void AddProduct_ProductHaveSmallerPrice_ReturnFalse()
        {
            var startDate = DateTime.Now.AddDays(1);

            var product = new Product
            {
                Name = "Photo Camera",
                Description = "Very good quality.",
                IDState = 1,
                IDUser = 1,
                IDCategory = 1,
                StartDate = startDate,
                EndDate = startDate.AddMonths(1),
                StartPrice = 6.54,
            };

            Assert.IsFalse(productRepository.AddProduct(product));
        }

        /// <summary>Gets the products when call method get list of shown products.</summary>
        [TestMethod]
        public void GetProducts_WhenCallMethod_GetListOfShownProducts()
        {
            startupApplication.LogIn("Silvia.Brassoi@yahoo.com", "Silvia", 2);

            auctionMock.Products.Add(new Product
            {
                ID = 1,
                Name = "Popcorn",
                Description = "Can be found in different flavors.",
                IDState = 1,
                IDUser = 1,
                IDCategory = 1,
                StartDate = DateTime.Now.AddMinutes(1),
                EndDate = DateTime.Now.AddMinutes(10),
                StartPrice = 25f,
                Coin = "RON",
                Specification = "Quality pictures. Good colors.",
            });

            auctionMock.Products.Add(new Product
            {
                ID = 2,
                Name = "Pasta",
                Description = "Can be found in different flavors.",
                IDState = 1,
                IDUser = 1,
                IDCategory = 2,
                StartDate = DateTime.Now.AddMinutes(1),
                EndDate = DateTime.Now.AddMinutes(10),
                StartPrice = 25f,
                Coin = "RON",
                Specification = "Quality pictures. Good colors.",
                Active = true
            });

            auctionMock.Products.Add(new Product
            {
                ID = 3,
                Name = "Fish",
                Description = "Can be found in different flavors.",
                IDState = 1,
                IDUser = 1,
                IDCategory = 1,
                StartDate = DateTime.Now.AddMinutes(1),
                EndDate = DateTime.Now.AddMinutes(10),
                StartPrice = 25f,
                Coin = "RON",
                Specification = "Quality pictures. Good colors.",
                Active = true
            });

            userRepository.LogOut();

            startupApplication.LogIn("Cata.Brassoi@yahoo.com", "Silvia", 2);

            auctionMock.Products.Add(new Product
            {
                ID = 4,
                Name = "Paste",
                Description = "Can be found in different flavors.",
                IDState = 1,
                IDCategory = 2,
                IDUser = 2,
                StartDate = DateTime.Now.AddMinutes(1),
                EndDate = DateTime.Now.AddMinutes(10),
                StartPrice = 25f,
                Coin = "RON",
                Specification = "Quality pictures. Good colors.",
                Active = true
            });

            auctionMock.Products.Add(new Product
            {
                ID = 5,
                Name = "Photo camera CANON",
                Description = "Can be found in different flavors.",
                IDState = 1,
                IDCategory = 1,
                IDUser = 2,
                StartDate = DateTime.Now.AddMinutes(1),
                EndDate = DateTime.Now.AddMinutes(10),
                StartPrice = 25f,
                Coin = "RON",
                Specification = "Quality pictures. Good colors.",
                Active = true
            });

            providerMenu.LogOut();

            startupApplication.LogIn("Cata.Brassoi@yahoo.com", "Silvia", 1);

            List<ShownProduct> products = productRepository.GetProductsThatDoesNotBelongToAUser(userRepository.GetActiveUser().ID);

            List<ShownProduct> resultList = new List<ShownProduct>
            {
                new ShownProduct { Id = 1, Name = "Popcorn", Description  = "Can be found in different flavors.", Price = 25f },
                new ShownProduct { Id = 2, Name = "Pasta", Description  = "Can be found in different flavors.", Price = 25f },
                new ShownProduct { Id = 3, Name = "Fish", Description  = "Can be found in different flavors.", Price = 25f },
            };

            int times = 0;
            foreach (var product in products)
            {
                foreach (var result in resultList)
                {
                    if (product.Id == result.Id)
                    {
                        times++;
                    }
                }
            }

            Assert.IsTrue(times == products.Count());

            bidderMenu.LogOut();

            startupApplication.LogIn("Silvia.Brassoi@yahoo.com", "Silvia", 1);

            resultList = new List<ShownProduct>
            {
                new ShownProduct { Id = 4, Name = "Chips", Description  = "Can be found in different flavors.", Price = 25f },
                new ShownProduct { Id = 5, Name = "Oreo", Description  = "Can be found in different flavors.", Price = 25f },
            };

            products = productRepository.GetProductsThatDoesNotBelongToAUser(userRepository.GetActiveUser().ID);

            times = 0;
            foreach (var product in products)
            {
                foreach (var result in resultList)
                {
                    if (product.Id == result.Id)
                    {
                        times++;
                    }
                }
            }

            Assert.IsTrue(times == products.Count());
        }

        /// <summary>Adds the product product have null coin return false.</summary>
        [TestMethod]
        public void AddProduct_ProductHaveNullCoin_ReturnFalse()
        {
            var product = new Product
            {
                Name = "Photo camera CANON",
                Description = "Very good quality.",
                IDState = 1,
                IDUser = 1,
                IDCategory = 1,
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(23),
                StartPrice = 15.34,
                Specification = "Quality pictures. Good colors.",
                Active = true,
                Coin = null
            };

            Assert.IsFalse(productRepository.AddProduct(product));
        }

        /// <summary>Adds the product product have empty coin return false.</summary>
        [TestMethod]
        public void AddProduct_ProductHaveEmptyCoin_ReturnFalse()
        {
            var product = new Product
            {
                Name = "Photo camera CANON",
                Description = "Very good quality.",
                IDState = 1,
                IDUser = 1,
                IDCategory = 1,
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(23),
                StartPrice = 15.34,
                Specification = "Quality pictures. Good colors.",
                Active = true,
                Coin = string.Empty
            };

            Assert.IsFalse(productRepository.AddProduct(product));
        }

        /// <summary>Adds the product product have smaller coin return false.</summary>
        [TestMethod]
        public void AddProduct_ProductHaveSmallerCoin_ReturnFalse()
        {
            var product = new Product
            {
                Name = "Photo camera CANON",
                Description = "Very good quality.",
                IDState = 1,
                IDUser = 1,
                IDCategory = 1,
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(23),
                StartPrice = 15.34,
                Specification = "Quality pictures. Good colors.",
                Active = true,
                Coin = "RO"
            };

            Assert.IsFalse(productRepository.AddProduct(product));
        }

        /// <summary>Adds the product product have longer coin return false.</summary>
        [TestMethod]
        public void AddProduct_ProductHaveLongerCoin_ReturnFalse()
        {
            var product = new Product
            {
                Name = "Photo camera CANON",
                Description = "Very good quality.",
                IDState = 1,
                IDUser = 1,
                IDCategory = 1,
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(23),
                StartPrice = 15.34,
                Specification = "Quality pictures. Good colors.",
                Active = true,
                Coin = "ROOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOONNNNNNNNNNNNNNNNNN"
            };

            Assert.IsFalse(productRepository.AddProduct(product));
        }

        /// <summary>Adds the product product have lower coin return false.</summary>
        [TestMethod]
        public void AddProduct_ProductHaveLowerCoin_ReturnFalse()
        {
            var product = new Product
            {
                Name = "Photo camera CANON",
                Description = "Very good quality.",
                IDState = 1,
                IDUser = 1,
                IDCategory = 1,
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(23),
                StartPrice = 15.34,
                Active = true,
                Specification = "Quality pictures. Good colors.",
                Coin = "ron"
            };

            Assert.IsFalse(productRepository.AddProduct(product));
        }

        /// <summary>Adds the product product have digit coin return false.</summary>
        [TestMethod]
        public void AddProduct_ProductHaveDigitCoin_ReturnFalse()
        {
            var product = new Product
            {
                Name = "Photo camera CANON",
                Description = "Very good quality.",
                IDState = 1,
                IDUser = 1,
                IDCategory = 1,
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(23),
                StartPrice = 15.34,
                Active = true,
                Specification = "Quality pictures. Good colors.",
                Coin = "RON2"
            };

            Assert.IsFalse(productRepository.AddProduct(product));
        }

        /// <summary>Adds the product product have symbol coin return false.</summary>
        [TestMethod]
        public void AddProduct_ProductHaveSymbolCoin_ReturnFalse()
        {
            var product = new Product
            {
                Name = "Photo camera CANON",
                Description = "Very good quality.",
                IDState = 1,
                IDUser = 1,
                IDCategory = 1,
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(23),
                StartPrice = 15.34,
                Active = true,
                Specification = "Quality pictures. Good colors.",
                Coin = "RON#$"
            };

            Assert.IsFalse(productRepository.AddProduct(product));
        }

        /// <summary>Products the get category get category.</summary>
        [TestMethod]
        public void Product_GetCategory_GetCategory()
        {
            auctionMock.Categories.Add(new Category { ID = 1, Name = "Food" });

            auctionMock.Products.Add(new Product
            {
                ID = 10,
                Name = "Photo camera CANON",
                Description = "Very good quality.",
                IDState = 1,
                IDUser = 1,
                IDCategory = 1,
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(23),
                StartPrice = 15.34,
                EndPrice = 46.23,
                AuctedUser = 1,
                Active = true,
                Specification = "Quality pictures. Good colors.",
                Coin = "RON#$"
            });

            Product product = auctionMock.Products
                .Where(x => x.ID == 10).SingleOrDefault();

            product.Category = new Category();

            Assert.IsTrue(product.Category.ID == 0);
        }

        /// <summary>Products the state of the get state get.</summary>
        [TestMethod]
        public void Product_GetState_GetState()
        {
            auctionMock.States.Add(new State { ID = 1, Name = "Now" });

            auctionMock.Products.Add(new Product
            {
                ID = 10,
                Name = "Photo camera CANON",
                Description = "Very good quality.",
                IDState = 1,
                IDUser = 1,
                IDCategory = 1,
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(23),
                StartPrice = 15.34,
                EndPrice = 46.23,
                AuctedUser = 1,
                Specification = "Quality pictures. Good colors.",
                Active = true,
                Coin = "RON#$"
            });

            Product product = auctionMock.Products.Single(x => x.ID == 10);

            product.State = new State();

            Assert.IsTrue(product.State.ID == 0);
        }

        /// <summary>Products the get user get user.</summary>
        [TestMethod]
        public void Product_GetUser_GetUser()
        {
            auctionMock.Products.Add(new Product
            {
                ID = 10,
                Name = "Photo camera CANON",
                Description = "Very good quality.",
                IDState = 1,
                IDUser = 1,
                IDCategory = 1,
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(23),
                StartPrice = 15.34,
                EndPrice = 46.23,
                AuctedUser = 1,
                Specification = "Quality pictures. Good colors.",
                Active = true,
                Coin = "RON#$"
            });

            Product product = auctionMock.Products.Single(x => x.ID == 10);

            product.User = new User();

            Assert.IsTrue(product.User.ID == 0);
        }

        /// <summary>Products the get user1 get user1.</summary>
        [TestMethod]
        public void Product_GetUser1_GetUser1()
        {
            auctionMock.Products.Add(new Product
            {
                ID = 10,
                Name = "Photo camera CANON",
                Description = "Very good quality.",
                IDState = 1,
                IDUser = 1,
                IDCategory = 1,
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(23),
                StartPrice = 15.34,
                EndPrice = 46.23,
                AuctedUser = 1,
                Active = true,
                Coin = "RON#$"
            });

            Product product = auctionMock.Products.Single(x => x.ID == 10);

            product.User1 = new User();

            Assert.IsTrue(product.User1.ID == 0);
        }

        /// <summary>Adds the product product have null specifications return false.</summary>
        [TestMethod]
        public void AddProduct_ProductHaveNullSpecifications_ReturnFalse()
        {
            var product = new Product
            {
                Name = "Video camera",
                Description = "Very good quality.",
                IDState = 1,
                IDUser = 1,
                IDCategory = 1,
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(23),
                StartPrice = 15.34,
                Active = true
            };

            Assert.IsFalse(productRepository.AddProduct(product));
        }

        /// <summary>Adds the product product have empty specification return false.</summary>
        [TestMethod]
        public void AddProduct_ProductHaveEmptySpecification_ReturnFalse()
        {
            var product = new Product
            {
                Name = "Video camera",
                Description = "Very good quality.",
                IDState = 1,
                IDUser = 1,
                IDCategory = 1,
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(23),
                StartPrice = 15.34,
                Specification = string.Empty,
                Active = true
            };

            Assert.IsFalse(productRepository.AddProduct(product));
        }

        /// <summary>Adds the product product have smaller specification return false.</summary>
        [TestMethod]
        public void AddProduct_ProductHaveSmallerSpecification_ReturnFalse()
        {
            var product = new Product
            {
                Name = "Video camera",
                Description = "Very good quality.",
                IDState = 1,
                IDUser = 1,
                IDCategory = 1,
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(23),
                StartPrice = 15.34,
                Specification = "spec",
                Active = true
            };

            Assert.IsFalse(productRepository.AddProduct(product));
        }

        /// <summary>Adds the product product have longer specification return false.</summary>
        [TestMethod]
        public void AddProduct_ProductHaveLongerSpecification_ReturnFalse()
        {
            var product = new Product
            {
                Name = "Video camera",
                Description = "Very good quality.",
                IDState = 1,
                IDUser = 1,
                IDCategory = 1,
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(23),
                StartPrice = 15.34,
                Specification = "Speeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee",
                Active = true
            };

            Assert.IsFalse(productRepository.AddProduct(product));
        }

        /// <summary>Adds the product product have lower specification return false.</summary>
        [TestMethod]
        public void AddProduct_ProductHaveLowerSpecification_ReturnFalse()
        {
            var product = new Product
            {
                Name = "Video camera",
                Description = "Very good quality.",
                IDState = 1,
                IDUser = 1,
                IDCategory = 1,
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(23),
                StartPrice = 15.34,
                Specification = "goodddddddd",
                Active = true
            };

            Assert.IsFalse(productRepository.AddProduct(product));
        }

        /// <summary>Adds the product product have symbol specification return false.</summary>
        [TestMethod]
        public void AddProduct_ProductHaveSymbolSpecification_ReturnFalse()
        {
            var product = new Product
            {
                Name = "Video camera",
                Description = "Very good quality.",
                IDState = 1,
                IDUser = 1,
                IDCategory = 1,
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(23),
                StartPrice = 15.34,
                Specification = "Mega good @ddd",
                Active = true
            };

            Assert.IsFalse(productRepository.AddProduct(product));
        }
    }
}
