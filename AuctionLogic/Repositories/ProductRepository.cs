//-----------------------------------------------------------------------
// <copyright file="ProductRepository.cs" company="Transilvania University of Brasov">
//     Copyright (c) Bogdan Gheorghe Nicolae. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AuctionLogic.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Business;
    using log4net;
    using Models;

    /// <summary>Product repository class.</summary>
    public class ProductRepository
    {
        /// <summary>The log</summary>
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>The auction</summary>
        private readonly AuctionDB auction;

        /// <summary>The product service</summary>
        private readonly ProductService productService = new ProductService();

        /// <summary>Initializes a new instance of the <see cref="ProductRepository" /> class.</summary>
        /// <param name="auction">The auction.</param>
        public ProductRepository(AuctionDB auction)
        {
            this.auction = auction;
        }

        /// <summary>Adds the product.</summary>
        /// <param name="product">The product.</param>
        /// <returns>Return true if it's all ok.</returns>
        public bool AddProduct(Product product)
        {
            Log.Info("AddProduct was called.");

            if (productService.TestProduct(product))
            {
                auction.Products.Add(product);
                auction.SaveChanges();
                return true;
            }

            return false;
        }

        /// <summary>Gets the no of products actives of user.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Return number of active product by user.</returns>
        public int GetNoOfProductsActivesOfUser(int userId)
        {
            Log.Info("GetNoOfProductsActivesOfUser was called.");

            return auction.Products
                .Count(x => x.IDUser == userId && x.Active);
        }

        /// <summary>Gets the no of products actives of user by category.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns>Return number of active product by user by category.</returns>
        public int GetNoOfProductsActivesOfUserByCategory(int userId, int categoryId)
        {
            Log.Info("GetNoOfProductsActivesOfUserByCategory was called.");

            return auction.Products
                .Count(x => x.IDUser == userId && x.Active && x.IDCategory == categoryId);
        }

        /// <summary>Gets the products that does not belong to a user.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Return a list of products that can be displayed.</returns>
        public List<ShownProduct> GetProductsThatDoesNotBelongToAUser(int userId)
        {
            Log.Info("GetProductsThatDoesNotBelongToAUser was called.");

            return auction.Products
                 .Where(x => x.Active && x.IDUser != userId)
                 .Select(x => new ShownProduct { Id = x.ID, Name = x.Name, Description = x.Description, Price = x.StartPrice })
                 .ToList();
        }

        /// <summary>Gets the product by identifier.</summary>
        /// <param name="productId">The product identifier.</param>
        /// <returns>Return product by id.</returns>
        public Product GetProductById(int productId)
        {
            Log.Info("GetProductById was called.");

            return auction.Products
                .SingleOrDefault(x => x.ID == productId);
        }

        /// <summary>Gets the active bidding products.</summary>
        /// <returns>Return number of active bids.</returns>
        public List<ShownProduct> GetActiveBiddingProducts()
        {
            Log.Info("GetActiveBiddingProducts was called.");

            var currentUser = auction.Users
                .Where(x => x.Active)
                .Select(x => x.ID)
                .SingleOrDefault();

            return auction.Products
                .Where(x => x.Active && x.AuctedUser == currentUser)
                .Select(x => new ShownProduct { Id = x.ID, Name = x.Name, Description = x.Description, Price = (double)x.EndPrice })
                .ToList();
        }

        /// <summary>Gets the won bidding products.</summary>
        /// <returns>Return list of won product by user.</returns>
        public List<Product> GetWonBiddingProducts()
        {
            Log.Info("GetWonBiddingProducts was called.");

            var currentUser = auction.Users
                .Where(x => x.Active)
                .Select(x => x.ID)
                .SingleOrDefault();

            return auction.Products
                .Where(x => x.Active == false && x.AuctedUser == currentUser)
                .ToList();
        }

        /// <summary>Saves the changes.</summary>
        /// <param name="product">The product.</param>
        public void SaveChanges(Product product)
        {
            Log.Info("SaveChanges was called.");

            var currentProduct = auction.Products
                .SingleOrDefault(x => x.ID == product.ID);

            currentProduct = product;

            auction.SaveChanges();
        }
    }
}
