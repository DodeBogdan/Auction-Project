//-----------------------------------------------------------------------
// <copyright file="UserIsLoggedOutException.cs" company="Transilvania University of Brasov">
//     Copyright (c) Brassoi Silvia Maria. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AuctionLogic.Exceptions
{
    using System;

    /// <summary>User is logged out exception.</summary>
    [Serializable]
    internal class UserIsLoggedOutException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="UserIsLoggedOutException" /> class.</summary>
        /// <param name="message">The message that describes the error.</param>
        public UserIsLoggedOutException(string message) : base(message)
        {
        }
    }
}