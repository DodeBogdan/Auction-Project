//-----------------------------------------------------------------------
// <copyright file="CategoryService.cs" company="Transilvania University of Brasov">
//     Copyright (c) Brassoi Silvia Maria. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AuctionLogic.Bussines
{
    using System.Linq;
    using System.Reflection;
    using log4net;
    using Models;

    /// <summary>The category service.</summary>
    public class CategoryService
    {
        /// <summary>The log</summary>
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>Tests the category.</summary>
        /// <param name="category">The category.</param>
        /// <returns>Return true if category is valid, false if not.</returns>
        public bool TestCategory(Category category)
        {
            Log.Info("TestCategory() was called");

            if (category?.Name == null)
            {
                return false;
            }

            if (category.Name.Length == 0)
            {
                return false;
            }

            if ((category.Name.Length < 3) || (category.Name.Length > 50))
            {
                return false;
            }

            if (!category.Name.All(a => char.IsLetter(a) || char.IsWhiteSpace(a) || (a == '-')))
            {
                return false;
            }

            if (char.IsLower(category.Name.First()))
            {
                return false;
            }

            return true;
        }

        /// <summary>Tests the category parent.</summary>
        /// <param name="categorySon">The category son.</param>
        /// <param name="categoryParent">The category parent.</param>
        /// <returns>Return true or false.</returns>
        public bool TestCategoryParent(int categorySon, int categoryParent)
        {
            if (categorySon < 0)
            {
                return false;
            }

            if (categoryParent < 0)
            {
                return false;
            }

            if (categorySon == categoryParent)
            {
                return false;
            }

            return true;
        }
    }
}
