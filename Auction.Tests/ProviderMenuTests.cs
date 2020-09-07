//-----------------------------------------------------------------------
// <copyright file="ProviderMenuTests.cs" company="Transilvania University of Brasov">
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

    /// <summary>Provider menu tests.</summary>
    [TestClass]
    public class ProviderMenuTests
    {
        /// <summary>The auction mock</summary>
        private AuctionDB auctionMock;

        /// <summary>The startup application</summary>
        private StartupApplication startupApplication;

        /// <summary>The provider menu</summary>
        private ProviderMenu providerMenu;

        /// <summary>The role repository</summary>
        private RoleRepository roleRepository;

        /// <summary>The category repository</summary>
        private CategoryRepository categoryRepository;

        /// <summary>The state repository</summary>
        private StateRepository stateRepository;

        /// <summary>The product repository</summary>
        private ProductRepository productRepository;

        /// <summary>The user repository</summary>
        private UserRepository userRepository;

        /// <summary>Sets up.</summary>
        [TestInitialize]
        public void SetUp()
        {
            auctionMock = EntityFrameworkMock.Create<AuctionDB>();
            startupApplication = new StartupApplication(auctionMock);
            roleRepository = new RoleRepository(auctionMock);
            categoryRepository = new CategoryRepository(auctionMock);
            stateRepository = new StateRepository(auctionMock);
            productRepository = new ProductRepository(auctionMock);
            userRepository = new UserRepository(auctionMock);
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
                    Gender = "M",
                    CNP = "1123456789123",
                    Adress = "Mandra nr. 61",
                    Phone = "0762956316",
                    RoleStatus = 2,
                    Active = false
                });
        }

        /// <summary>Adds the product add valid product get one product.</summary>
        [TestMethod]
        public void AddProduct_AddValidProduct_GetOneProduct()
        {
            startupApplication.LogIn("Silvia.Brassoi@yahoo.com", "Silvia", 2);

            Product product = new Product
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
                Active = true,
                Specification = "May contain traces of gluten, peanuts, hazelnuts.",
                Coin = "RON"
            };

            providerMenu.AddProduct(product);

            Assert.IsTrue(auctionMock.Products.Count() == 1);
        }

        /// <summary>Adds the product add to many products expected exception.</summary>
        [TestMethod]
        [ExpectedException(typeof(StartedAndUnfinishedException))]
        public void AddProduct_AddToManyProducts_ExpectedException()
        {
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
                Active = true,
                Specification = "May contain traces of gluten, peanuts, hazelnuts.",
                Coin = "RON"
            });

            providerMenu.AddProduct(new Product
            {
                ID = 2,
                Name = "Sweetcorn",
                Description = "Can be found in different flavors.",
                IDState = 1,
                IDCategory = 2,
                IDUser = 1,
                StartDate = DateTime.Now.AddMinutes(1),
                EndDate = DateTime.Now.AddMinutes(10),
                StartPrice = 25f,
                Active = true,
                Specification = "May contain traces of gluten, peanuts, hazelnuts.",
                Coin = "RON"
            });

            providerMenu.AddProduct(new Product
            {
                ID = 3,
                Name = "Pickles",
                Description = "Can be found in different flavors.",
                IDState = 1,
                IDCategory = 1,
                IDUser = 1,
                StartDate = DateTime.Now.AddMinutes(1),
                EndDate = DateTime.Now.AddMinutes(10),
                StartPrice = 25f,
                Active = true,
                Specification = "May contain traces of gluten, peanuts, hazelnuts.",
                Coin = "RON"
            });

            providerMenu.AddProduct(new Product
            {
                ID = 4,
                Name = "Biscuits",
                Description = "Can be found in different flavors.",
                IDState = 1,
                IDCategory = 3,
                IDUser = 1,
                StartDate = DateTime.Now.AddMinutes(1),
                EndDate = DateTime.Now.AddMinutes(10),
                StartPrice = 25f,
                Active = true,
                Specification = "May contain traces of gluten, peanuts, hazelnuts.",
                Coin = "RON"
            });
        }

        /// <summary>Adds the product add to many products by category expected exception.</summary>
        [TestMethod]
        [ExpectedException(typeof(StartedAndUnfinishedByCategoryException))]
        public void AddProduct_AddToManyProductsByCategory_ExpectedException()
        {
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
                Active = true,
                Specification = "May contain traces of gluten, peanuts, hazelnuts.",
                Coin = "RON"
            });

            providerMenu.AddProduct(new Product
            {
                ID = 2,
                Name = "Sausages",
                Description = "Can be found in different flavors.",
                IDState = 1,
                IDCategory = 1,
                IDUser = 1,
                StartDate = DateTime.Now.AddMinutes(1),
                EndDate = DateTime.Now.AddMinutes(10),
                StartPrice = 25f,
                Active = true,
                Specification = "May contain traces of gluten, peanuts, hazelnuts.",
                Coin = "RON"
            });

            providerMenu.AddProduct(new Product
            {
                ID = 3,
                Name = "Chicken breasts",
                Description = "Can be found in different flavors.",
                IDState = 1,
                IDCategory = 1,
                IDUser = 1,
                StartDate = DateTime.Now.AddMinutes(1),
                EndDate = DateTime.Now.AddMinutes(10),
                StartPrice = 25f,
                Active = true,
                Specification = "May contain traces of gluten, peanuts, hazelnuts.",
                Coin = "RON"
            });
        }

        /// <summary>Sets the product inactive when close bidding product does not exist.</summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidProductException))]
        public void SetProductInactive_WhenCloseBidding_ProductDoesNotExist()
        {
            startupApplication.LogIn("Silvia.Brassoi@yahoo.com", "Silvia", 2);

            var product = productRepository.GetProductById(2);

            providerMenu.SetProductInactive(product);
        }

        /// <summary>Sets the product inactive when close bidding product cannot be set by other user.</summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidUserException))]
        public void SetProductInactive_WhenCloseBidding_ProductCannotBeSetByOtherUser()
        {
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
                Active = true,
                Specification = "May contain traces of gluten, peanuts, hazelnuts.",
                Coin = "RON"
            });

            userRepository.LogOut();

            startupApplication.Register(
               new User
               {
                   ID = 2,
                   FirstName = "Cata",
                   LastName = "Brassoi",
                   Email = "Cata.Brassoi@yahoo.com",
                   Password = "Silvia",
                   Age = 23,
                   RoleStatus = 1,
                   Active = false
               });

            startupApplication.LogIn("Cata.Brassoi@yahoo.com", "Silvia", 2);

            var product = productRepository.GetProductById(1);

            providerMenu.SetProductInactive(product);
        }

        /// <summary>Sets the product inactive when close bidding product turn active to false.</summary>
        [TestMethod]
        public void SetProductInactive_WhenCloseBidding_ProductTurnActiveToFalse()
        {
            auctionMock.Products.Add(new Product
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
                Active = true,
                Specification = "May contain traces of gluten, peanuts, hazelnuts.",
                Coin = "RON"
            });

            startupApplication.LogIn("Silvia.Brassoi@yahoo.com", "Silvia", 2);

            var product = productRepository.GetProductById(1);

            providerMenu.SetProductInactive(product);
        }

        /// <summary>Adds the product when user is banned throw exception.</summary>
        [TestMethod]
        [ExpectedException(typeof(BannedTimeException))]
        public void AddProduct_WhenUserIsBanned_ThrowException()
        {
            auctionMock.Users.Add(
               new User
               {
                   ID = 3,
                   FirstName = "Dode",
                   LastName = "Brassoi",
                   Email = "Dode.Brassoi@yahoo.com",
                   Password = "Silvia",
                   Age = 23,
                   RoleStatus = 2,
                   BannedTime = DateTime.Now.AddDays(2),
                   Active = true
               });

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
                Active = true,
                Coin = "RON"
            });
        }

        /// <summary>Adds the product when user is banned banned has expired.</summary>
        [TestMethod]
        public void AddProduct_WhenUserIsBanned_BannedHasExpired()
        {
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
                   Phone = "0761324567",
                   RoleStatus = 2,
                   BannedTime = DateTime.Now.AddDays(-5),
                   Active = true
               });

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
                Active = true,
                Specification = "May contain traces of gluten, peanuts, hazelnuts.",
                Coin = "RON"
            });
        }
    }
}
