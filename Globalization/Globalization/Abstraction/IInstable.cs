namespace Globalization.Abstraction
{
    public enum InstableEventType
    {
        onReady,
        onInitStart,
        beforeDestroy
    }

    public interface IInstable : IEventable<InstableEventType>
    {
    }
}