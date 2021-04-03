namespace ProfileService.Messaging.Common.Events
{
    using MediatR;

    public interface IEventNotificationHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IEventNotification
    {
    }
}