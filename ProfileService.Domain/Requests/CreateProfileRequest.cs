using System;

namespace ProfileService.Domain.Requests
{
    public class CreateProfileRequest
    {
        public Guid UserId { get; init; }
        
        public string Username { get; init; }

        public string Bio { get; init; }
        
        public string ImageUrl { get; init; }
        
        public string DisplayName { get; init; }
    }
}
