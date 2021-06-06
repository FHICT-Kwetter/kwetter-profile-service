using System;
using ProfileService.Domain.Requests;

namespace ProfileService.Domain.Models
{
    public class Profile
    {
        public Guid UserId { get; private set; }

        public string Bio { get; private set; }

        public string ImageUrl { get; private set; }

        public string Username { get; private set; }

        public string DisplayName { get; private set; }

        public static Profile Create(CreateProfileRequest create) => new()
        {
            UserId = create.UserId,
            Bio = create.Bio,
            ImageUrl = create.ImageUrl,
            DisplayName = create.DisplayName,
            Username = create.Username,
        };

        public void Update(MutateProfileRequest mutate)
        {
            this.Bio = mutate.Bio ?? this.Bio;
            this.ImageUrl = mutate.ImageUrl ?? this.ImageUrl;
            this.DisplayName = mutate.DisplayName ?? this.DisplayName;
        }
    }
}