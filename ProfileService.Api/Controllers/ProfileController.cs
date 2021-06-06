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

    /// <summary>
    /// The <see cref="ProfileController"/>.
    /// </summary>
    [ApiController]
    [Authorize]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("/")]
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
        [HttpGet("{id}/userinfo")]
        public async Task<IActionResult> ReadUserInfo([FromRoute] string id)
        {
            var userId = Guid.Parse(id);
            var result = await this.mediator.Send(new GetUserInfo() { userId = userId });
            return this.Ok(result);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ReadProfileOwn()
        {
            var userId = Guid.Parse(this.User.Claims.First(x => x.Type == "sub").Value);
            var result = await this.mediator.Send(new ReadProfile(userId));

            var profile = new ReadProfileResponse
            {
                UserId = result.UserId,
                Username = result.Username,
                DisplayName = result.DisplayName,
                Bio = result.Bio,
                ImageUrl = result.ImageUrl,
            };

            return this.Ok(profile);
        }

        [AllowAnonymous]
        [HttpGet("{username}")]
        public async Task<IActionResult> ReadProfile([FromRoute] string username)
        {
            var result = await this.mediator.Send(new ReadProfile(username));

            var profile = new ReadProfileResponse
            {
                UserId = result.UserId,
                Username = result.Username,
                DisplayName = result.DisplayName,
                Bio = result.Bio,
                ImageUrl = result.ImageUrl,
            };

            return this.Ok(profile);
        }

        [Authorize]
        [HttpPut("me")]
        public async Task<IActionResult> UpdateProfile([FromBody] EditProfileRequest request)
        {
            var userId = Guid.Parse(this.User.Claims.First(x => x.Type == "sub").Value);
            var result = await this.mediator.Send(new UpdateProfile(userId, request.Bio, request.ImageUrl, request.DisplayName));
            return this.Ok(result);
        }
    }
}