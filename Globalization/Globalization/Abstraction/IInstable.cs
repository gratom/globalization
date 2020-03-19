namespace Globalization.Abstraction
{
    /// <summary>
    /// Enum for events in Instable types
    /// </summary>
    public enum InstableEventType
    {
        /// <summary>
        /// calls, when instance is ready to use
        /// </summary>
        onReady,

        /// <summary>
        /// calls, when initialize started
        /// </summary>
        onInitStart,

        /// <summary>
        /// calls, before instance removed
        /// </summary>
        beforeDestroy
    }

    /// <summary>
    /// Interface for types that have a single instance. For example, for managers.
    /// </summary>
    public interface IInstable : IEventable<InstableEventType>
    {
    }
}