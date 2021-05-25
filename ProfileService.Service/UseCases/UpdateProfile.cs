// <copyright file="UpdateProfile.cs" company="Kwetter">
//     Copyright Kwetter. All rights reserved.
// </copyright>
// <author>Dirk Heijnen</author>

namespace ProfileService.Service.UseCases
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using ProfileService.Data.UnitOfWork;
    using ProfileService.Domain.Models;
    using ProfileService.Domain.Requests;

    /// <summary>
    /// Defines the update profile usecase request.
    /// </summary>
    public sealed record UpdateProfile(Guid UserId, string Bio, string ImageUrl, string DisplayName) : IRequest<Profile>;

    /// <summary>
    /// Defines the update profile usercase handler.
    /// </summary>
    public class UpdateProfileHandler : IRequestHandler<UpdateProfile, Profile>
    {
        /// <summary>
        /// The <see cref="IUnitOfWork"/>.
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateProfileHandler"/> class.
        /// </summary>
        /// <param name="unitOfWork">The <see cref="IUnitOfWork"/>.</param>
        public UpdateProfileHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Executes the use case logic.
        /// </summary>
        /// <param name="request">The incoming request for the use case.</param>
        /// <param name="cancellationToken">The cancellationtoken.</param>
        /// <returns>An awaitable task which returns the updated profile.</returns>
        public async Task<Profile> Handle(UpdateProfile request, CancellationToken cancellationToken)
        {
            // Find profile.
            var profile = await this.unitOfWork.Profiles.Read(request.UserId);

            // Update profile.
            profile.Update(new MutateProfileRequest
            {
                Bio = request.Bio,
                ImageUrl = request.ImageUrl,
                DisplayName = request.DisplayName,
            });

            // Save updated profile.
            await this.unitOfWork.Profiles.Update(profile);
            await this.unitOfWork.SaveAsync();

            // Return updated profile data.
            return profile;
        }
    }
}