//-----------------------------------------------------------------------
// <copyright file="StateService.cs" company="Transilvania University of Brasov">
//     Copyright (c) Bogdan Gheorghe Nicolae. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AuctionLogic.Business
{
    using System.Linq;
    using System.Reflection;
    using AuctionLogic.Exceptions;
    using log4net;
    using Models;

    /// <summary>State service class</summary>
    public class StateService
    {
        /// <summary>The log</summary>
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>Tests the state.</summary>
        /// <param name="state">The state.</param>
        /// <exception cref="AuctionLogic.Exceptions.InvalidStateException">
        /// TestState - state can not be null.
        /// or
        /// TestState - state name can not be null.
        /// or
        /// TestState - state name can not be empty.
        /// or
        /// TestState - state name have invalid length.
        /// or
        /// TestState - state name can not contain signs or digits.
        /// or
        /// TestState - state name can not start with lower character.
        /// </exception>
        public void TestState(State state)
        {
            Log.Info("TestState() was called.");
            if (state == null)
            {
                throw new InvalidStateException("TestState - state can not be null.");
            }

            if (state.Name == null)
            {
                throw new InvalidStateException("TestState - state name can not be null.");
            }

            if (state.Name.Length == 0)
            {
                throw new InvalidStateException("TestState - state name can not be empty.");
            }

            if ((state.Name.Length < 3) || (state.Name.Length > 50))
            {
                throw new InvalidStateException("TestState - state name have invalid length.");
            }

            if (!state.Name.All(a => char.IsLetter(a) || char.IsWhiteSpace(a) || (a == '-')))
            {
                throw new InvalidStateException("TestState - state name can not contain signs or digits.");
            }

            if (char.IsLower(state.Name[0]))
            {
                throw new InvalidStateException("TestState - state name can not start with lower character.");
            }
        }
    }
}
