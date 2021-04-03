namespace ProfileService.Service.UseCases
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
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
        public Task<Profile> Handle(ReadProfile request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();

            // Find the profile of the user with the matching username.

            // Throw NotFound error when the username was not found.

            // Return profile object when the username was found.
        }
    }
}