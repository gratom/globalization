namespace Globalization.Abstraction
{
    public delegate void EventableDelegate();

    public interface IEventable<EventTypes> where EventTypes : System.Enum
    {
        void AddListener(EventTypes eventType, EventableDelegate eventableDelegate);

        void RemoveListener(EventTypes eventType, EventableDelegate eventableDelegate);
    }
}