// <copyright file="EditProfileRequest.cs" company="Kwetter">
//     Copyright Kwetter. All rights reserved.
// </copyright>
// <author>Dirk Heijnen</author>

namespace ProfileService.Api.Contracts.Requests
{
    /// <summary>
    /// Defines the <see cref="EditProfileRequest"/>
    /// </summary>
    public class EditProfileRequest
    {
        /// <summary>
        /// Gets or sets the display name of the user.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the bio text in the profile.
        /// </summary>
        public string Bio { get; set; }

        /// <summary>
        /// Gets or sets the link to the profile image of the user.
        /// </summary>
        public string ImageUrl { get; set; }
    }
}