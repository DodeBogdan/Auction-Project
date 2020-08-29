//-----------------------------------------------------------------------
// <copyright file="InvalidProductException.cs" company="Transilvania University of Brasov">
//     Copyright (c) Brassoi Silvia Maria. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AuctionLogic.Exceptions
{
    using System;

    /// <summary>InvalidProductException class</summary>
    [Serializable]
    internal class InvalidProductException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="InvalidProductException" /> class.</summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidProductException(string message) : base(message)
        {
        }
    }
}