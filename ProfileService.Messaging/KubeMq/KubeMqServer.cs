using System;

namespace ProfileService.Messaging.KubeMq
{
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using ProfileService.Messaging.Configuration;
    using ProfileService.Messaging.Grpc;
    using ProfileService.Messaging.PubSub;
    using ProfileService.Messaging.Queue;

    public interface IKubeMqServer
    {
        QueueManager QueueManager { get; }

        PubSubManager PubSubManager { get; }

        GrpcManager GrpcManager { get; }
    }

    public class KubeMqServer : IKubeMqServer
    {
        public QueueManager QueueManager { get; }

        public PubSubManager PubSubManager { get; }

        public GrpcManager GrpcManager { get; }

        public KubeMqServer(IOptions<KubeMqOptions> options, IServiceProvider serviceProvider, IMediator mediator)
        {
            this.PubSubManager = new PubSubManager(options.Value, mediator);
            this.QueueManager = new QueueManager(options.Value);
            this.GrpcManager = new GrpcManager(options.Value);
        }
    }
}