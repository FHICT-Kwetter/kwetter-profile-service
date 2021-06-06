using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ProfileService.Data.UnitOfWork;

namespace ProfileService.Service.UseCases
{
    public class GetUserInfo : IRequest<object>
    {
        public Guid userId { get; set; }
    }
    
    public class GetUserInfoHandler: IRequestHandler<GetUserInfo, object>
    {
        public IUnitOfWork unitOfWork;

        public GetUserInfoHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<object> Handle(GetUserInfo request, CancellationToken cancellationToken)
        {
            var profile = await this.unitOfWork.Profiles.Read(request.userId);
            
            return new
            {
                profile.DisplayName,
                profile.Username,
                profile.ImageUrl,
            };
        }
    }
}