//-----------------------------------------------------------------------
// <copyright file="InvalidUserException.cs" company="Transilvania University of Brasov">
//     Copyright (c) Bogdan Gheorghe Nicolae. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AuctionLogic.Exceptions
{
    using System;

    /// <summary>InvalidUserException class</summary>
    [Serializable]
    internal class InvalidUserException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="InvalidUserException" /> class.</summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidUserException(string message) : base(message)
        {
        }
    }
}