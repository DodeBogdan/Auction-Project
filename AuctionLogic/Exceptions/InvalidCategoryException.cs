//-----------------------------------------------------------------------
// <copyright file="InvalidCategoryException.cs" company="Transilvania University of Brasov">
//     Copyright (c) Bogdan Gheorghe Nicolae. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace AuctionLogic.Exceptions
{
    /// <summary>Invalid Category Exception</summary>
    [Serializable]
    internal class InvalidCategoryException : Exception
    {
        /// <summary>Initializes a new instance of the <a onclick="return false;" href="InvalidCategoryException" originaltag="see">InvalidCategoryException</a> class.</summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidCategoryException(string message) : base(message)
        {
        }
    }
}