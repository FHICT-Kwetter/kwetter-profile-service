namespace ProfileService.Messaging.Common.Query
{
    using MediatR;

    public interface IQueryRequest<out TResponse> : IRequest<TResponse>
    {
        
    }
}