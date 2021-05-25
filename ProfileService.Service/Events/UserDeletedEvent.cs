using ProfileService.Data.UnitOfWork;

namespace ProfileService.Service.Events
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;

    public record UserDeletedEvent(Guid UserId, string Email, string Username) : IRequest;

    public class UserDeletedEventHandler : IRequestHandler<UserDeletedEvent>
    {
        private IUnitOfWork unitOfWork;

        public UserDeletedEventHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        
        public async Task<Unit> Handle(UserDeletedEvent request, CancellationToken cancellationToken)
        {
            var foundProfile = await this.unitOfWork.Profiles.Read(request.UserId);

            if (foundProfile == null)
            {
                return Unit.Value;
            }
            await this.unitOfWork.Profiles.Delete(foundProfile);
            await this.unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }

}