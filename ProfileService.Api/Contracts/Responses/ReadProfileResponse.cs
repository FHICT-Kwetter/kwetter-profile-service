namespace ProfileService.Api.Contracts.Responses
{
    public class ReadProfileResponse
    {
        /// <summary>
        /// The username of the user.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The display name of the user.
        /// </summary>
        public string DisplayName { get; set; }

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