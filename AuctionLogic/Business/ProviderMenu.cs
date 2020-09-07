//-----------------------------------------------------------------------
// <copyright file="ProviderMenu.cs" company="Transilvania University of Brasov">
//     Copyright (c) Bogdan Gheorghe Nicolae. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AuctionLogic.Business
{
    using System;
    using System.Reflection;
    using Exceptions;
    using Help;
    using log4net;
    using Models;
    using Repositories;

    /// <summary>Provider menu class.</summary>
    public class ProviderMenu
    {
        /// <summary>The log</summary>
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>The product repository</summary>
        private readonly ProductRepository productRepository;

        /// <summary>The user repository</summary>
        private readonly UserRepository userRepository;

        /// <summary>Initializes a new instance of the <see cref="ProviderMenu" /> class.</summary>
        /// <param name="productRepository">The product repository.</param>
        /// <param name="userRepository">The user repository.</param>
        public ProviderMenu(ProductRepository productRepository, UserRepository userRepository)
        {
            this.productRepository = productRepository;
            this.userRepository = userRepository;
        }

        /// <summary>Gets the no of products actives of user.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Return number of active products.</returns>
        public int GetNoOfProductsActivesOfUser(int userId)
        {
            Log.Info($"GetNoOfProductsActivesOfUser({userId}) was called");

            return productRepository.GetNoOfProductsActivesOfUser(userId);
        }

        /// <summary>Gets the no of products actives of user by category.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns>Return number of active products by category.</returns>
        public int GetNoOfProductsActivesOfUserByCategory(int userId, int categoryId)
        {
            Log.Info($"GetNoOfProductsActivesOfUserByCategory({userId},{categoryId}) was called");

            return productRepository.GetNoOfProductsActivesOfUserByCategory(userId, categoryId);
        }

        /// <summary>Adds the product.</summary>
        /// <param name="product">The product.</param>
        /// <exception cref="BannedTimeException">You cannot place products while your account is pending.</exception>
        /// <exception cref="StartedAndUnfinishedException">You have too many auctions started and unfinished.</exception>
        /// <exception cref="StartedAndUnfinishedByCategoryException">You have too many started and unfinished auctions based on a category.</exception>
        public void AddProduct(Product product)
        {
            Log.Info($"AddProduct({product.Name}) was called.");

            var user = userRepository.GetActiveUser();

            if (user.BannedTime > DateTime.Now)
            {
                Log.Error("You cannot place products while your account is pending.");

                throw new BannedTimeException("You cannot place products while your account is pending.");
            }

            var noOfProductsActivesOfUser = GetNoOfProductsActivesOfUser(product.IDUser);

            if (noOfProductsActivesOfUser >= ApplicationHelp.StartedAndUnfinishedBids)
            {
                Log.Error("You have too many auctions started and unfinished.");
                throw new StartedAndUnfinishedException("You have too many auctions started and unfinished.");
            }

            var noOfProductsActivesOfUserByCategory = GetNoOfProductsActivesOfUserByCategory(product.IDUser, product.IDCategory);

            if (noOfProductsActivesOfUserByCategory >= ApplicationHelp.StartedAndUnfinishedBidsByCategory)
            {
                Log.Error("You have too many started and unfinished auctions based on a category.");
                throw new StartedAndUnfinishedByCategoryException("You have too many started and unfinished auctions based on a category.");
            }

            product.IDUser = user.ID;

            productRepository.AddProduct(product);
        }

        /// <summary>Sets the product inactive.</summary>
        /// <param name="product">The product.</param>
        /// <exception cref="InvalidProductException">There is no product with that id.</exception>
        /// <exception cref="InvalidUserException">You cannot close an auction that does not belong to you.</exception>
        public void SetProductInactive(Product product)
        {
            Log.Info("SetProductInactive() was called");

            if (product == null)
            {
                Log.Error("There is no product with that id.");
                throw new InvalidProductException("There is no product with that id.");
            }

            if (product.IDUser != userRepository.GetActiveUser().ID)
            {
                Log.Error("You cannot close an auction that does not belong to you");
                throw new InvalidUserException("You cannot close an auction that does not belong to you");
            }

            product.Active = false;

            productRepository.SaveChanges(product);
        }
    }
}
