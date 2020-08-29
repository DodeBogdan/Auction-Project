//-----------------------------------------------------------------------
// <copyright file="ProductService.cs" company="Transilvania University of Brasov">
//     Copyright (c) Brassoi Silvia Maria. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AuctionLogic.Bussines
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Help;
    using log4net;
    using Models;

    /// <summary>Product service class.</summary>
    public class ProductService
    {
        /// <summary>The log</summary>
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>Tests the product.</summary>
        /// <param name="product">The product.</param>
        /// <returns>Return true if product is valid, false if not.</returns>
        internal bool TestProduct(Product product)
        {
            Log.Info("TestProduct() was called.");

            if (product?.Name == null)
            {
                return false;
            }

            if (product.Name.Length == 0)
            {
                return false;
            }

            if ((product.Name.Length < 3) || (product.Name.Length > 50))
            {
                return false;
            }

            if (!product.Name.All(a => char.IsLetter(a) || char.IsWhiteSpace(a) || (a == '-')))
            {
                return false;
            }

            if (char.IsLower(product.Name.First()))
            {
                return false;
            }

            if (product.Description == null)
            {
                return false;
            }

            if (product.Description.Length == 0)
            {
                return false;
            }

            if ((product.Description.Length < 15) || (product.Description.Length > 100))
            {
                return false;
            }

            if (char.IsLower(product.Description.First()))
            {
                return false;
            }

            if (!product.Description.All(a => char.IsLetter(a) || char.IsWhiteSpace(a) || (a == '-') || (a == '.') || (a == ',')))
            {
                return false;
            }

            if (product.IDState == 0)
            {
                return false;
            }

            if (product.IDState < 0 || product.IDState > 5)
            {
                return false;
            }

            if (product.IDCategory == 0)
            {
                return false;
            }

            if (product.IDCategory < 0)
            {
                return false;
            }

            if (product.IDUser == 0)
            {
                return false;
            }

            if (product.IDUser < 0)
            {
                return false;
            }

            DateTime now = DateTime.Now;

            if (product.StartDate == null)
            {
                return false;
            }

            if (product.StartDate < now)
            {
                return false;
            }

            if (product.StartDate > now.AddMonths(4))
            {
                return false;
            }

            if (product.EndDate == null)
            {
                return false;
            }

            if (product.EndDate < product.StartDate)
            {
                return false;
            }

            if (product.EndDate > product.StartDate.AddMonths(4))
            {
                return false;
            }

            if (product.StartPrice == 0)
            {
                return false;
            }

            if (product.StartPrice < ApplicationHelp.Step)
            {
                return false;
            }

            if (product.Coin == null)
            {
                return false;
            }

            if (product.Coin.Length == 0)
            {
                return false;
            }

            if ((product.Coin.Length <= 2) || (product.Coin.Length > 30))
            {
                return false;
            }

            if (!product.Coin.All(char.IsLetter))
            {
                return false;
            }

            if (product.Coin.All(char.IsLower))
            {
                return false;
            }

            if (product.Specification == null)
            {
                return false;
            }

            if (product.Specification.Length == 0)
            {
                return false;
            }

            if ((product.Specification.Length < 10) || (product.Specification.Length > 100))
            {
                return false;
            }

            if (char.IsLower(product.Specification.First()))
            {
                return false;
            }

            return product.Specification.All(a => char.IsLetter(a) || char.IsWhiteSpace(a) || (a == '-') || (a == '.') || (a == ','));
        }
    }
}
