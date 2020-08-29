//-----------------------------------------------------------------------
// <copyright file="InvalidPasswordException.cs" company="Transilvania University of Brasov">
//     Copyright (c) Brassoi Silvia Maria. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AuctionLogic.Exceptions
{
    using System;

    /// <summary>Invalid password exception class.</summary>
    [Serializable]
    internal class InvalidPasswordException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="InvalidPasswordException" /> class.</summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidPasswordException(string message) : base(message)
        {
        }
    }
}