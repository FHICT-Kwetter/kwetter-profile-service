namespace ProfileService.Service.UseCases
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using ProfileService.Data.Contexts;
    using ProfileService.Domain.Models;

    public class ReadProfile : IRequest<Profile>
    {
        public string Username { get; private set; }

        public ReadProfile(string username)
        {
            Username = username;
        }
    }

    public class ReadProfileHandler : IRequestHandler<ReadProfile, Profile>
    {
        private readonly ProfileContext context;

        public ReadProfileHandler(ProfileContext context)
        {
            this.context = context;
        }

        public async Task<Profile> Handle(ReadProfile request, CancellationToken cancellationToken)
        {
            var profile = await this.context.Profiles
                .Where(x => string.Equals(x.Username, request.Username, StringComparison.CurrentCultureIgnoreCase))
                .FirstOrDefaultAsync(cancellationToken);

            if (profile == null)
            {
                throw new Exception("Profile was not found!");
            }

            return new Profile
            {
                Username = profile.Username,
                Bio = profile.Bio,
                ImageLink = profile.ImageUrl,
            };
        }
    }
}