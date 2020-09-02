//-----------------------------------------------------------------------
// <copyright file="StartupApplication.cs" company="Transilvania University of Brasov">
//     Copyright (c) Bogdan Gheorghe Nicolae. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AuctionLogic.Business
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Exceptions;
    using log4net;
    using Models;
    using Repositories;

    /// <summary>Startup application class</summary>
    public class StartupApplication
    {
        /// <summary>The log</summary>
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>The auction database</summary>
        private readonly AuctionDB auctionDB;

        /// <summary>The user repository</summary>
        private readonly UserRepository userRepository;

        /// <summary>Initializes a new instance of the <see cref="StartupApplication" /> class.</summary>
        /// <param name="auctionDB">The auction database.</param>
        public StartupApplication(AuctionDB auctionDB)
        {
            this.auctionDB = auctionDB;
            userRepository = new UserRepository(auctionDB);

            Refresh();
        }

        /// <summary>Logs the in.</summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <param name="role">The role.</param>
        /// <exception cref="InvalidRoleStatusException">Invalid role.</exception>
        /// <exception cref="InvalidUserException">User not found.</exception>
        /// <exception cref="UserLoggedInException">The user is already logged in.</exception>
        /// <exception cref="InvalidPasswordException">Username and password do not match.</exception>
        public void LogIn(string email, string password, int role)
        {
            Log.Info($"LogIn({email},*,{role}) was called.");

            if (role <= 0 || role > 2)
            {
                Log.Error("Invalid role.");
                throw new InvalidRoleStatusException("Invalid role.");
            }

            var currentUser = auctionDB.Users.FirstOrDefault(x => x.Email == email);

            if (currentUser == null)
            {
                Log.Error("User not found.");
                throw new InvalidUserException("User not found.");
            }

            if (currentUser.Active)
            {
                Log.Error("The user is already logged in.");
                throw new UserLoggedInException("The user is already logged in.");
            }

            if (currentUser.Password.Trim() != password)
            {
                Log.Error("Username and password do not match.");
                throw new InvalidPasswordException("Username and password do not match.");
            }

            currentUser.Active = true;
            currentUser.RoleStatus = role;
            auctionDB.SaveChanges();
        }

        /// <summary>Registers the specified user.</summary>
        /// <param name="user">The user.</param>
        public void Register(User user)
        {
            Log.Info($"Register({user.FirstName}) was called.");
            userRepository.AddUser(user);
        }

        /// <summary>
        ///   <para>
        ///  Refreshes this instance.
        /// </para>
        /// </summary>
        public void Refresh()
        {
            IQueryable<Product> products = auctionDB.Products
                .Where(x => x.EndDate < DateTime.Now);

            foreach (Product p in products)
            {
                p.Active = false;
            }

            auctionDB.SaveChanges();
        }
    }
}
