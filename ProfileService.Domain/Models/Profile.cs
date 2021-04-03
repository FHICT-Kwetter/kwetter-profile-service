// <copyright file="Profile.cs" company="Kwetter">
//     Copyright Kwetter. All rights reserved.
// </copyright>
// <author>Dirk Heijnen</author>

namespace ProfileService.Domain.Models
{
    public class Profile
    {
        /// <summary>
        /// The bio text in the profile.
        /// </summary>
        public string Bio { get; set; }

        /// <summary>
        /// The link to the profile image of the user.
        /// </summary>
        public string ImageLink { get; set; }

        /// <summary>
        /// Get the username for which the profile belongs to, is read-only.
        /// </summary>
        public string Username { get; set; }
    }
}