//-----------------------------------------------------------------------
// <copyright file="BidderMenuTests.cs" company="Transilvania University of Brasov">
//     Copyright (c) Bogdan Gheorghe Nicolae. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Auction.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AuctionLogic.Business;
    using AuctionLogic.Exceptions;
    using AuctionLogic.Models;
    using AuctionLogic.Repositories;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Telerik.JustMock.EntityFramework;

    /// <summary>Test class for Bidder Menu</summary>
    [TestClass]
    public class BidderMenuTests
    {
        /// <summary>The auction mock</summary>
        private AuctionDB auctionMock;

        /// <summary>The startup application</summary>
        private StartupApplication startupApplication;

        /// <summary>The bidder menu</summary>
        private BidderMenu bidderMenu;

        /// <summary>The provider menu</summary>
        private ProviderMenu providerMenu;

        /// <summary>The role repository</summary>
        private RoleRepository roleRepository;

        /// <summary>The category repository</summary>
        private CategoryRepository categoryRepository;

        /// <summary>The state repository</summary>
        private StateRepository stateRepository;

        /// <summary>The user repository</summary>
        private UserRepository userRepository;

        /// <summary>The product repository</summary>
        private ProductRepository productRepository;

        /// <summary>Sets up.</summary>
        [TestInitialize]
        public void SetUp()
        {
            auctionMock = EntityFrameworkMock.Create<AuctionDB>();
            startupApplication = new StartupApplication(auctionMock);
            roleRepository = new RoleRepository(auctionMock);
            categoryRepository = new CategoryRepository(auctionMock);
            stateRepository = new StateRepository(auctionMock);
            userRepository = new UserRepository(auctionMock);
            productRepository = new ProductRepository(auctionMock);
            bidderMenu = new BidderMenu(userRepository, productRepository);
            providerMenu = new ProviderMenu(productRepository, userRepository);

            roleRepository.AddRole(new Role { ID = 1, RoleName = "Bidder" });
            roleRepository.AddRole(new Role { ID = 2, RoleName = "Provider" });

            categoryRepository.AddCategory(new Category { ID = 1, Name = "Food" });
            categoryRepository.AddCategory(new Category { ID = 2, Name = "Electronics" });
            categoryRepository.AddCategory(new Category { ID = 3, Name = "Drinks" });

            stateRepository.AddState(new State { ID = 1, Name = "New" });

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
                   Phone = "0761936316",
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
                    RoleStatus = 2,
                    Phone = "0722735533",
                    Active = false
                });

            startupApplication.Register(
                new User
                {
                    ID = 3,
                    FirstName = "Dode",
                    LastName = "Brassoi",
                    Email = "Dode.Brassoi@yahoo.com",
                    Password = "Silvia",
                    Age = 23,
                    Gender = "M",
                    CNP = "1123456789123",
                    Adress = "Mandra nr. 61",
                    RoleStatus = 1,
                    Phone = "0762273552",
                    Active = false
                });

            startupApplication.LogIn("Silvia.Brassoi@yahoo.com", "Silvia", 2);

            providerMenu.AddProduct(new Product
            {
                ID = 1,
                Name = "Popcorn",
                Description = "Can be found in different flavors.",
                IDState = 1,
                IDCategory = 1,
                IDUser = 1,
                StartDate = DateTime.Now.AddMinutes(1),
                EndDate = DateTime.Now.AddMinutes(10),
                StartPrice = 25f,
                Specification = "Quality pictures. Good colors.",
                Active = true,
                Coin = "RON"
            });

            providerMenu.AddProduct(new Product
            {
                ID = 2,
                Name = "Pasta",
                Description = "Can be found in different flavors.",
                IDState = 1,
                IDCategory = 2,
                IDUser = 1,
                StartDate = DateTime.Now.AddMinutes(1),
                EndDate = DateTime.Now.AddMinutes(10),
                StartPrice = 25f,
                Specification = "Quality pictures. Good colors.",
                Active = true,
                Coin = "RON"
            });

            providerMenu.AddProduct(new Product
            {
                ID = 3,
                Name = "Fish",
                Description = "Can be found in different flavors.",
                IDState = 1,
                IDCategory = 1,
                IDUser = 1,
                StartDate = DateTime.Now.AddMinutes(1),
                EndDate = DateTime.Now.AddMinutes(10),
                StartPrice = 25f,
                Specification = "Quality pictures. Good colors.",
                Active = true,
                Coin = "RON"
            });

            userRepository.LogOut();

            startupApplication.LogIn("Cata.Brassoi@yahoo.com", "Silvia", 2);

            providerMenu.AddProduct(new Product
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
                Specification = "Quality pictures. Good colors.",
                Active = true,
                Coin = "RON"
            });

            providerMenu.AddProduct(new Product
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
                Specification = "Quality pictures. Good colors.",
                Active = true,
                Coin = "RON"
            });

            providerMenu.LogOut();
        }

        /// <summary>Adds the product invalid user logged in excepted exception.</summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidRoleStatusException))]
        public void AddProduct_InvalidUserLoggedIn_ExceptedException()
        {
            startupApplication.LogIn("Cata.Brassoi@yahoo.com", "Silvia", 2);

            bidderMenu.VerifyRoleStatus();
        }

        /// <summary>Gets the products does not belong to current user get right list return list.</summary>
        [TestMethod]
        public void GetProductsDoesNotBelongToCurrentUser_GetRightList_ReturnList()
        {
            startupApplication.LogIn("Cata.Brassoi@yahoo.com", "Silvia", 1);

            var products = bidderMenu.GetProductsDoesNotBelongToCurrentUser();

            List<ShownProduct> resultList = new List<ShownProduct>
            {
                new ShownProduct { Id = 1, Name = "Popcorn", Description  = "Can be found in different flavors.", Price = 25f },
                new ShownProduct { Id = 2, Name = "Pasta", Description  = "Can be found in different flavors.", Price = 25f },
                new ShownProduct { Id = 3, Name = "Fish", Description  = "Can be found in different flavors.", Price = 25f },
            };

            var times = 0;
            foreach (var p in products)
            {
                foreach (var result in resultList)
                {
                    if (p.Id == result.Id)
                    {
                        times++;
                    }
                }
            }

            Assert.IsTrue(times == products.Count());
        }

        /// <summary>Gets the product by identifier when select product expected exception.</summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidProductException))]
        public void GetProductById_WhenSelectProduct_ExpectedException()
        {
            startupApplication.LogIn("Cata.Brassoi@yahoo.com", "Silvia", 1);

            bidderMenu.GetProductById(4);
        }

        /// <summary>Gets the product by identifier when select product get right product.</summary>
        [TestMethod]
        public void GetProductById_WhenSelectProduct_GetRightProduct()
        {
            startupApplication.LogIn("Cata.Brassoi@yahoo.com", "Silvia", 1);

            var product = bidderMenu.GetProductById(2);

            Product resultedProduct = new Product
            {
                ID = 2,
                Name = "Pasta",
                Description = "Can be found in different flavors.",
                IDState = 1,
                IDCategory = 2,
                IDUser = 1,
                StartDate = DateTime.Now.AddMinutes(1),
                EndDate = DateTime.Now.AddMinutes(10),
                StartPrice = 25f,
                Specification = "Quality pictures. Good colors.",
                Active = true,
                Coin = "RON"
            };
            Assert.IsTrue(product.ID == resultedProduct.ID);
        }

        /// <summary>Bids for product when bid price to small.</summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidPriceException))]
        public void BidForProduct_WhenBid_PriceToSmall()
        {
            startupApplication.LogIn("Cata.Brassoi@yahoo.com", "Silvia", 1);

            bidderMenu.BidForProduct(2, 7.4, "RON");
        }

        /// <summary>Bids for product when bid price to high than started price.</summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidPriceException))]
        public void BidForProduct_WhenBid_PriceToHighThanStartedPrice()
        {
            startupApplication.LogIn("Cata.Brassoi@yahoo.com", "Silvia", 1);

            bidderMenu.BidForProduct(2, 28.5, "RON");
        }

        /// <summary>Bids for product when bid price to small than last.</summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidPriceException))]
        public void BidForProduct_WhenBid_PriceToSmallThanLast()
        {
            startupApplication.LogIn("Cata.Brassoi@yahoo.com", "Silvia", 1);

            bidderMenu.BidForProduct(2, 27, "RON");

            bidderMenu.LogOut();
            startupApplication.LogIn("Silvia.Brassoi@yahoo.com", "Silvia", 1);

            bidderMenu.BidForProduct(2, 26, "RON");
        }

        /// <summary>Bids for product when bid price to big.</summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidPriceException))]
        public void BidForProduct_WhenBid_PriceToBig()
        {
            startupApplication.LogIn("Cata.Brassoi@yahoo.com", "Silvia", 1);

            bidderMenu.BidForProduct(2, 27, "RON");

            bidderMenu.LogOut();
            startupApplication.LogIn("Silvia.Brassoi@yahoo.com", "Silvia", 1);

            bidderMenu.BidForProduct(2, 40, "RON");
        }

        /// <summary>Bids for product when bid place price.</summary>
        [TestMethod]
        public void BidForProduct_WhenBid_PlacePrice()
        {
            startupApplication.LogIn("Cata.Brassoi@yahoo.com", "Silvia", 1);

            bidderMenu.BidForProduct(2, 27, "RON");

            bidderMenu.LogOut();
            startupApplication.LogIn("Dode.Brassoi@yahoo.com", "Silvia", 1);

            bidderMenu.BidForProduct(2, 29.4, "RON");

            var product = bidderMenu.GetProductById(2);

            Assert.IsTrue(product.EndPrice == 29.4);
        }

        /// <summary>Bids for product when bid user is last bidder.</summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidUserException))]
        public void BidForProduct_WhenBid_UserIsLastBidder()
        {
            startupApplication.LogIn("Cata.Brassoi@yahoo.com", "Silvia", 1);

            bidderMenu.BidForProduct(2, 27, "RON");
            bidderMenu.BidForProduct(2, 29.4, "RON");
        }

        /// <summary>Bids for product when bid place price coin is wrong.</summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidCoinException))]
        public void BidForProduct_WhenBid_PlacePriceCoinIsWrong()
        {
            startupApplication.LogIn("Cata.Brassoi@yahoo.com", "Silvia", 1);

            bidderMenu.BidForProduct(2, 27, "EUR");
        }

        /// <summary>Assigns the score to won products when assign score get latest n scores.</summary>
        [TestMethod]
        public void AssignScoreToWonProducts_WhenAssignScore_GetLatestNScores()
        {
            auctionMock.Products.Add(new Product
            {
                ID = 6,
                Name = "Photo Camera",
                Description = "Very good quality.",
                IDState = 1,
                IDUser = 1,
                IDCategory = 1,
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(2),
                StartPrice = 25.34,
                EndPrice = 27.56,
                AuctedUser = 2,
                Specification = "Quality pictures. Good colors.",
                Active = false,
                Coin = "RON"
            });

            auctionMock.Products.Add(new Product
            {
                ID = 7,
                Name = "Photo Camera",
                Description = "Very good quality.",
                IDState = 1,
                IDUser = 1,
                IDCategory = 1,
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(2),
                StartPrice = 25.34,
                EndPrice = 27.56,
                AuctedUser = 2,
                Active = false,
                Specification = "Quality pictures. Good colors.",
                Coin = "RON"
            });

            auctionMock.Products.Add(new Product
            {
                ID = 8,
                Name = "Photo Camera",
                Description = "Very good quality.",
                IDState = 1,
                IDUser = 1,
                IDCategory = 1,
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(2),
                StartPrice = 25.34,
                EndPrice = 31.56,
                AuctedUser = 2,
                Active = false,
                Specification = "Quality pictures. Good colors.",
                Coin = "RON"
            });

            auctionMock.Products.Add(new Product
            {
                ID = 9,
                Name = "Photo Camera",
                Description = "Very good quality.",
                IDState = 1,
                IDUser = 1,
                IDCategory = 1,
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(2),
                StartPrice = 25.34,
                EndPrice = 42.56,
                AuctedUser = 2,
                Active = false,
                Specification = "Quality pictures. Good colors.",
                Coin = "RON"
            });

            startupApplication.LogIn("Cata.Brassoi@yahoo.com", "Silvia", 1);

            bidderMenu.AssignScoreToWonProducts(9.34, 6);
            bidderMenu.AssignScoreToWonProducts(6.25, 7);
            bidderMenu.AssignScoreToWonProducts(1.34, 8);
            bidderMenu.AssignScoreToWonProducts(7.25, 9);

            var userScore = auctionMock.Users
                .SingleOrDefault(x => x.ID == 1);

            Assert.IsTrue(userScore.Score.ToString("F2") == ((7.25 + 1.34 + 6.25 + 5.64333333) / 4).ToString("F2"));
        }

        /// <summary>Assigns the score to won products when assign score product does not exist.</summary>
        [TestMethod]
        [ExpectedException((typeof(InvalidProductException)))]
        public void AssignScoreToWonProducts_WhenAssignScore_ProductDoesNotExist()
        {
            startupApplication.LogIn("Cata.Brassoi@yahoo.com", "Silvia", 1);

            bidderMenu.AssignScoreToWonProducts(9.34, 12);
        }

        /// <summary>Assigns the score to won products when assign score product won by other person.</summary>
        [TestMethod]
        [ExpectedException((typeof(InvalidScoreException)))]
        public void AssignScoreToWonProducts_WhenAssignScore_ProductWonByOtherPerson()
        {
            auctionMock.Products.Add(new Product
            {
                ID = 5,
                Name = "Photo Camera",
                Description = "Very good quality.",
                IDState = 1,
                IDUser = 1,
                IDCategory = 1,
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(2),
                StartPrice = 25.34,
                EndPrice = 27.56,
                Specification = "Quality pictures. Good colors.",
                AuctedUser = 2,
                Active = false,
                Coin = "RON"
            });

            startupApplication.LogIn("Silvia.Brassoi@yahoo.com", "Silvia", 1);

            bidderMenu.AssignScoreToWonProducts(9.34, 1);
        }

        /// <summary>Assigns the score to won products when assign score product already have score.</summary>
        [TestMethod]
        [ExpectedException((typeof(InvalidScoreException)))]
        public void AssignScoreToWonProducts_WhenAssignScore_ProductAlreadyHaveScore()
        {
            auctionMock.Products.Add(new Product
            {
                ID = 6,
                Name = "Photo Camera",
                Description = "Very good quality.",
                IDState = 1,
                IDUser = 1,
                IDCategory = 1,
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(2),
                StartPrice = 25.34,
                EndPrice = 27.56,
                AuctedUser = 2,
                Specification = "Quality pictures. Good colors.",
                Score = 7.5,
                Active = false,
                Coin = "RON"
            });

            startupApplication.LogIn("Cata.Brassoi@yahoo.com", "Silvia", 1);

            bidderMenu.AssignScoreToWonProducts(9.34, 6);
        }

        /// <summary>Assigns the score to won products when assign score to early to assign score.</summary>
        [TestMethod]
        [ExpectedException((typeof(InvalidScoreException)))]
        public void AssignScoreToWonProducts_WhenAssignScore_ToEarlyToAssignScore()
        {
            auctionMock.Products.Add(new Product
            {
                ID = 6,
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
                Specification = "Quality pictures. Good colors.",
                Active = false,
                Coin = "RON"
            });

            startupApplication.LogIn("Cata.Brassoi@yahoo.com", "Silvia", 1);

            bidderMenu.AssignScoreToWonProducts(9.34, 6);
        }

        /// <summary>Assigns the score to won products when assign score user get banned.</summary>
        [TestMethod]
        public void AssignScoreToWonProducts_WhenAssignScore_UserGetBanned()
        {
            auctionMock.Products.Add(new Product
            {
                ID = 6,
                Name = "Photo Camera",
                Description = "Very good quality.",
                IDState = 1,
                IDUser = 1,
                IDCategory = 1,
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(5),
                StartPrice = 25.34,
                EndPrice = 27.56,
                AuctedUser = 2,
                Specification = "Quality pictures. Good colors.",
                Active = false,
                Coin = "RON"
            });

            startupApplication.LogIn("Cata.Brassoi@yahoo.com", "Silvia", 1);

            var user = auctionMock.Users
                .SingleOrDefault(x => x.ID == 1);

            Assert.IsTrue(user.BannedTime == null);

            bidderMenu.AssignScoreToWonProducts(4.23, 6);

            user = auctionMock.Users
                .SingleOrDefault(x => x.ID == 1);

            Assert.IsTrue(user.BannedTime != null);
        }

        /// <summary>Gets the won bidding products when get won bidding products expected exception.</summary>
        [TestMethod]
        [ExpectedException(typeof(NoWonBiddingException))]
        public void GetWonBiddingProducts_WhenGetWonBiddingProducts_ExpectedException()
        {
            startupApplication.LogIn("Cata.Brassoi@yahoo.com", "Silvia", 1);

            bidderMenu.GetWonBiddingProducts();
        }

        /// <summary>Gets the won bidding products when get won bidding products get a list.</summary>
        [TestMethod]
        public void GetWonBiddingProducts_WhenGetWonBiddingProducts_GetAList()
        {
            startupApplication.LogIn("Cata.Brassoi@yahoo.com", "Silvia", 1);

            auctionMock.Products.Add(new Product
            {
                ID = 10,
                Name = "Popcorn",
                Description = "Can be found in different flavors.",
                IDState = 1,
                IDCategory = 1,
                IDUser = 1,
                StartDate = DateTime.Now.AddMinutes(1),
                EndDate = DateTime.Now.AddMinutes(10),
                StartPrice = 25f,
                EndPrice = 32f,
                Active = false,
                Specification = "Quality pictures. Good colors.",
                AuctedUser = 2,
                Coin = "RON"
            });

            int count = bidderMenu.GetWonBiddingProducts().Count;

            Assert.IsTrue(count != 0);
        }

        /// <summary>Gets the active bidding products when get won bidding products expected exception.</summary>
        [TestMethod]
        [ExpectedException(typeof(NoActiveBiddingsException))]
        public void GetActiveBiddingProducts_WhenGetWonBiddingProducts_ExpectedException()
        {
            startupApplication.LogIn("Cata.Brassoi@yahoo.com", "Silvia", 1);

            bidderMenu.GetActiveBiddingProducts();
        }

        /// <summary>Gets the active bidding products when get won bidding products get a list.</summary>
        [TestMethod]
        public void GetActiveBiddingProducts_WhenGetWonBiddingProducts_GetAList()
        {
            startupApplication.LogIn("Cata.Brassoi@yahoo.com", "Silvia", 1);

            auctionMock.Products.Add(new Product
            {
                ID = 10,
                Name = "Popcorn",
                Description = "Can be found in different flavors.",
                IDState = 1,
                IDCategory = 1,
                IDUser = 1,
                StartDate = DateTime.Now.AddMinutes(1),
                EndDate = DateTime.Now.AddMinutes(10),
                StartPrice = 25f,
                EndPrice = 32f,
                Specification = "Quality pictures. Good colors.",
                Active = true,
                AuctedUser = 2,
                Coin = "RON"
            });

            var count = bidderMenu.GetActiveBiddingProducts().Count;

            Assert.IsTrue(count != 0);
        }
    }
}
