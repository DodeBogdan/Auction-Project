//-----------------------------------------------------------------------
// <copyright file="RoleRepository.cs" company="Transilvania University of Brasov">
//     Copyright (c) Bogdan Gheorghe Nicolae. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AuctionLogic.Repositories
{
    using System.Reflection;
    using Business;
    using log4net;
    using Models;

    /// <summary>Role repository class.</summary>
    public class RoleRepository
    {
        /// <summary>The log</summary>
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>The auction</summary>
        private readonly AuctionDB auction;

        /// <summary>The role service</summary>
        private readonly RoleService roleService = new RoleService();

        /// <summary>Initializes a new instance of the <see cref="RoleRepository" /> class.</summary>
        /// <param name="auction">The auction.</param>
        public RoleRepository(AuctionDB auction)
        {
            this.auction = auction;
        }

        /// <summary>Adds the role.</summary>
        /// <param name="role">The role.</param>
        /// <returns>Return the state of test.</returns>
        public bool AddRole(Role role)
        {
            Log.Info("AddRole was called.");

            if (roleService.TestRole(role))
            {
                auction.Roles.Add(role);
                auction.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
