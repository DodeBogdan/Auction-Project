//-----------------------------------------------------------------------
// <copyright file="UserService.cs" company="Transilvania University of Brasov">
//     Copyright (c) Bogdan Gheorghe Nicolae. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AuctionLogic.Business
{
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using Help;
    using log4net;
    using Models;

    /// <summary>User service class</summary>
    public class UserService
    {
        /// <summary>The log</summary>
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>Tests the user.</summary>
        /// <param name="user">The user.</param>
        /// <returns>Return true if user is valid, false if not.</returns>
        public bool TestUser(User user)
        {
            Log.Info("TestUser() was called.");

            if (user?.FirstName == null)
            {
                return false;
            }

            if (user.FirstName.Length == 0)
            {
                return false;
            }

            if ((user.FirstName.Length < 3) || (user.FirstName.Length > 50))
            {
                return false;
            }

            if (!user.FirstName.All(a => char.IsLetter(a) || char.IsWhiteSpace(a) || (a == '-')))
            {
                return false;
            }

            if (char.IsLower(user.FirstName[0]))
            {
                return false;
            }

            if (user.LastName == null)
            {
                return false;
            }

            if (user.LastName.Length == 0)
            {
                return false;
            }

            if (user.LastName == null)
            {
                return false;
            }

            if ((user.LastName.Length < 3) || (user.LastName.Length > 50))
            {
                return false;
            }

            if (!user.LastName.All(a => char.IsLetter(a) || char.IsWhiteSpace(a) || (a == '-')))
            {
                return false;
            }

            if (char.IsLower(user.LastName[0]))
            {
                return false;
            }

            if (user.Age < 18 || user.Age > 107)
            {
                return false;
            }

            Regex regex = new Regex(@"[A-Za-z\s@.-0-9]+");

            if (regex.Match(user.Email) == Match.Empty || user.Email.Length < 4)
            {
                return false;
            }

            if (user.Password == null)
            {
                return false;
            }

            if (user.Password.Length == 0)
            {
                return false;
            }

            if ((user.Password.Length < 3) || (user.Password.Length > 50))
            {
                return false;
            }

            if (char.IsLower(user.Password[0]))
            {
                return false;
            }

            if (user.RoleStatus == 0)
            {
                return false;
            }

            if (user.RoleStatus < 0 || user.RoleStatus > 2)
            {
                return false;
            }

            user.Score = ApplicationHelp.DefaultScore;

            if (user.Gender == null)
            {
                return false;
            }

            if (user.Gender.Length == 0)
            {
                return false;
            }

            if (user.Gender.Length != 1)
            {
                return false;
            }

            if (!user.Gender.All(a => char.IsLetter(a) || a != 'M' || a != 'F'))
            {
                return false;
            }

            if (char.IsLower(user.Gender[0]))
            {
                return false;
            }

            if (user.CNP == null)
            {
                return false;
            }

            if (user.CNP.Length != 13)
            {
                return false;
            }

            if (!user.CNP.All(char.IsDigit))
            {
                return false;
            }

            if ((user.Gender[0] == 'M' && user.CNP[0] != '1') && (user.Gender[0] == 'M' && user.CNP[0] != '5'))
            {
                return false;
            }

            if ((user.Gender[0] == 'F' && user.CNP[0] != '2') && (user.Gender[0] == 'F' && user.CNP[0] != '6'))
            {
                return false;
            }

            if (user.Adress == null)
            {
                return false;
            }

            if (user.Adress.Length == 0)
            {
                return false;
            }

            if ((user.Adress.Length < 3) || (user.Adress.Length > 50))
            {
                return false;
            }

            if (!user.Adress.All(a => char.IsLetter(a) || char.IsDigit(a) || char.IsWhiteSpace(a) || a == '-' || a == '.' || a == ','))
            {
                return false;
            }

            if (char.IsLower(user.Adress[0]))
            {
                return false;
            }

            if (user.Phone == null)
            {
                return false;
            }

            if (user.Phone.Length != 10)
            {
                return false;
            }

            if (!user.Phone.All(char.IsDigit))
            {
                return false;
            }

            return true;
        }
    }
}
