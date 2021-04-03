// <copyright file="ProfileController.cs" company="Kwetter">
//     Copyright Kwetter. All rights reserved.
// </copyright>
// <author>Dirk Heijnen</author>

namespace ProfileService.Api.Controllers
{
    using System.Net.Mime;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

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
            return this.Ok(username);
        }
    }
}