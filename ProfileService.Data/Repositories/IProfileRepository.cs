// <copyright file="IProfileRepository.cs" company="Kwetter">
//     Copyright Kwetter. All rights reserved.
// </copyright>
// <author>Dirk Heijnen</author>

namespace ProfileService.Data.Repositories
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using ProfileService.Data.Contexts;
    using ProfileService.Data.Entities;
    using ProfileService.Domain.Models;

    /// <summary>
    /// Defines the interface for communicating with the profile database tables.
    /// </summary>
    public interface IProfileRepository
    {
        /// <summary>
        /// Finds a profile by a username.
        /// </summary>
        /// <param name="username">The username to find the profile for.</param>
        /// <returns>The <see cref="Profile"/> domain model.</returns>
        Task<Profile> FindByUsername(string username);

        /// <summary>
        /// Creates a new profile entry in the database.
        /// </summary>
        /// <param name="profile">The <see cref="Profile"/> to add to the database.</param>
        /// <param name="userId">The user id of the user creating the profile.</param>
        /// <returns>The created profile.</returns>
        Task<Profile> Create(Profile profile, Guid userId);
    }

    /// <summary>
    /// Defines the implementation of the ProfileRepository.
    /// </summary>
    public class ProfileRepository : IProfileRepository
    {
        /// <summary>
        /// The database context.
        /// </summary>
        private readonly ProfileContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileRepository"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public ProfileRepository(ProfileContext context)
        {
            this.context = context;
        }

        /// <inheritdoc/>
        public async Task<Profile> FindByUsername(string username)
        {
            var foundProfile = await this.context.Profiles
                .AsNoTracking()
                .Where(x => string.Equals(x.Username, username, StringComparison.CurrentCultureIgnoreCase))
                .FirstOrDefaultAsync();

            if (foundProfile == null)
            {
                return null;
            }

            return new Profile
            {
                Username = foundProfile.Username,
                Bio = foundProfile.Bio,
                ImageLink = foundProfile.ImageUrl,
            };
        }

        /// <inheritdoc/>
        public async Task<Profile> Create(Profile profile, Guid userId)
        {
            if (await this.FindByUsername(profile.Username) != null)
            {
                throw new Exception("Profile with this username already exists");
            }

            if (await this.context.Profiles.FirstOrDefaultAsync(x => x.UserId == userId) != null)
            {
                throw new Exception("This user already has a profile");
            }

            var profileEntity = new ProfileEntity
            {
                UserId = userId,
                Username = profile.Username,
                Bio = profile.Bio,
                ImageUrl = profile.ImageLink,
            };

            await this.context.Profiles.AddAsync(profileEntity);
            return profile;
        }
    }
}