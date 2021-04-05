// <copyright file="Profile.cs" company="Kwetter">
//     Copyright Kwetter. All rights reserved.
// </copyright>
// <author>Dirk Heijnen</author>

namespace ProfileService.Domain.Models
{
    using ProfileService.Domain.Requests;

    /// <summary>
    /// Defines the profile domain model.
    /// </summary>
    public class Profile
    {
        /// <summary>
        /// Gets the bio text in the profile.
        /// </summary>
        public string Bio { get; private set; }

        /// <summary>
        /// Gets the link to the profile image of the user.
        /// </summary>
        public string ImageUrl { get; private set; }

        /// <summary>
        /// Gets get the username for which the profile belongs to, is read-only.
        /// </summary>
        public string Username { get; private set; }

        /// <summary>
        /// Gets the display name, a non-unique name the user can use in his profile.
        /// </summary>
        public string DisplayName { get; private set; }

        /// <summary>
        /// Factory method that will create a new profile.
        /// </summary>
        /// <param name="create">Creates a new instance of a profile.</param>
        /// <returns>The created profile.</returns>
        public static Profile Create(CreateProfileRequest create)
        {
            return new()
            {
                Bio = create.Bio,
                ImageUrl = create.ImageUrl,
                DisplayName = create.DisplayName,
                Username = create.Username,
            };
        }

        /// <summary>
        /// Updates the profile.
        /// </summary>
        /// <param name="mutate">The mutate request, all values given in the request are updated.</param>
        public void Update(MutateProfileRequest mutate)
        {
            this.Bio = mutate.Bio ?? this.Bio;
            this.ImageUrl = mutate.ImageUrl ?? this.ImageUrl;
            this.DisplayName = mutate.DisplayName ?? this.DisplayName;
        }
    }
}