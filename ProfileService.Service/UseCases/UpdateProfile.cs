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

    /// <summary>
    /// Defines the update profile usecase request.
    /// </summary>
    public sealed record UpdateProfile(Guid UserId, string Bio, string ImageLink) : IRequest<Profile>;

    /// <summary>
    /// Defines the update profile usercase handler.
    /// </summary>
    internal sealed class UpdateProfileHandler : IRequestHandler<UpdateProfile, Profile>
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
            // Find profile with user id.
            var profile = await this.unitOfWork.Profiles.Read(request.UserId);

            // Update bio or imagelink
            profile.Bio = request.Bio;
            profile.ImageLink = request.ImageLink;

            // Update profile
            var updatedProfile = await this.unitOfWork.Profiles.Update(profile);

            // Save the changes to the database transactionally.
            await this.unitOfWork.SaveAsync();

            // Return updated profile data.
            return updatedProfile;
        }
    }
}