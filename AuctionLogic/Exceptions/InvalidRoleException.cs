//-----------------------------------------------------------------------
// <copyright file="InvalidRoleException.cs" company="Transilvania University of Brasov">
//     Copyright (c) Bogdan Gheorghe Nicolae. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AuctionLogic.Exceptions
{
    using System;

    /// <summary>Invalid role exception</summary>
    [Serializable]
    internal class InvalidRoleException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="InvalidRoleException" /> class.</summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidRoleException(string message) : base(message)
        {
        }
    }
}