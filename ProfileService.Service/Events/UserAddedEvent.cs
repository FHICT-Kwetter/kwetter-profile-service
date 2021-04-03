// <copyright file="UserAddedEvent.cs" company="Kwetter">
//     Copyright Kwetter. All rights reserved.
// </copyright>
// <author>Dirk Heijnen</author>

namespace ProfileService.Service.Events
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
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
        /// The Logger.
        /// </summary>
        private readonly ILogger<UserAddedEventHandler> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserAddedEventHandler"/> class.
        /// </summary>
        /// <param name="logger">The Logger.</param>
        public UserAddedEventHandler(ILogger<UserAddedEventHandler> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// The handle function itself.
        /// </summary>
        /// <param name="eventParam">The <see cref="UserAddedEvent"/>.</param>
        /// <param name="cancellationToken">The cancellationtoken.</param>
        /// <returns>An awaitable task.</returns>
        public async Task Handle(UserAddedEvent eventParam, CancellationToken cancellationToken)
        {
            this.logger.LogInformation($"Event came in with: ID: {eventParam.UserId}, Email: {eventParam.Email} and Username: {eventParam.Username}");
        }
    }
}