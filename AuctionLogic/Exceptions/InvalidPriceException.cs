//-----------------------------------------------------------------------
// <copyright file="InvalidPriceException.cs" company="Transilvania University of Brasov">
//     Copyright (c) Brassoi Silvia Maria. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AuctionLogic.Exceptions
{
    using System;

    /// <summary>InvalidPriceException class</summary>
    [Serializable]
    internal class InvalidPriceException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="InvalidPriceException" /> class.</summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidPriceException(string message) : base(message)
        {
        }
    }
}