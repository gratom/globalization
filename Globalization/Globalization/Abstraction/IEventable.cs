namespace Globalization.Abstraction
{
    /// <summary>
    /// Delegate for IEventable types
    /// </summary>
    public delegate void EventableDelegate();

    /// <summary>
    /// Interface for any classes, that have events
    /// </summary>
    /// <typeparam name="EventTypes">Enum type that shows what types of events can occur in a child class</typeparam>
    public interface IEventable<EventTypes> where EventTypes : System.Enum
    {
        /// <summary>
        /// Add your listener to one of events
        /// </summary>
        /// <param name="eventType">type of event</param>
        /// <param name="eventableDelegate">delegate of your listener</param>
        void AddListener(EventTypes eventType, EventableDelegate eventableDelegate);

        /// <summary>
        /// Remove your listener from one of events. Note, that you need to instance of this delegate for correct deleting
        /// </summary>
        /// <param name="eventType">type of event</param>
        /// <param name="eventableDelegate">your delegate instance</param>
        void RemoveListener(EventTypes eventType, EventableDelegate eventableDelegate);
    }
}