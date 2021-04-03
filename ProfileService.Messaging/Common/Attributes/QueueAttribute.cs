namespace ProfileService.Messaging.Common.Attributes
{
    using System;

    /// <summary>
    /// The queue attribute is used on messages and will specify the channel on which to publish the message.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class QueueAttribute : Attribute
    {
        /// <summary>
        /// Gets the name of the queue.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueueAttribute"/> class.
        /// </summary>
        /// <param name="name">The name of the queue.</param>
        public QueueAttribute(string name)
        {
            this.Name = name;
        }
    }
}