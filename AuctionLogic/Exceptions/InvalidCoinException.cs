//-----------------------------------------------------------------------
// <copyright file="InvalidCoinException.cs" company="Transilvania University of Brasov">
//     Copyright (c) Brassoi Silvia Maria. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AuctionLogic.Exceptions
{
    using System;

    /// <summary>Invalid coin exception class.</summary>
    [Serializable]
    internal class InvalidCoinException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="InvalidCoinException" /> class.</summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidCoinException(string message) : base(message)
        {
        }
    }
}