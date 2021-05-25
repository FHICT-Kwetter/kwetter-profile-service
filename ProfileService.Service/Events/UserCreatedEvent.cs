namespace ProfileService.Service.Events
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using ProfileService.Data.UnitOfWork;
    using ProfileService.Domain.Models;
    using ProfileService.Domain.Requests;

    public record UserCreatedEvent(Guid UserId, string Email, string Username) : IRequest;

    public class UserCreatedEventHandler : IRequestHandler<UserCreatedEvent>
    {
        private IUnitOfWork unitOfWork;

        public UserCreatedEventHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UserCreatedEvent request, CancellationToken cancellationToken)
        {
            var profile = Profile.Create(new CreateProfileRequest
            {
                Username = request.Username,
                Bio = $"{request.Username}'s Kwetter Profile",
                ImageUrl = "https://ik.imagekit.io/5ii0qakqx65/profile_placeholder_i-fAWNvvvrMy.jpg",
                DisplayName = request.Username,
            });

            await this.unitOfWork.Profiles.Create(profile, request.UserId);
            await this.unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}