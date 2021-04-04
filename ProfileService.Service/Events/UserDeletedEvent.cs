// <copyright file="UserDeletedEvent.cs" company="Kwetter">
//     Copyright Kwetter. All rights reserved.
// </copyright>
// <author>Dirk Heijnen</author>

namespace ProfileService.Service.Events
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using ProfileService.Data.UnitOfWork;
    using ProfileService.Messaging.Common.Attributes;
    using ProfileService.Messaging.Common.Events;

    /// <summary>
    /// The user added event dto.
    /// </summary>
    [EventType("user_deleted")]
    public class UserDeletedEvent : IEventNotification
    {
        /// <summary>
        /// The user id.
        /// </summary>
        public Guid UserId { get; set; }
    }

    /// <summary>
    /// Defines the event handler for the "user_deleted" event.
    /// </summary>
    internal sealed class UserDeletedEventHandler : IEventNotificationHandler<UserDeletedEvent>
    {
        /// <summary>
        /// The <see cref="IUnitOfWork"/>.
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The Logger.
        /// </summary>
        private readonly ILogger<UserDeletedEventHandler> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserDeletedEventHandler"/> class.
        /// </summary>
        /// <param name="logger">The Logger.</param>
        /// <param name="unitOfWork">The <see cref="IUnitOfWork"/>.</param>
        public UserDeletedEventHandler(ILogger<UserDeletedEventHandler> logger, IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// The handle function itself.
        /// </summary>
        /// <param name="eventParam">The <see cref="UserAddedEvent"/>.</param>
        /// <param name="cancellationToken">The cancellationtoken.</param>
        /// <returns>An awaitable task.</returns>
        public async Task Handle(UserDeletedEvent eventParam, CancellationToken cancellationToken)
        {
            var foundProfile = await this.unitOfWork.Profiles.Read(eventParam.UserId);

            if (foundProfile == null)
            {
                Console.WriteLine($"Tried to delete profile for user with id {eventParam.UserId} but no profile was found");
            }

            await this.unitOfWork.Profiles.Delete(foundProfile);
            await this.unitOfWork.SaveAsync();
        }
    }
}