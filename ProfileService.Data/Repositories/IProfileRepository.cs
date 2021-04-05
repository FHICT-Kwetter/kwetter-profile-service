// <copyright file="IProfileRepository.cs" company="Kwetter">
//     Copyright Kwetter. All rights reserved.
// </copyright>
// <author>Dirk Heijnen</author>

using AutoMapper;

namespace ProfileService.Data.Repositories
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using ProfileService.Data.Contexts;
    using ProfileService.Data.Entities;
    using ProfileService.Domain.Exceptions;
    using ProfileService.Domain.Models;

    /// <summary>
    /// Defines the interface for communicating with the profile database tables.
    /// </summary>
    public interface IProfileRepository
    {
        /// <summary>
        /// Creates a new profile entry in the database.
        /// </summary>
        /// <param name="profile">The <see cref="Profile"/> to add to the database.</param>
        /// <param name="userId">The user id of the user creating the profile.</param>
        /// <returns>The created profile.</returns>
        Task<Profile> Create(Profile profile, Guid userId);

        /// <summary>
        /// Finds a profile by a username.
        /// </summary>
        /// <param name="username">The username to find the profile for.</param>
        /// <returns>The <see cref="Profile"/> domain model.</returns>
        Task<Profile> Read(string username);

        /// <summary>
        /// Finds a profile by a user id.
        /// </summary>
        /// <param name="userId">The user id to find the profile for.</param>
        /// <returns>The <see cref="Profile"/> domain model.</returns>
        Task<Profile> Read(Guid userId);

        /// <summary>
        /// Updates an existing profile with new information.
        /// </summary>
        /// <param name="profile">The <see cref="Profile"/> with the new information.</param>
        /// <returns>The updated profile.</returns>
        Task<Profile> Update(Profile profile);

        /// <summary>
        /// Deletes a profile from the profile database.
        /// </summary>
        /// <param name="profile">The profile to delete.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task Delete(Profile profile);
    }

    /// <summary>
    /// Defines the implementation of the ProfileRepository.
    /// </summary>
    public class ProfileRepository : IProfileRepository
    {
        /// <summary>
        /// The database context.
        /// </summary>
        private readonly IProfileContext context;

        /// <summary>
        /// The automapper.
        /// </summary>
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileRepository"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="mapper">The automapper.</param>
        public ProfileRepository(IProfileContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<Profile> Create(Profile profile, Guid userId)
        {
            if (await this.context.Profiles.FirstOrDefaultAsync(x => x.Username.ToLower().Contains(profile.Username.ToLower())) != null)
            {
                throw new ProfileCreationException("Profile with this username already exists.");
            }

            if (await this.context.Profiles.FirstOrDefaultAsync(x => x.UserId == userId) != null)
            {
                throw new ProfileCreationException("This user already has a profile.");
            }

            var profileEntity = new ProfileEntity
            {
                UserId = userId,
                Username = profile.Username,
                Bio = profile.Bio,
                ImageUrl = profile.ImageLink,
                DisplayName = profile.DisplayName,
            };

            await this.context.Profiles.AddAsync(profileEntity);
            return profile;
        }

        /// <inheritdoc/>
        public async Task<Profile> Read(string username)
        {
            var foundProfile = await this.context.Profiles
                .AsNoTracking()
                .Where(x => x.Username.ToLower().Contains(username.ToLower()))
                .FirstOrDefaultAsync();

            if (foundProfile == null)
            {
                throw new ProfileNotFoundException("Profile with the provided username was not found.");
            }

            return this.mapper.Map<Profile>(foundProfile);
        }

        /// <inheritdoc/>
        public async Task<Profile> Read(Guid userId)
        {
            var foundProfile = await this.context.Profiles
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .FirstOrDefaultAsync();

            if (foundProfile == null)
            {
                throw new ProfileNotFoundException("Profile with the provided userid was not found.");
            }

            return this.mapper.Map<Profile>(foundProfile);
        }

        /// <inheritdoc/>
        public async Task<Profile> Update(Profile profile)
        {
            var foundProfile = await this.context.Profiles
                .Where(x => x.Username.ToLower() == profile.Username.ToLower())
                .FirstOrDefaultAsync();

            if (foundProfile == null)
            {
                throw new ProfileNotFoundException("Profile to update was not found.");
            }

            foundProfile.Bio = profile.Bio;
            foundProfile.ImageUrl = profile.ImageLink;

            this.context.Update(foundProfile);
            return profile;
        }

        /// <inheritdoc/>
        public async Task Delete(Profile profile)
        {
            var foundProfile = await this.context.Profiles
                .Where(x => x.Username.ToLower() == profile.Username.ToLower())
                .FirstOrDefaultAsync();

            if (foundProfile == null)
            {
                throw new ProfileNotFoundException("Profile to delete was not found.");
            }

            this.context.Profiles.Remove(foundProfile);
        }
    }
}