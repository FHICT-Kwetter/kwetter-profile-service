// <copyright file="ChannelAttribute.cs" company="Kwetter">
//     Copyright Kwetter. All rights reserved.
// </copyright>
// <author>Dirk Heijnen</author>

namespace ProfileService.Messaging.Common.Attributes
{
    using System;

    /// <summary>
    /// The channel attribute is used on events, command and queries to set the channel they will be published to.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class ChannelAttribute : Attribute
    {
        /// <summary>
        /// Gets the topic of the channel.
        /// </summary>
        public string Topic { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChannelAttribute"/> class.
        /// </summary>
        /// <param name="topic">The topic of the channel.</param>
        public ChannelAttribute(string topic)
        {
            this.Topic = topic;
        }
    }
}