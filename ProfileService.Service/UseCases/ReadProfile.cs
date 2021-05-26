// <copyright file="ReadProfile.cs" company="Kwetter">
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
    /// Defines the read profile usecase request.
    /// </summary>
    public class ReadProfile : IRequest<Profile>
    {
        public string Username { get; set; }

        public Guid UserId { get; set; }

        public ReadProfile(string username)
        {
            this.Username = username;
        }

        public ReadProfile(Guid userId)
        {
            this.UserId = userId;
        }
    }

    /// <summary>
    /// Defines the read profile usercase handler.
    /// </summary>
    public class ReadProfileHandler : IRequestHandler<ReadProfile, Profile>
    {
        /// <summary>
        /// The <see cref="IUnitOfWork"/>.
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadProfileHandler"/> class.
        /// </summary>
        /// <param name="unitOfWork">The <see cref="IUnitOfWork"/>.</param>
        public ReadProfileHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Executes the use case logic.
        /// </summary>
        /// <param name="request">The incoming request for the use case.</param>
        /// <param name="cancellationToken">The cancellationtoken.</param>
        /// <returns>An awaitable task which returns the profile.</returns>
        public async Task<Profile> Handle(ReadProfile request, CancellationToken cancellationToken)
        {
            if (request.Username != null)
            {
                return await this.unitOfWork.Profiles.Read(request.Username);
            }

            return await this.unitOfWork.Profiles.Read(request.UserId);
        }
    }
}