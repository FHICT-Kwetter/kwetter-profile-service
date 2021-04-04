// <copyright file="UserAddedEvent.cs" company="Kwetter">
//     Copyright Kwetter. All rights reserved.
// </copyright>
// <author>Dirk Heijnen</author>

namespace ProfileService.Service.Events
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using ProfileService.Data.UnitOfWork;
    using ProfileService.Domain.Models;
    using ProfileService.Messaging.Common.Attributes;
    using ProfileService.Messaging.Common.Events;

    /// <summary>
    /// The user added event dto.
    /// </summary>
    [EventType("user_added")]
    public class UserAddedEvent : IEventNotification
    {
        /// <summary>
        /// The user id.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// The users email address.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The users username.
        /// </summary>
        public string Username { get; set; }
    }

    /// <summary>
    /// Defines the event handler for the "user_added" event.
    /// </summary>
    internal sealed class UserAddedEventHandler : IEventNotificationHandler<UserAddedEvent>
    {
        /// <summary>
        /// The <see cref="IUnitOfWork"/>.
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserAddedEventHandler"/> class.
        /// </summary>
        /// <param name="unitOfWork">The <see cref="IUnitOfWork"/>.</param>
        public UserAddedEventHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// The handle function itself.
        /// </summary>
        /// <param name="eventParam">The <see cref="UserAddedEvent"/>.</param>
        /// <param name="cancellationToken">The cancellationtoken.</param>
        /// <returns>An awaitable task.</returns>
        public async Task Handle(UserAddedEvent eventParam, CancellationToken cancellationToken)
        {
            var createdProfile = new Profile
            {
                Username = eventParam.Username,
                Bio = string.Empty,
                ImageLink = "https://ik.imagekit.io/5ii0qakqx65/profile_placeholder_i-fAWNvvvrMy.jpg",
            };

            await this.unitOfWork.Profiles.Create(createdProfile, eventParam.UserId);
            await this.unitOfWork.SaveAsync();
        }
    }
}