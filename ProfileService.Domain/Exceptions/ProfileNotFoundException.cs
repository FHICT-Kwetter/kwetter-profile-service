// <copyright file="ProfileNotFoundException.cs" company="Kwetter">
//     Copyright Kwetter. All rights reserved.
// </copyright>
// <author>Dirk Heijnen</author>

namespace ProfileService.Domain.Exceptions
{
    using ProfileService.Domain.Exceptions.Base;

    /// <summary>
    /// Defines the exception for failures in the the lookup of a profile.
    /// </summary>
    public class ProfileNotFoundException : BaseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        public ProfileNotFoundException(string message) : base(message)
        {
        }
    }
}