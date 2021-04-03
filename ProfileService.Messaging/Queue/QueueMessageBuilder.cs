namespace ProfileService.Messaging.Queue
{
    using KubeMQ.Grpc;
    using KubeMQ.SDK.csharp.Queue;
    using KubeMQ.SDK.csharp.Tools;

    public class QueueMessageBuilder
    {
        private Message message;

        public QueueMessageBuilder()
        {
            this.message = new Message
            {
                Body = null,
                Policy = new QueueMessagePolicy(),
                Metadata = string.Empty,
            };
        }

        public QueueMessageBuilder AddBody(object body)
        {
            this.message.Body = Converter.ToByteArray(body);
            return this;
        }

        /// <summary>
        /// Adds a expiration amount to the message.
        /// </summary>
        /// <param name="expirationInSeconds">The amount of seconds untill expiration.</param>
        /// <returns>The <see cref="QueueMessageBuilder"/>.</returns>
        public QueueMessageBuilder AddExpiration(int expirationInSeconds)
        {
            this.message.Policy.ExpirationSeconds = expirationInSeconds;
            return this;
        }

        /// <summary>
        /// Adds a delay to the message.
        /// </summary>
        /// <param name="delayInSeconds">The delay in seconds.</param>
        /// <returns>The <see cref="QueueMessageBuilder"/>.</returns>
        public QueueMessageBuilder AddDelay(int delayInSeconds)
        {
            this.message.Policy.DelaySeconds = delayInSeconds;
            return this;
        }

        /// <summary>
        /// Adds a metadata string to the message.
        /// </summary>
        /// <param name="metaData">The metadata.</param>
        /// <returns>The <see cref="QueueMessageBuilder"/>.</returns>
        public QueueMessageBuilder AddMetaData(string metaData)
        {
            this.message.Metadata = metaData;
            return this;
        }

        /// <summary>
        /// Build the message object.
        /// </summary>
        /// <returns>The message.</returns>
        public Message Build()
        {
            return this.message;
        }
    }
}