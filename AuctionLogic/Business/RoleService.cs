//-----------------------------------------------------------------------
// <copyright file="RoleService.cs" company="Transilvania University of Brasov">
//     Copyright (c) Bogdan Gheorghe Nicolae. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AuctionLogic.Business
{
    using System.Linq;
    using System.Reflection;
    using Exceptions;
    using log4net;
    using Models;

    /// <summary>
    ///   <para>Role service class</para>
    /// </summary>
    public class RoleService
    {
        /// <summary>The log</summary>
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>Tests the role.</summary>
        /// <param name="role">The role.</param>
        /// <exception cref="InvalidRoleException">
        /// TestRole - role can not be null.
        /// or
        /// TestRole - role name can not be null.
        /// or
        /// TestRole - role name can not be empty.
        /// or
        /// TestRole - role name have invalid length.
        /// or
        /// TestRole - role name can not contain signs or digits.
        /// or
        /// TestRole - role name can not start with lower character.
        /// </exception>
        public void TestRole(Role role)
        {
            Log.Info("TestRole() was called.");

            if (role == null)
            {
                throw new InvalidRoleException("TestRole - role can not be null.");
            }

            if (role.RoleName == null)
            {
                throw new InvalidRoleException("TestRole - role name can not be null.");
            }

            if (role.RoleName.Length == 0)
            {
                throw new InvalidRoleException("TestRole - role name can not be empty.");
            }

            if ((role.RoleName.Length < 3) || (role.RoleName.Length > 50))
            {
                throw new InvalidRoleException("TestRole - role name have invalid length.");
            }

            if (!role.RoleName.All(a => char.IsLetter(a) || char.IsWhiteSpace(a) || (a == '-')))
            {
                throw new InvalidRoleException("TestRole - role name can not contain signs or digits.");
            }

            if (char.IsLower(role.RoleName[0]))
            {
                throw new InvalidRoleException("TestRole - role name can not start with lower character.");
            }
        }
    }
}
