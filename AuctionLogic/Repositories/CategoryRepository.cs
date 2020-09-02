//-----------------------------------------------------------------------
// <copyright file="CategoryRepository.cs" company="Transilvania University of Brasov">
//     Copyright (c) Bogdan Gheorghe Nicolae. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AuctionLogic.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using AuctionLogic.Exceptions;
    using Business;
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
        public void AddCategory(Category category)
        {
            Log.Info("AddCategory was called.");

            categoryService.TestCategory(category);

            auction.Categories.Add(category);
            auction.SaveChanges();
        }

        /// <summary>Gets the category by identifier.</summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns>Return category.</returns>
        public Category GetCategoryById(int categoryId)
        {
            return auction.Categories
                .SingleOrDefault(x => x.ID == categoryId);
        }

        /// <summary>Adds the parent to category.</summary>
        /// <param name="categorySon">The category son.</param>
        /// <param name="categoryParent">The category parent.</param>
        /// <exception cref="AuctionLogic.Exceptions.InvalidCategoryException">
        /// AddParentToCategory - there are no items with categorySon id.
        /// or
        /// AddParentToCategory - categorySon is already bound with a son of categoryParent
        /// or
        /// AddParentToCategory - there are no items with categoryParent id.
        /// </exception>
        public void AddParentToCategory(int categorySon, int categoryParent)
        {
            Log.Info("AddParentToCategory was called.");

            categoryService.TestCategoryParent(categorySon, categoryParent);

            var sonCategory = GetCategoryById(categorySon);

            if (sonCategory == null)
            {
                throw new InvalidCategoryException("AddParentToCategory - there are no items with categorySon id.");
            }

            List<int> parentCategories = new List<int>();

            foreach (var c in sonCategory.ParentsList)
            {
                if (!parentCategories.Contains(c.ID))
                {
                    parentCategories.Add(c.ID);
                }
            }

            for (var index = 0; index < parentCategories.Count; index++)
            {
                List<Category> cat = GetCategoryById(parentCategories[index]).ParentsList.ToList();

                foreach (var c in cat)
                {
                    if (!parentCategories.Contains(c.ID))
                    {
                        parentCategories.Add(c.ID);
                    }
                }
            }

            if (parentCategories.Contains(categoryParent))
            {
                throw new InvalidCategoryException("AddParentToCategory - categorySon is already bound with a son of categoryParent.");
            }

            Category parentCategory = GetCategoryById(categoryParent);

            if (parentCategory == null)
            {
                throw new InvalidCategoryException("AddParentToCategory - there are no items with categoryParent id.");
            }

            sonCategory.ParentsList.Add(parentCategory);
            parentCategory.SonsList.Add(sonCategory);

            auction.SaveChanges();
        }
    }
}
