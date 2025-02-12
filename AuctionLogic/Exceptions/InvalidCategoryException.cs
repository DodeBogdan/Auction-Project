﻿//-----------------------------------------------------------------------
// <copyright file="InvalidCategoryException.cs" company="Transilvania University of Brasov">
//     Copyright (c) Bogdan Gheorghe Nicolae. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AuctionLogic.Exceptions
{
    using System;

    /// <summary>Invalid Category Exception</summary>
    [Serializable]
    internal class InvalidCategoryException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="InvalidCategoryException" /> class.</summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidCategoryException(string message) : base(message)
        {
        }
    }
}