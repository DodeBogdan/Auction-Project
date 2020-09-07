//-----------------------------------------------------------------------
// <copyright file="CategoryService.cs" company="Transilvania University of Brasov">
//     Copyright (c) Bogdan Gheorghe Nicolae. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AuctionLogic.Business
{
    using System.Linq;
    using System.Reflection;
    using Exceptions;
    using log4net;
    using Models;

    /// <summary>The category service.</summary>
    public class CategoryService
    {
        /// <summary>The log</summary>
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>Tests the category.</summary>
        /// <param name="category">The category.</param>
        /// <exception cref="AuctionLogic.Exceptions.InvalidCategoryException">
        /// TestCategory - category can not be null.
        /// or
        /// TestCategory - category name can not be null.
        /// or
        /// TestCategory - category name can not be empty.
        /// or
        /// TestCategory - category name have invalid length.
        /// or
        /// TestCategory - category name can not contain signs or digits.
        /// or
        /// TestCategory - category name can not start with lower character.
        /// </exception>
        public void TestCategory(Category category)
        {
            Log.Info("TestCategory() was called");

            if (category == null)
            {
                throw new InvalidCategoryException("TestCategory - category can not be null.");
            }

            if (category.Name == null)
            {
                throw new InvalidCategoryException("TestCategory - category name can not be null.");
            }

            if (category.Name.Length == 0)
            {
                throw new InvalidCategoryException("TestCategory - category name can not be empty.");
            }

            if ((category.Name.Length < 3) || (category.Name.Length > 50))
            {
                throw new InvalidCategoryException("TestCategory - category name have invalid length.");
            }

            if (!category.Name.All(a => char.IsLetter(a) || char.IsWhiteSpace(a) || (a == '-')))
            {
                throw new InvalidCategoryException("TestCategory - category name can not contain signs or digits.");
            }

            if (char.IsLower(category.Name[0]))
            {
                throw new InvalidCategoryException("TestCategory - category name can not start with lower character.");
            }
        }

        /// <summary>Tests the category parent.</summary>
        /// <param name="categorySon">The category son.</param>
        /// <param name="categoryParent">The category parent.</param>
        /// <exception cref="AuctionLogic.Exceptions.InvalidCategoryException">
        /// TestCategoryParent - categorySon can not be smaller than 0.
        /// or
        /// TestCategoryParent - categoryParent can not be smaller than 0.
        /// or
        /// TestCategoryParent - categorySon can not be equal with categoryParent.
        /// </exception>
        public void TestCategoryParent(int categorySon, int categoryParent)
        {
            if (categorySon <= 0)
            {
                throw new InvalidCategoryException("TestCategoryParent - categorySon can not be smaller than 0.");
            }

            if (categoryParent <= 0)
            {
                throw new InvalidCategoryException("TestCategoryParent - categoryParent can not be smaller than 0.");
            }

            if (categorySon == categoryParent)
            {
                throw new InvalidCategoryException("TestCategoryParent - categorySon can not be equal with categoryParent.");
            }
        }
    }
}
