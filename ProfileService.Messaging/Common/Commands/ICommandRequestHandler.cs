namespace ProfileService.Messaging.Common.Commands
{
    using MediatR;

    public interface ICommandRequestHandler<in TCommand> : IRequestHandler<TCommand> where TCommand : ICommandRequest
    {
    }
}