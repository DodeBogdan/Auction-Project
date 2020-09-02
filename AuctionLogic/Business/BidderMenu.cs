//-----------------------------------------------------------------------
// <copyright file="BidderMenu.cs" company="Transilvania University of Brasov">
//     Copyright (c) Bogdan Gheorghe Nicolae. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AuctionLogic.Business
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Exceptions;
    using Help;
    using log4net;
    using Models;
    using Repositories;

    /// <summary>The bidder menu class.</summary>
    public class BidderMenu
    {
        /// <summary>The log</summary>
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>The user repository</summary>
        private readonly UserRepository userRepository;

        /// <summary>The product repository</summary>
        private readonly ProductRepository productRepository;

        /// <summary>Initializes a new instance of the <see cref="BidderMenu" /> class.</summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="productRepository">The product repository.</param>
        public BidderMenu(UserRepository userRepository, ProductRepository productRepository)
        {
            this.userRepository = userRepository;
            this.productRepository = productRepository;
        }

        /// <summary>Verifies the role status.</summary>
        /// <exception cref="InvalidRoleStatusException">Only bidders can see the items!</exception>
        public void VerifyRoleStatus()
        {
            var user = userRepository.GetActiveUser();

            if (user.RoleStatus == 2)
            {
                Log.Error("Only bidders can see the items!");
                throw new InvalidRoleStatusException("Only bidders can see the items!");
            }
        }

        /// <summary>Gets the products does not belong to current user.</summary>
        /// <returns>Return a list of product that can be displayed.</returns>
        public List<ShownProduct> GetProductsDoesNotBelongToCurrentUser()
        {
            Log.Info("GetProductsDoesNotBelongToCurrentUser() was called.");

            VerifyRoleStatus();

            return productRepository.GetProductsThatDoesNotBelongToAUser(userRepository.GetActiveUser().ID);
        }

        /// <summary>Gets the product by identifier.</summary>
        /// <param name="productId">The product identifier.</param>
        /// <returns>Return a product.</returns>
        /// <exception cref="InvalidProductException">The selected product does not exist.</exception>
        public Product GetProductById(int productId)
        {
            Log.Info($"GetProductById({productId}) was called.");

            var product = productRepository.GetProductById(productId);

            List<int> list = productRepository.GetProductsThatDoesNotBelongToAUser(userRepository.GetActiveUser().ID)
                .Select(x => x.Id).ToList();

            var ok = false;

            foreach (var x in list)
            {
                if (x == product.ID)
                {
                    ok = true;
                }
            }

            if (ok == false)
            {
                Log.Error("The selected item does not exist.");

                throw new InvalidProductException("The selected item does not exist.");
            }

            return product;
        }

        /// <summary>Gets the won bidding products.</summary>
        /// <returns>Return a list of products.</returns>
        /// <exception cref="NoWonBiddingException">There are no won auctions.</exception>
        public List<Product> GetWonBiddingProducts()
        {
            Log.Info("GetWonBiddingProducts() was called.");

            List<Product> productList = productRepository.GetWonBiddingProducts();

            if (productList.Count == 0)
            {
                Log.Error("There are no won auctions.");

                throw new NoWonBiddingException("There are no won auctions.");
            }

            return productList;
        }

        /// <summary>Assigns the score to won products.</summary>
        /// <param name="score">The score.</param>
        /// <param name="productId">The product identifier.</param>
        /// <exception cref="InvalidProductException">The product does not exist.</exception>
        /// <exception cref="InvalidScoreException">The product was not won by you.
        /// or
        /// You can no longer provide a product score.
        /// or
        /// You can't add the score yet.</exception>
        public void AssignScoreToWonProducts(double score, int productId)
        {
            Log.Info($"AssignScoreToWonProducts({score}, {productId}) was called.");

            Product product = productRepository.GetProductById(productId);

            if (product == null)
            {
                Log.Error("The product does not exist.");
                throw new InvalidProductException("The product does not exist.");
            }

            if (product.AuctedUser != userRepository.GetActiveUser().ID)
            {
                Log.Error("The product was not won by you.");
                throw new InvalidScoreException("The product was not won by you.");
            }

            if (product.Score != null)
            {
                Log.Error("You can no longer provide a product score.");
                throw new InvalidScoreException("You can no longer provide a product score.");
            }

            if (product.EndDate.AddDays(ApplicationHelp.DaysToWait) < DateTime.Now)
            {
                Log.Error("You can't add the score yet.");
                throw new InvalidScoreException("You can't add the score yet.");
            }

            product.Score = score;

            userRepository.ChangeUserScore(product.IDUser);
        }

        /// <summary>Gets the active bidding products.</summary>
        /// <returns>Return a list of products that can be display.</returns>
        /// <exception cref="NoActiveBiddingsException">There are no started auctions .</exception>
        public List<ShownProduct> GetActiveBiddingProducts()
        {
            Log.Info("GetActiveBiddingProducts() was called.");

            List<ShownProduct> productList = productRepository.GetActiveBiddingProducts();

            if (productList.Count == 0)
            {
                Log.Error("There are no started auctions ");
                throw new NoActiveBiddingsException("There are no started auctions ");
            }

            return productList;
        }

        /// <summary>Bids for product.</summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="price">The price.</param>
        /// <param name="coin">The coin.</param>
        /// <exception cref="InvalidUserException">You can't bid for an item you're in the lead.</exception>
        /// <exception cref="InvalidCoinException">The auction currency is not good.</exception>
        /// <exception cref="InvalidPriceException">
        /// The initial price is higher than the auction price.
        /// or
        /// The initial price was exceeded by more than 10%.
        /// or
        /// The price is too low.
        /// or
        /// The price is too high.
        /// </exception>
        public void BidForProduct(int productId, double price, string coin)
        {
            Log.Info($"BidForProduct({productId}, {price}, {coin}) was called");

            Product product = productRepository.GetProductById(productId);

            if (product.AuctedUser == userRepository.GetActiveUser().ID)
            {
                Log.Error("You can't bid for an item you're in the lead.");
                throw new InvalidUserException("You can't bid for an item you're in the lead.");
            }

            if (product.Coin != coin)
            {
                Log.Error("The auction currency is not good.");
                throw new InvalidCoinException("The auction currency is not good.");
            }

            if (product.StartPrice > price)
            {
                Log.Error("The initial price is higher than the auction price.");
                throw new InvalidPriceException("The initial price is higher than the auction price.");
            }

            if (product.EndPrice == null)
            {
                if ((product.StartPrice + (product.StartPrice * 0.1)) < price)
                {
                    Log.Error("The initial price was exceeded by more than 10%.");
                    throw new InvalidPriceException("The initial price was exceeded by more than 10%.");
                }
            }

            if (product.EndPrice != null)
            {
                if (product.EndPrice > price)
                {
                    Log.Error("The price is too low.");
                    throw new InvalidPriceException("The price is too low.");
                }
            }

            if (product.EndPrice != null)
            {
                if ((product.EndPrice + (product.EndPrice * 0.1)) < price)
                {
                    Log.Error("The price is too high.");
                    throw new InvalidPriceException("The price is too high.");
                }
            }

            product.EndPrice = price;
            product.AuctedUser = userRepository.GetActiveUser().ID;

            productRepository.SaveChanges(product);
        }

        /// <summary>Logs the out.</summary>
        public void LogOut()
        {
            Log.Info("LogOut() was called.");

            userRepository.LogOut();
        }
    }
}
