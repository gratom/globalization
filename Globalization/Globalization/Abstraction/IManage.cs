namespace Globalization.Abstraction
{
    /// <summary>
    /// Delegate for removing manager from Servises
    /// </summary>
    public delegate void RemovingDelegate();

    /// <summary>
    /// Base interface for managers
    /// </summary>
    public interface IManage : IInstable
    {
        /// <summary>
        /// Is ready Manager to use it
        /// </summary>
        bool IsReady { get; }

        /// <summary>
        /// Function for Servise class. By this function you can remove manager from Servises. Note : only by this function.
        /// </summary>
        /// <param name="removingDelegate"></param>
        void SetRemovingFunc(RemovingDelegate removingDelegate);

        /// <summary>
        /// That function need to determine main interface type
        /// </summary>
        /// <returns>type of main interface</returns>
        System.Type GetManageType();
    }
}