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
    using Exceptions;
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
        /// <exception cref="InvalidUserException">
        /// TestUser - user can not be null.
        /// or
        /// TestUser - user first name can not be null.
        /// or
        /// TestUser - user first name can not be empty.
        /// or
        /// TestUser - user first name have invalid length.
        /// or
        /// TestUser - user first name can not contain signs or digits.
        /// or
        /// TestUser - user first name can not start with lower character.
        /// or
        /// TestUser - user last name can not be null.
        /// or
        /// TestUser - user first last can not be empty.
        /// or
        /// TestUser - user last name have invalid length.
        /// or
        /// TestUser - user last name can not contain signs or digits.
        /// or
        /// TestUser - user last name can not start with lower character.
        /// or
        /// TestUser - user have invalid age.
        /// or
        /// TestUser - user email is invalid.
        /// or
        /// TestUser - user password can not be null.
        /// or
        /// TestUser - user password can not be empty.
        /// or
        /// TestUser - user password have invalid length.
        /// or
        /// TestUser - user password can not start with lower character.
        /// or
        /// TestUser - user role can not be null.
        /// or
        /// TestUser - user have invalid role.
        /// or
        /// TestUser - user gender can not be null.
        /// or
        /// TestUser - user gender can not be empty.
        /// or
        /// TestUser - user gender have invalid length.
        /// or
        /// TestUser - user have invalid gender.
        /// or
        /// TestUser - user gender can not start with lower character.
        /// or
        /// TestUser - user CNP can not be null.
        /// or
        /// TestUser - user CNP have invalid length.
        /// or
        /// TestUser - user have invalid CNP.
        /// or
        /// TestUser - user have invalid CNP.
        /// or
        /// TestUser - user have invalid CNP.
        /// or
        /// TestUser - user address can not be null.
        /// or
        /// TestUser - user address can not be empty.
        /// or
        /// TestUser - user address have invalid length.
        /// or
        /// TestUser - user have invalid address.
        /// or
        /// TestUser - user address can not start with lower character.
        /// or
        /// TestUser - user phone can not be null.
        /// or
        /// TestUser - user phone have invalid length.
        /// or
        /// TestUser - user have invalid phone.
        /// </exception>
        public void TestUser(User user)
        {
            Log.Info("TestUser() was called.");

            if (user == null)
            {
                throw new InvalidUserException("TestUser - user can not be null.");
            }

            if (user.FirstName == null)
            {
                throw new InvalidUserException("TestUser - user first name can not be null.");
            }

            if (user.FirstName.Length == 0)
            {
                throw new InvalidUserException("TestUser - user first name can not be empty.");
            }

            if ((user.FirstName.Length < 3) || (user.FirstName.Length > 50))
            {
                throw new InvalidUserException("TestUser - user first name have invalid length.");
            }

            if (!user.FirstName.All(a => char.IsLetter(a) || char.IsWhiteSpace(a) || (a == '-')))
            {
                throw new InvalidUserException("TestUser - user first name can not contain signs or digits.");
            }

            if (char.IsLower(user.FirstName[0]))
            {
                throw new InvalidUserException("TestUser - user first name can not start with lower character.");
            }

            if (user.LastName == null)
            {
                throw new InvalidUserException("TestUser - user last name can not be null.");
            }

            if (user.LastName.Length == 0)
            {
                throw new InvalidUserException("TestUser - user first last can not be empty.");
            }

            if ((user.LastName.Length < 3) || (user.LastName.Length > 50))
            {
                throw new InvalidUserException("TestUser - user last name have invalid length.");
            }

            if (!user.LastName.All(a => char.IsLetter(a) || char.IsWhiteSpace(a) || (a == '-')))
            {
                throw new InvalidUserException("TestUser - user last name can not contain signs or digits.");
            }

            if (char.IsLower(user.LastName[0]))
            {
                throw new InvalidUserException("TestUser - user last name can not start with lower character.");
            }

            if (user.Age < 18 || user.Age > 107)
            {
                throw new InvalidUserException("TestUser - user have invalid age.");
            }

            var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

            if (regex.Match(user.Email) == Match.Empty || user.Email.Length < 4 || user.Email.Length > 50)
            {
                throw new InvalidUserException("TestUser - user email is invalid.");
            }

            if (user.Password == null)
            {
                throw new InvalidUserException("TestUser - user password can not be null.");
            }

            if (user.Password.Length == 0)
            {
                throw new InvalidUserException("TestUser - user password can not be empty.");
            }

            if ((user.Password.Length < 3) || (user.Password.Length > 50))
            {
                throw new InvalidUserException("TestUser - user password have invalid length.");
            }

            if (char.IsLower(user.Password[0]))
            {
                throw new InvalidUserException("TestUser - user password can not start with lower character.");
            }

            if (user.RoleStatus == 0)
            {
                throw new InvalidUserException("TestUser - user role can not be null.");
            }

            if (user.RoleStatus < 0 || user.RoleStatus > 2)
            {
                throw new InvalidUserException("TestUser - user have invalid role.");
            }

            user.Score = ApplicationHelp.DefaultScore;

            if (user.Gender == null)
            {
                throw new InvalidUserException("TestUser - user gender can not be null.");
            }

            if (user.Gender.Length == 0)
            {
                throw new InvalidUserException("TestUser - user gender can not be empty.");
            }

            if (user.Gender.Length != 1)
            {
                throw new InvalidUserException("TestUser - user gender have invalid length.");
            }

            if (char.IsLower(user.Gender[0]))
            {
                throw new InvalidUserException("TestUser - user gender can not start with lower character.");
            }

            if (user.Gender[0] != 'F' && user.Gender[0] != 'M')
            {
                throw new InvalidUserException("TestUser - user have invalid gender.");
            }

            if (user.CNP == null)
            {
                throw new InvalidUserException("TestUser - user cnp can not be null.");
            }

            if (user.CNP.Length != 13)
            {
                throw new InvalidUserException("TestUser - user cnp have invalid length.");
            }

            if (!user.CNP.All(char.IsDigit))
            {
                throw new InvalidUserException("TestUser - user have invalid cnp.");
            }

            if ((user.Gender[0] == 'M' && user.CNP[0] != '1') && (user.Gender[0] == 'M' && user.CNP[0] != '5'))
            {
                throw new InvalidUserException("TestUser - user have invalid cnp.");
            }

            if ((user.Gender[0] == 'F' && user.CNP[0] != '2') && (user.Gender[0] == 'F' && user.CNP[0] != '6'))
            {
                throw new InvalidUserException("TestUser - user have invalid cnp.");
            }

            if (user.Adress == null)
            {
                throw new InvalidUserException("TestUser - user address can not be null.");
            }

            if (user.Adress.Length == 0)
            {
                throw new InvalidUserException("TestUser - user address can not be empty.");
            }

            if ((user.Adress.Length < 3) || (user.Adress.Length > 50))
            {
                throw new InvalidUserException("TestUser - user address have invalid length.");
            }

            if (!user.Adress.All(a => char.IsLetter(a) || char.IsDigit(a) || char.IsWhiteSpace(a) || a == '-' || a == '.' || a == ','))
            {
                throw new InvalidUserException("TestUser - user have invalid address.");
            }

            if (char.IsLower(user.Adress[0]))
            {
                throw new InvalidUserException("TestUser - user address can not start with lower character.");
            }

            if (user.Phone == null)
            {
                throw new InvalidUserException("TestUser - user phone can not be null.");
            }

            if (user.Phone.Length != 10)
            {
                throw new InvalidUserException("TestUser - user phone have invalid length.");
            }

            if (!user.Phone.All(char.IsDigit))
            {
                throw new InvalidUserException("TestUser - user have invalid phone.");
            }
        }
    }
}
