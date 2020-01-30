namespace Globalization.Abstraction
{
    public delegate void RemovingDelegate();

    public interface IManage : IInstable
    {
        bool IsReady { get; }

        void SetRemovingFunc(RemovingDelegate removingDelegate);

        System.Type GetManageType();
    }
}