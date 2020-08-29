//-----------------------------------------------------------------------
// <copyright file="UserLoggedInException.cs" company="Transilvania University of Brasov">
//     Copyright (c) Brassoi Silvia Maria. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AuctionLogic.Exceptions
{
    using System;

    /// <summary>User logged in exception class</summary>
    [Serializable]
    internal class UserLoggedInException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="UserLoggedInException">UserLoggedInException</see> class.</summary>
        /// <param name="message">The message that describes the error.</param>
        public UserLoggedInException(string message) : base(message)
        {
        }
    }
}