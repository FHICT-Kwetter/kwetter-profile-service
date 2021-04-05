// <copyright file="MutateProfileRequest.cs" company="Kwetter">
//     Copyright Kwetter. All rights reserved.
// </copyright>
// <author>Dirk Heijnen</author>

namespace ProfileService.Domain.Requests
{
    /// <summary>
    /// Defines the mutate profile request.
    /// </summary>
    public class MutateProfileRequest
    {
        /// <summary>
        /// Gets the bio value, if set the bio will be updated to the new value.
        /// </summary>
        public string Bio { get; init; }

        /// <summary>
        /// Gets the image url, if set the image will be updated to the new value.
        /// </summary>
        public string ImageUrl { get; init; }

        /// <summary>
        /// Gets the display name, if set the display name will be updated to the new value.
        /// </summary>
        public string DisplayName { get; init; }
    }
}