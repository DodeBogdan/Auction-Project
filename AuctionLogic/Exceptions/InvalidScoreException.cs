//-----------------------------------------------------------------------
// <copyright file="InvalidScoreException.cs" company="Transilvania University of Brasov">
//     Copyright (c) Brassoi Silvia Maria. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AuctionLogic.Exceptions
{
    using System;

    /// <summary>InvalidScoreException class</summary>
    [Serializable]
    internal class InvalidScoreException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="InvalidScoreException" /> class.</summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidScoreException(string message) : base(message)
        {
        }
    }
}