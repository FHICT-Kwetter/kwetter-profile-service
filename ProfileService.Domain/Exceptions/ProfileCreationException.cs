// <copyright file="ProfileCreationException.cs" company="Kwetter">
//     Copyright Kwetter. All rights reserved.
// </copyright>
// <author>Dirk Heijnen</author>

namespace ProfileService.Domain.Exceptions
{
    using ProfileService.Domain.Exceptions.Base;

    /// <summary>
    /// Defines the exception for failures in the creation of a profile.
    /// </summary>
    public class ProfileCreationException : BaseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileCreationException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        public ProfileCreationException(string message) : base(message)
        {
        }
    }
}