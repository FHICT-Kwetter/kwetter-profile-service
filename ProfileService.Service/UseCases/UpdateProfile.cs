namespace ProfileService.Service.UseCases
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using ProfileService.Domain.Models;

    public class UpdateProfile : IRequest<Profile>
    {
        public UpdateProfile(Guid userId, string bio, string imageLink)
        {
            UserId = userId;
            Bio = bio;
            ImageLink = imageLink;
        }

        /// <summary>
        /// The user id.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// The bio text in the profile.
        /// </summary>
        public string Bio { get; set; }

        /// <summary>
        /// The link to the profile image of the user.
        /// </summary>
        public string ImageLink { get; set; }
        
        
    }

    public class UpdateProfileHandler : IRequestHandler<UpdateProfile, Profile>
    {
        public Task<Profile> Handle(UpdateProfile request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();

            // Find user with id.

            // Update bio or imagelink

            // Save user

            // Return updated profile data.
        }
    }
}