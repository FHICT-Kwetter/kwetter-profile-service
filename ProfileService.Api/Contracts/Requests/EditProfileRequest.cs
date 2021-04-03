namespace ProfileService.Api.Contracts.Requests
{
    public class EditProfileRequest
    {
        /// <summary>
        /// The bio text in the profile.
        /// </summary>
        public string Bio { get; set; }

        /// <summary>
        /// The link to the profile image of the user.
        /// </summary>
        public string ImageLink { get; set; }
    }
}