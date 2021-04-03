namespace ProfileService.Messaging.Common.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Class)]
    public class EventTypeAttribute : Attribute
    {
        /// <summary>
        /// Gets the name of the event.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventTypeAttribute"/> class.
        /// </summary>
        /// <param name="name">The name of the event.</param>
        public EventTypeAttribute(string name)
        {
            this.Name = name;
        }
    }
}