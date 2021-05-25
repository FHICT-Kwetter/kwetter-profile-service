// <copyright file="CreateProfileRequest.cs" company="Kwetter">
//     Copyright Kwetter. All rights reserved.
// </copyright>
// <author>Dirk Heijnen</author>

namespace ProfileService.Domain.Requests
{
    /// <summary>
    /// Defines the create profile request.
    /// </summary>
    public class CreateProfileRequest
    {
        /// <summary>
        /// Gets get the username for which the profile belongs to, is read-only.
        /// </summary>
        public string Username { get; init; }

        /// <summary>
        /// Gets the bio text in the profile.
        /// </summary>
        public string Bio { get; init; }

        /// <summary>
        /// Gets the link to the profile image of the user.
        /// </summary>
        public string ImageUrl { get; init; }

        /// <summary>
        /// Gets the display name, a non-unique name the user can use in his profile.
        /// </summary>
        public string DisplayName { get; init; }
    }
}