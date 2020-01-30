namespace Globalization.Tools
{
    public static class Debugger
    {
        public delegate void DebugDelegate(string message);

        public static event DebugDelegate newMessageEvent;

        public static void SetExeption(string exeption)
        {
            newMessageEvent?.Invoke(exeption);
        }
    }
}