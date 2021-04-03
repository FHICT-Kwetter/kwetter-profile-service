namespace ProfileService.Messaging.Common.Messages
{
    using MediatR;

    public interface IMessageNotificationHandler<in TMessage> : INotificationHandler<TMessage> where TMessage : IMessageNotification
    {
    }
}