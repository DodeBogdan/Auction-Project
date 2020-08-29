//-----------------------------------------------------------------------
// <copyright file="InvalidRoleStatusException.cs" company="Transilvania University of Brasov">
//     Copyright (c) Brassoi Silvia Maria. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AuctionLogic.Exceptions
{
    using System;

    /// <summary>InvalidRoleStatusException class</summary>
    [Serializable]
    internal class InvalidRoleStatusException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="InvalidRoleStatusException" /> class.</summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidRoleStatusException(string message) : base(message)
        {
        }
    }
}