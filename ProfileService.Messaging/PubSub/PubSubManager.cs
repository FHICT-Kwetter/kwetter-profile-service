// <copyright file="PubSubManager.cs" company="Kwetter">
//     Copyright Kwetter. All rights reserved.
// </copyright>
// <author>Dirk Heijnen</author>

namespace ProfileService.Messaging.PubSub
{
    using System;
    using KubeMQ.SDK.csharp.Events;
    using KubeMQ.SDK.csharp.Subscription;
    using KubeMQ.SDK.csharp.Tools;
    using Newtonsoft.Json;
    using ProfileService.Messaging.Configuration;

    /// <summary>
    /// Defines the pubsub manager.
    /// This object is responsible for communicating with KubeMQ's pub/sub API.
    /// </summary>
    public class PubSubManager
    {
        /// <summary>
        /// The <see cref="KubeMqOptions"/>.
        /// </summary>
        private readonly KubeMqOptions options;

        /// <summary>
        /// Initializes a new instance of the <see cref="PubSubManager"/> class.
        /// </summary>
        /// <param name="options">The kubemq server configuration.</param>
        public PubSubManager(KubeMqOptions options)
        {
            this.options = options;
        }

        /// <summary>
        /// Sends an event to KubeMQ pub/sub.
        /// </summary>
        /// <param name="eventParam">The event that needs to be send.</param>
        /// <param name="shouldPersist">Indicated whether the event should be persisted in the event store or not.</param>
        public void SendEvent(string channelName, object eventParam, bool shouldPersist)
        {
            var jsonSerializedEvent = JsonConvert.SerializeObject(eventParam);

            var channel = new Channel(new ChannelParameters
            {
                KubeMQAddress = this.options.KubeMqServerAddress,
                ClientID = this.options.ClientId,
                ChannelName = channelName,
                Store = shouldPersist,
            });

            // todo: Add other fields to event object like metadata.
            channel.SendEvent(new Event
            {
                Body = Converter.ToByteArray(jsonSerializedEvent),
            });
        }

        /// <summary>
        /// Will subscribe to an event channel and handle incoming events.
        /// </summary>
        /// <param name="eventChannel">The name of the event channel.</param>
        public void SubscribeToEvent(string eventChannel)
        {
            this.HandleSubscribe(new SubscribeRequest
            {
                Channel = eventChannel,
                SubscribeType = SubscribeType.Events,
                ClientID = this.options.ClientId,
            });
        }

        /// <summary>
        /// Will subscribe to an eventstore.
        /// </summary>
        /// <param name="eventChannel">The channel name.</param>
        /// <param name="storeType">The <see cref="EventsStoreType"/> indicating what type of events to receive.</param>
        public void SubscribeToEventStore(string eventChannel, EventsStoreType storeType)
        {
            this.HandleSubscribe(new SubscribeRequest
            {
                Channel = eventChannel,
                SubscribeType = SubscribeType.EventsStore,
                ClientID = this.options.ClientId,
                EventsStoreType = storeType,
                EventsStoreTypeValue = this.GetEventStoreTypeValue(storeType),
            });
        }

        private int GetEventStoreTypeValue(EventsStoreType storeType)
        {
            return storeType switch
            {
                EventsStoreType.Undefined => 0,
                EventsStoreType.StartNewOnly => 1,
                EventsStoreType.StartFromFirst => 2,
                EventsStoreType.StartFromLast => 3,
                EventsStoreType.StartAtSequence => 4,
                EventsStoreType.StartAtTime => 5,
                EventsStoreType.StartAtTimeDelta => 6,
                _ => throw new ArgumentOutOfRangeException(nameof(storeType), storeType, null),
            };
        }

        private void HandleSubscribe(SubscribeRequest subscribeRequest)
        {
            var subscriber = new Subscriber(this.options.KubeMqServerAddress);

            try
            {
                var eventCallback = this.GetHandleEventCallback();
                var errorCallback = this.GetHandleErrorCallback();

                subscriber.SubscribeToEvents(subscribeRequest, eventCallback, errorCallback);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception caught: {ex.Message}");
            }
        }

        private Subscriber.HandleEventDelegate GetHandleEventCallback()
        {
            return receive =>
            {
                Console.WriteLine($"Event received: Id: {receive.EventID} and Body: {Converter.FromByteArray(receive.Body)}");
            };
        }

        private Subscriber.HandleEventErrorDelegate GetHandleErrorCallback()
        {
            return error =>
            {
                Console.WriteLine($"Error received: {error.Message}");
            };
        }
    }
}