using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ProfileService.Data.UnitOfWork;
using ProfileService.Domain.Models;

namespace ProfileService.Service.UseCases
{
    public class ReadProfile : IRequest<Profile>
    {
        public string Username { get; }

        public Guid UserId { get; }

        public ReadProfile(string username)
        {
            this.Username = username;
        }

        public ReadProfile(Guid userId)
        {
            this.UserId = userId;
        }
    }

    public class ReadProfileHandler : IRequestHandler<ReadProfile, Profile>
    {
        private readonly IUnitOfWork unitOfWork;
        
        public ReadProfileHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

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