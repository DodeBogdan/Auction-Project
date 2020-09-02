//-----------------------------------------------------------------------
// <copyright file="UserRepository.cs" company="Transilvania University of Brasov">
//     Copyright (c) Bogdan Gheorghe Nicolae. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AuctionLogic.Repositories
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Business;
    using Exceptions;
    using Help;
    using log4net;
    using Models;

    /// <summary>User repository class.</summary>
    public class UserRepository
    {
        /// <summary>The log</summary>
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>The auction</summary>
        private readonly AuctionDB auction;

        /// <summary>The user service</summary>
        private readonly UserService userService = new UserService();

        /// <summary>Initializes a new instance of the <see cref="UserRepository" /> class.</summary>
        /// <param name="auction">The auction.</param>
        public UserRepository(AuctionDB auction)
        {
            this.auction = auction;
        }

        /// <summary>Adds the user.</summary>
        /// <param name="user">The user.</param>
        /// <returns>Return the state of test.</returns>
        public bool AddUser(User user)
        {
            Log.Info("AddUser was called.");

            if (userService.TestUser(user))
            {
                auction.Users.Add(user);
                auction.SaveChanges();
                return true;
            }

            return false;
        }

        /// <summary>Logs the out.</summary>
        /// <exception cref="UserIsLoggedOutException">The user is not logged in.</exception>
        public void LogOut()
        {
            Log.Info("LogOut was called.");

            var user = auction.Users
                .Where(x => x.Active)
                .SingleOrDefault();

            if (user == null)
            {
                throw new UserIsLoggedOutException("Utilizatorul nu este conectat.");
            }

            user.Active = false;

            auction.SaveChanges();
        }

        /// <summary>Gets the active user.</summary>
        /// <returns>Return active user.</returns>
        public User GetActiveUser()
        {
            Log.Info("GetActiveUser was called.");

            return auction.Users
                 .Where(x => x.Active).SingleOrDefault();
        }

        /// <summary>Changes the user score.</summary>
        /// <param name="idUser">The i d user.</param>
        /// <exception cref="InvalidProductException">There are no finished products.</exception>
        public void ChangeUserScore(int idUser)
        {
            Log.Info("ChangeUserScore was called.");

            var scoreList = auction.Products
                .Where(x => x.Score != null)
                .Select(x => x.Score)
                .ToList();

            if (scoreList.Count == 0)
            {
                Log.Error("Nu sunt produse finalizate");
                throw new InvalidProductException("Nu sunt produse finalizate");
            }

            var user = auction.Users
                .Where(x => x.ID == idUser)
                .SingleOrDefault();

            int number = 0;
            double sum = 0;
            int index;
            for (index = scoreList.Count - 1; index >= 0 && number < ApplicationHelp.LastNScores; index--)
            {
                sum += (double)scoreList[index];
                number++;
            }

            if (index >= 0)
            {
                number++;
                sum += user.Score;
            }

            user.Score = sum / number;

            if (user.Score < ApplicationHelp.MinimumScore)
            {
                user.BannedTime = DateTime.Now.AddDays(ApplicationHelp.BannedDays);
            }

            auction.SaveChanges();
        }
    }
}
