using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.PubSub.V1;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ProfileService.Service.Events;
using Encoding = System.Text.Encoding;

namespace ProfileService.PubSub
{
    public static class PubSubService
    {
        public static async Task Publish(string topicId, object message)
        {
            var publisher = await PublisherClient.CreateAsync(TopicName.FromProjectTopic("kwetter-308618", topicId));
            await publisher.PublishAsync(JsonConvert.SerializeObject(message));
        }

        public static Task Subscribe(string subscriptionId, IServiceScopeFactory scopeFactory)
        {
            var subscriptionName = SubscriptionName.FromProjectSubscription("kwetter-308618", subscriptionId);
            var subscription = SubscriberClient.CreateAsync(subscriptionName).Result;

   
            return subscription.StartAsync((message, _) =>
            {
                var mediator = scopeFactory.CreateScope().ServiceProvider.GetRequiredService<IMediator>();
                var json = Encoding.UTF8.GetString(message.Data.ToArray());
        
                switch (subscriptionId)
                {
                    case "profileservice--user-created":
                        mediator.Send(JsonConvert.DeserializeObject<UserCreatedEvent>(json), _);
                        break;
                
                    case "profileservice--user-deleted":
                        mediator.Send(JsonConvert.DeserializeObject<UserDeletedEvent>(json), _);
                        break; 
                }
                
                return Task.FromResult(SubscriberClient.Reply.Ack);
            });
        }
    }
}
