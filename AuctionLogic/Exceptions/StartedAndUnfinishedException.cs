//-----------------------------------------------------------------------
// <copyright file="StartedAndUnfinishedException.cs" company="Transilvania University of Brasov">
//     Copyright (c) Brassoi Silvia Maria. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AuctionLogic.Exceptions
{
    using System;

    /// <summary>Started and unfinished exception class</summary>
    [Serializable]
    internal class StartedAndUnfinishedException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="StartedAndUnfinishedException" /> class.</summary>
        /// <param name="message">The message that describes the error.</param>
        public StartedAndUnfinishedException(string message) : base(message)
        {
        }
    }
}