//-----------------------------------------------------------------------
// <copyright file="RoleService.cs" company="Transilvania University of Brasov">
//     Copyright (c) Brassoi Silvia Maria. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AuctionLogic.Bussines
{
    using System.Linq;
    using System.Reflection;
    using log4net;
    using Models;

    /// <summary>
    ///   <para>Role service class</para>
    /// </summary>
    public class RoleService
    {
        /// <summary>The log</summary>
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        ///   <para>
        ///  Tests the role.
        /// </para>
        /// </summary>
        /// <param name="role">The role.</param>
        /// <returns>Return true if role is valid, false if not.</returns>
        public bool TestRole(Role role)
        {
            Log.Info("TestRole() was called.");

            if (role?.RoleName == null)
            {
                return false;
            }

            if (role.RoleName.Length == 0)
            {
                return false;
            }

            if ((role.RoleName.Length < 3) || (role.RoleName.Length > 50))
            {
                return false;
            }

            if (!role.RoleName.All(a => char.IsLetter(a) || char.IsWhiteSpace(a) || (a == '-')))
            {
                return false;
            }

            return !char.IsLower(role.RoleName.First());
        }
    }
}
