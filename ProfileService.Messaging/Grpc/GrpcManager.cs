// <copyright file="GrpcManager.cs" company="Kwetter">
//     Copyright Kwetter. All rights reserved.
// </copyright>
// <author>Dirk Heijnen</author>

namespace ProfileService.Messaging.Grpc
{
    using System;
    using System.Threading.Tasks;
    using KubeMQ.SDK.csharp.CommandQuery;
    using KubeMQ.SDK.csharp.Subscription;
    using KubeMQ.SDK.csharp.Tools;
    using ProfileService.Messaging.Configuration;

    /// <summary>
    /// Defines the Grpc manager.
    /// This object is responsible for communicating with KubeMQ's Grpc API.
    /// </summary>
    public class GrpcManager
    {
        /// <summary>
        /// The <see cref="KubeMqOptions"/>.
        /// </summary>
        private readonly KubeMqOptions options;

        /// <summary>
        /// Initializes a new instance of the <see cref="GrpcManager"/> class.
        /// </summary>
        /// <param name="options">The kubemq server configuration.</param>
        public GrpcManager(KubeMqOptions options)
        {
            this.options = options;
        }


        public void SubscribeToCommands(string channelName)
        {
            var responder = new Responder(this.options.KubeMqServerAddress);

            var subscribeRequest = new SubscribeRequest
            {
                Channel = channelName,
                ClientID = this.options.ClientId,
                SubscribeType = SubscribeType.Commands,
            };

            try
            {
                var commandCallback = this.GetCommandCallback();
                var errorCallback = this.GetHandleErrorCallback();
                responder.SubscribeToRequests(subscribeRequest, commandCallback, errorCallback);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception caught: {ex.Message}");
            }
        }

        public void SubscribeToQueries(string channelName)
        {
            var responder = new Responder(this.options.KubeMqServerAddress);

            var subscribeRequest = new SubscribeRequest
            {
                Channel = channelName,
                ClientID = this.options.ClientId,
                SubscribeType = SubscribeType.Queries,
            };

            try
            {
                var queryCallback = this.GetQueryCallback();
                var errorCallback = this.GetHandleErrorCallback();
                responder.SubscribeToRequests(subscribeRequest, queryCallback, errorCallback);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception caught: {ex.Message}");
            }
        }

        public void SendQuery(string channelName)
        {
            var channel = new Channel(new ChannelParameters
            {
                KubeMQAddress = this.options.KubeMqServerAddress,
                ClientID = this.options.ClientId,
                RequestsType = RequestType.Query,
                ChannelName = channelName,
                Timeout = 1000,
            });

            try
            {
                var result = channel.SendRequest(new Request
                {
                    Body = Converter.ToByteArray("My test query"),
                });

                if (!result.Executed)
                {
                    Console.WriteLine($"Response error: {result.Error}");
                    return;
                }

                Console.WriteLine($"Response Received:{result.RequestID} ExecutedAt:{result.Timestamp}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception caught: {ex.Message}");
            }
        }

        public async Task SendQueryAsync(string channelName)
        {
            var channel = new Channel(new ChannelParameters
            {
                KubeMQAddress = this.options.KubeMqServerAddress,
                ClientID = this.options.ClientId,
                RequestsType = RequestType.Query,
                ChannelName = channelName,
                Timeout = 1000,
            });

            try
            {
                var result = await channel.SendRequestAsync(new Request
                {
                    Body = Converter.ToByteArray("My async test query"),
                });

                if (!result.Executed)
                {
                    Console.WriteLine($"Response error: {result.Error}");
                    return;
                }

                Console.WriteLine($"Response Received:{result.RequestID} ExecutedAt:{result.Timestamp}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception caught: {ex.Message}");
            }
        }

        public async Task SendCommandAsync(string channelName)
        {
            var channel = new Channel(new ChannelParameters
            {
                KubeMQAddress = this.options.KubeMqServerAddress,
                ClientID = this.options.ClientId,
                RequestsType = RequestType.Command,
                ChannelName = channelName,
                Timeout = 1000,
            });

            try
            {
                var result = await channel.SendRequestAsync(new Request
                {
                    Body = Converter.ToByteArray("My async test command"),
                });

                if (!result.Executed)
                {
                    Console.WriteLine($"Response error: {result.Error}");
                    return;
                }

                Console.WriteLine($"Response Received:{result.RequestID} ExecutedAt:{result.Timestamp}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception caught: {ex.Message}");
            }
        }

        public void SendCommand(string channelName)
        {
            var channel = new Channel(new ChannelParameters
            {
                KubeMQAddress = this.options.KubeMqServerAddress,
                ClientID = this.options.ClientId,
                RequestsType = RequestType.Command,
                ChannelName = channelName,
                Timeout = 1000,
            });

            try
            {
                var result = channel.SendRequest(new Request
                {
                    Body = Converter.ToByteArray("My test command"),
                });

                if (!result.Executed)
                {
                    Console.WriteLine($"Response error: {result.Error}");
                    return;
                }

                Console.WriteLine($"Response Received:{result.RequestID} ExecutedAt:{result.Timestamp}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception caught: {ex.Message}");
            }
        }

        private Responder.RespondDelegate GetQueryCallback()
        {
            return query =>
            {
                Console.WriteLine($"Command Received: Id:{query.RequestID} Body: {Converter.FromByteArray(query.Body)}");

                return new Response(query)
                {
                    // todo: Fill out this value with the proper command response.
                };
            };
        }

        private Responder.RespondDelegate GetCommandCallback()
        {
            return command =>
            {
                Console.WriteLine($"Command Received: Id:{command.RequestID} Body: {Converter.FromByteArray(command.Body)}");

                return new Response(command)
                {
                    // todo: Fill out this value with the proper command response.
                };
            };
        }

        private Responder.HandleCommandQueryErrorDelegate GetHandleErrorCallback()
        {
            return error =>
            {
                Console.WriteLine($"Error received: {error.Message}");
            };
        }
    }
}