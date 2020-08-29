//-----------------------------------------------------------------------
// <copyright file="NoActiveBiddingsException.cs" company="Transilvania University of Brasov">
//     Copyright (c) Brassoi Silvia Maria. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AuctionLogic.Exceptions
{
    using System;

    /// <summary>NoActiveBiddingsException class</summary>
    [Serializable]
    internal class NoActiveBiddingsException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="NoActiveBiddingsException" /> class.</summary>
        /// <param name="message">The message that describes the error.</param>
        public NoActiveBiddingsException(string message) : base(message)
        {
        }
    }
}