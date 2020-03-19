namespace Globalization.Tools
{
    /// <summary>
    /// Simple debag for everyone, why add callback to events
    /// </summary>
    public static class Debugger
    {
        /// <summary>
        /// Delegate type for messages
        /// </summary>
        /// <param name="message">your message</param>
        public delegate void DebugDelegate(string message);

        /// <summary>
        /// Event for ordinary messages
        /// </summary>
        public static event DebugDelegate NewMessageEvent;

        /// <summary>
        /// Event errors
        /// </summary>
        public static event DebugDelegate NewErrorEvent;

        /// <summary>
        /// Send new message
        /// </summary>
        /// <param name="message">your message</param>
        public static void SetMessage(string message)
        {
            NewMessageEvent?.Invoke(message);
        }

        /// <summary>
        /// Send new error message
        /// </summary>
        /// <param name="error">your error</param>
        public static void SetError(string error)
        {
            NewErrorEvent?.Invoke(error);
        }
    }
}