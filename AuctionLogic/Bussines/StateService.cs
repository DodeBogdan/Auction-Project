//-----------------------------------------------------------------------
// <copyright file="StateService.cs" company="Transilvania University of Brasov">
//     Copyright (c) Brassoi Silvia Maria. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AuctionLogic.Bussines
{
    using System.Linq;
    using System.Reflection;
    using log4net;
    using Models;

    /// <summary>State service class</summary>
    public class StateService
    {
        /// <summary>The log</summary>
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>Tests the state.</summary>
        /// <param name="state">The state.</param>
        /// <returns>Return true if state is valid, false if not.</returns>
        public bool TestState(State state)
        {
            Log.Info("TestState() was called.");

            if (state == null)
            {
                return false;
            }

            if (state.Name == null)
            {
                return false;
            }

            if (state.Name.Length == 0)
            {
                return false;
            }

            if ((state.Name.Length < 3) || (state.Name.Length > 50))
            {
                return false;
            }

            if (!state.Name.All(a => char.IsLetter(a) || char.IsWhiteSpace(a) || (a == '-')))
            {
                return false;
            }

            if (char.IsLower(state.Name.First()))
            {
                return false;
            }

            return true;
        }
    }
}
