namespace ProfileService.Messaging.Common.Query
{
    using MediatR;

    public interface IQueryRequestHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse> where TQuery : IQueryRequest<TResponse>
    {
        
    }
}