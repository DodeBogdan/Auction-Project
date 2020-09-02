//-----------------------------------------------------------------------
// <copyright file="BannedTimeException.cs" company="Transilvania University of Brasov">
//     Copyright (c) Bogdan Gheorghe Nicolae. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AuctionLogic.Exceptions
{
    using System;

    /// <summary>Banned time exception class.</summary>
    [Serializable]
    internal class BannedTimeException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="BannedTimeException" /> class.</summary>
        /// <param name="message">The message that describes the error.</param>
        public BannedTimeException(string message) : base(message)
        {
        }
    }
}