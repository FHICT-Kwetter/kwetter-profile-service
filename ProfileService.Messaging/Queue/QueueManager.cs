// <copyright file="QueueManager.cs" company="Kwetter">
//     Copyright Kwetter. All rights reserved.
// </copyright>
// <author>Dirk Heijnen</author>

namespace ProfileService.Messaging.Queue
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using KubeMQ.SDK.csharp.Queue;
    using KubeMQ.SDK.csharp.Tools;
    using ProfileService.Messaging.Configuration;

    /// <summary>
    /// This object is responsible for communicating with KubeMQ's Queue API.
    /// Docs: https://docs.kubemq.io/learn/message-patterns/queue
    /// </summary>
    public class QueueManager
    {
        /// <summary>
        /// The <see cref="KubeMqOptions"/>.
        /// </summary>
        private readonly KubeMqOptions options;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueueManager"/> class.
        /// </summary>
        /// <param name="options">The kubemq server configuration.</param>
        public QueueManager(KubeMqOptions options)
        {
            this.options = options;
        }

        /// <summary>
        /// Adds a message into the FIFO queue for processing.
        /// </summary>
        /// <param name="queueName">The name of the queue to send the message to.</param>
        /// <param name="messageBuilder">The labmda expression for constructing a message.</param>
        public void SendMessage(string queueName, Action<QueueMessageBuilder> messageBuilder)
        {
            var builder = new QueueMessageBuilder();
            messageBuilder(builder);

            var queue = new Queue(queueName, this.options.ClientId, this.options.KubeMqServerAddress);

            var result = queue.SendQueueMessage(builder.Build());

            if (result.IsError)
            {
                Console.WriteLine($"QueueManager.SendMessage Error: {result.Error}");
            }
        }

        /// <summary>
        /// Adds a batch of messages into the FIFO queue for processing.
        /// </summary>
        /// <param name="messages">The batch of messages to be send.</param>
        /// <param name="queueName">The name of the queue to send the batch of messages to.</param>
        public void SendBatchMessages(IEnumerable<Message> messages, string queueName)
        {
            var queue = new Queue(queueName, this.options.ClientId, this.options.KubeMqServerAddress);

            var result = queue.SendQueueMessagesBatch(messages);

            foreach (var error in result.Results.Where(res => res.IsError).ToList())
            {
                Console.WriteLine($"QueueManager.SendBatchMessages Error: {error}");
            }
        }

        /// <summary>
        /// Reads messages from a queue.
        /// </summary>
        /// <param name="queueName">The name of the queue to read from.</param>
        /// <param name="amountOfMessagesToConsume">The number of message to read from the queue.</param>
        /// <returns>A list containing the read messages.</returns>
        public IEnumerable<Message> ReceiveMessages(string queueName, int amountOfMessagesToConsume)
        {
            var queue = new Queue(queueName, this.options.ClientId, this.options.KubeMqServerAddress);

            var result = queue.ReceiveQueueMessages(amountOfMessagesToConsume);

            if (result.IsError)
            {
                Console.WriteLine($"QueueManager.ReceiveMessages Error: {result.Error}");
            }

            Console.WriteLine($"QueueManager.ReceiveMessages Amount received: {result.MessagesReceived}");

            foreach (var message in result.Messages)
            {
                Console.WriteLine($"QueueManager.ReceiveMessages MessageId: {message.MessageID}, Body: {Converter.FromByteArray(message.Body)}");
            }

            return result.Messages;
        }

        /// <summary>
        /// Peeks for messages in a queue, this method will read messages but not take them from the queue.
        /// </summary>
        /// <param name="queueName">The name of the queue to read from.</param>
        /// <param name="amountOfMessagesToPeek">The number of message to read from the queue.</param>
        /// <returns>A list containing the peeked messages.</returns>
        public IEnumerable<Message> PeekMessages(string queueName, int amountOfMessagesToPeek)
        {
            var queue = new Queue(queueName, this.options.ClientId, this.options.KubeMqServerAddress);

            var result = queue.PeekQueueMessage(amountOfMessagesToPeek);

            if (result.IsError)
            {
                Console.WriteLine($"QueueManager.ReceiveMessages Error: {result.Error}");
            }

            Console.WriteLine($"QueueManager.ReceiveMessages Amount received: {result.MessagesReceived}");

            foreach (var message in result.Messages)
            {
                Console.WriteLine($"QueueManager.ReceiveMessages MessageId: {message.MessageID}, Body: {Converter.FromByteArray(message.Body)}");
            }

            return result.Messages;
        }
    }
}