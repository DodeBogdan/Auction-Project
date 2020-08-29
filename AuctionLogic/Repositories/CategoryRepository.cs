//-----------------------------------------------------------------------
// <copyright file="CategoryRepository.cs" company="Transilvania University of Brasov">
//     Copyright (c) Brassoi Silvia Maria. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AuctionLogic.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Bussines;
    using log4net;
    using Models;

    /// <summary>Category repository class</summary>
    public class CategoryRepository
    {
        /// <summary>The log</summary>
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>The auction</summary>
        private readonly AuctionDB auction;

        /// <summary>The category service</summary>
        private readonly CategoryService categoryService = new CategoryService();

        /// <summary>Initializes a new instance of the <see cref="CategoryRepository" /> class.</summary>
        /// <param name="auction">The auction.</param>
        public CategoryRepository(AuctionDB auction)
        {
            this.auction = auction;
        }

        /// <summary>Adds the category.</summary>
        /// <param name="category">The category.</param>
        /// <returns>Return true if it's all ok.</returns>
        public bool AddCategory(Category category)
        {
            Log.Info("AddCategory wass called.");

            if (categoryService.TestCategory(category))
            {
                auction.Categories.Add(category);
                auction.SaveChanges();
                return true;
            }

            return false;
        }

        /// <summary>Gets the category by identifier.</summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns>Return category.</returns>
        public Category GetCategoryById(int categoryId)
        {
            return auction.Categories
                     .Where(x => x.ID == categoryId)
                     .SingleOrDefault();
        }

        /// <summary>Adds the parent to category.</summary>
        /// <param name="categorySon">The category son.</param>
        /// <param name="categoryParent">The category parent.</param>
        /// <returns>Return true or false.</returns>
        public bool AddParentToCategory(int categorySon, int categoryParent)
        {
            Log.Info("AddParentToCategory wass called.");

            if (categoryService.TestCategoryParent(categorySon, categoryParent) == false)
            {
                return false;
            }

            Category sonCategory = GetCategoryById(categorySon);

            if (sonCategory == null)
            {
                return false;
            }

            List<int> parentCategories = new List<int>();

            foreach (var c in sonCategory.ParentsList)
            {
                if (!parentCategories.Contains(c.ID))
                {
                    parentCategories.Add(c.ID);
                }
            }

            for (int index = 0; index < parentCategories.Count; index++)
            {
                ICollection<Category> cat = GetCategoryById(parentCategories[index]).ParentsList;

                foreach (Category c in cat)
                {
                    if (!parentCategories.Contains(c.ID))
                    {
                        parentCategories.Add(c.ID);
                    }
                }
            }

            if (parentCategories.Contains(categoryParent))
            {
                return false;
            }

            Category parentCategory = GetCategoryById(categoryParent);

            if (parentCategory == null)
            {
                return false;
            }

            sonCategory.ParentsList.Add(parentCategory);
            parentCategory.SonsList.Add(sonCategory);

            auction.SaveChanges();
            return true;
        }
    }
}
