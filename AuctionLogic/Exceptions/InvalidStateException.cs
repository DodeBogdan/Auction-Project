//-----------------------------------------------------------------------
// <copyright file="InvalidStateException.cs" company="Transilvania University of Brasov">
//     Copyright (c) Bogdan Gheorghe Nicolae. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AuctionLogic.Exceptions
{
    using System;

    /// <summary>Invalid state exception.</summary>
    [Serializable]
    internal class InvalidStateException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="InvalidStateException" /> class.</summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidStateException(string message) : base(message)
        {
        }
    }
}