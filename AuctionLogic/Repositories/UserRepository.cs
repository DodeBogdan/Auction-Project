//-----------------------------------------------------------------------
// <copyright file="UserRepository.cs" company="Transilvania University of Brasov">
//     Copyright (c) Bogdan Gheorghe Nicolae. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AuctionLogic.Repositories
{
    using System;
    using System.Collections.Generic;
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
        public void AddUser(User user)
        {
            Log.Info("AddUser was called.");

            userService.TestUser(user);

            auction.Users.Add(user);
            auction.SaveChanges();
        }

        /// <summary>Logs the out.</summary>
        /// <exception cref="UserIsLoggedOutException">The user is not logged in.</exception>
        public void LogOut()
        {
            Log.Info("LogOut was called.");

            var user = auction.Users
                .SingleOrDefault(x => x.Active);

            if (user == null)
            {
                throw new UserIsLoggedOutException("The user is not logged in.");
            }

            user.Active = false;

            auction.SaveChanges();
        }

        /// <summary>Gets the active user.</summary>
        /// <returns>Return active user.</returns>
        public User GetActiveUser()
        {
            Log.Info("GetActiveUser was called.");

            return auction.Users.SingleOrDefault(x => x.Active);
        }

        /// <summary>Changes the user score.</summary>
        /// <param name="idUser">The identifier user.</param>
        /// <exception cref="InvalidProductException">There are no finished products.</exception>
        /// <exception cref="InvalidUserException">User does not exist.</exception>
        public void ChangeUserScore(int idUser)
        {
            Log.Info("ChangeUserScore was called.");

            List<double> scoreList = auction.Products
                .Where(x => x.Score != null)
                .Select(x => x.Score.Value)
                .ToList();

            if (scoreList.Count == 0)
            {
                Log.Error("There are no finished products.");
                throw new InvalidProductException("There are no finished products.");
            }

            var user = auction.Users
                .SingleOrDefault(x => x.ID == idUser);

            if (user == null)
            {
                throw new InvalidUserException("User does not exist.");
            }

            var number = 0;

            double sum = 0;

            int index;

            for (index = scoreList.Count - 1; index >= 0 && number < ApplicationHelp.LastNScores; index--)
            {
                sum += scoreList[index];
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
