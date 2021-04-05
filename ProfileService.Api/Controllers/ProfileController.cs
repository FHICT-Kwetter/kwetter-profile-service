// <copyright file="ProfileController.cs" company="Kwetter">
//     Copyright Kwetter. All rights reserved.
// </copyright>
// <author>Dirk Heijnen</author>

namespace ProfileService.Api.Controllers
{
    using System;
    using System.Linq;
    using System.Net.Mime;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ProfileService.Api.Contracts.Requests;
    using ProfileService.Api.Contracts.Responses;
    using ProfileService.Service.UseCases;

    [ApiController]
    [Authorize]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/v1/profiles")]
    public class ProfileController : ControllerBase
    {
        /// <summary>
        /// The <see cref="IMediator"/>.
        /// </summary>
        private readonly IMediator mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator instance.</param>
        public ProfileController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet("{username}")]
        public async Task<IActionResult> ReadProfile([FromRoute] string username)
        {
            var result = await this.mediator.Send(new ReadProfile(username));
            var profile = new ReadProfileResponse
            {
                Username = result.Username,
                DisplayName = result.DisplayName,
                Bio = result.Bio,
                ImageLink = result.ImageLink,
            };
            return this.Ok(profile);
        }

        [Authorize]
        [HttpPut("me")]
        public async Task<IActionResult> UpdateProfile([FromBody] EditProfileRequest request)
        {
            var userId = Guid.Parse(this.User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value);
            var result = await this.mediator.Send(new UpdateProfile(userId, request.Bio, request.ImageLink));
            return this.Ok(result);
        }
    }
}