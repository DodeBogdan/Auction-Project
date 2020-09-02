//-----------------------------------------------------------------------
// <copyright file="StateRepository.cs" company="Transilvania University of Brasov">
//     Copyright (c) Bogdan Gheorghe Nicolae. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AuctionLogic.Repositories
{
    using System.Reflection;
    using Business;
    using log4net;
    using Models;

    /// <summary>State repository class.</summary>
    public class StateRepository
    {
        /// <summary>The log</summary>
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>The auction</summary>
        private readonly AuctionDB auction;

        /// <summary>The state service</summary>
        private readonly StateService stateService = new StateService();

        /// <summary>Initializes a new instance of the <see cref="StateRepository" /> class.</summary>
        /// <param name="auction">The auction.</param>
        public StateRepository(AuctionDB auction)
        {
            this.auction = auction;
        }

        /// <summary>Adds the state.</summary>
        /// <param name="state">The state.</param>
        /// <returns>Return the state of test.</returns>
        public bool AddState(State state)
        {
            Log.Info("AddState was called.");

            if (stateService.TestState(state))
            {
                auction.States.Add(state);
                auction.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
