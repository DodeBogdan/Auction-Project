//-----------------------------------------------------------------------
// <copyright file="NoWonBiddingException.cs" company="Transilvania University of Brasov">
//     Copyright (c) Brassoi Silvia Maria. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AuctionLogic.Exceptions
{
    using System;

    /// <summary>NoWonBiddingException class</summary>
    [Serializable]
    internal class NoWonBiddingException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="NoWonBiddingException" /> class.</summary>
        /// <param name="message">The message that describes the error.</param>
        public NoWonBiddingException(string message) : base(message)
        {
        }
    }
}