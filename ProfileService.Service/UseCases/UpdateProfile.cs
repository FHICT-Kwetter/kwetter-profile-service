using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ProfileService.Data.UnitOfWork;
using ProfileService.Domain.Models;
using ProfileService.Domain.Requests;

namespace ProfileService.Service.UseCases
{
    public sealed record UpdateProfile(Guid UserId, string Bio, string ImageUrl, string DisplayName) : IRequest<Profile>;

    public class UpdateProfileHandler : IRequestHandler<UpdateProfile, Profile>
    {
        private readonly IUnitOfWork unitOfWork;

        public UpdateProfileHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Profile> Handle(UpdateProfile request, CancellationToken cancellationToken)
        {
            var profile = await this.unitOfWork.Profiles.Read(request.UserId);
            
            profile.Update(new MutateProfileRequest
            {
                Bio = request.Bio,
                ImageUrl = request.ImageUrl,
                DisplayName = request.DisplayName,
            });
            
            await this.unitOfWork.Profiles.Update(profile);
            await this.unitOfWork.SaveAsync();
            
            return profile;
        }
    }
}