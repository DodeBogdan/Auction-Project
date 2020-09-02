//-----------------------------------------------------------------------
// <copyright file="StartedAndUnfinishedByCategoryException.cs" company="Transilvania University of Brasov">
//     Copyright (c) Bogdan Gheorghe Nicolae. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AuctionLogic.Exceptions
{
    using System;

    /// <summary>Started And Unfinished By Category Exception</summary>
    [Serializable]
    internal class StartedAndUnfinishedByCategoryException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="StartedAndUnfinishedByCategoryException" /> class.</summary>
        /// <param name="message">The message that describes the error.</param>
        public StartedAndUnfinishedByCategoryException(string message) : base(message)
        {
        }
    }
}