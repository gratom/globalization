using System.Collections.Generic;
using System.Linq;

namespace Globalization.Global
{
    using Globalization.Abstraction;
    using Globalization.Tools;

    /// <summary>
    /// Class for universal aсcess to all managers
    /// </summary>
    public static class Services
    {
        /// <summary>
        /// Callback type for deferred functions. You can add your function to moment of some manager ready to use,
        /// and it will be call just in that time
        /// </summary>
        /// <typeparam name="T">Type of manager, that you wait to use</typeparam>
        /// <param name="managerInstance">Instance of this manager</param>
        public delegate void DeferredCallBack<T>(T managerInstance) where T : class, IManage;

        private static List<IManage> baseManagers;

        private static Dictionary<System.Type, List<DeferredCallBack<IManage>>> callBackDictionary;

        static Services()
        {
            baseManagers = new List<IManage>();
            callBackDictionary = new Dictionary<System.Type, List<DeferredCallBack<IManage>>>();
        }

        #region public functions

        /// <summary>
        /// Get service/manager that you want to use
        /// </summary>
        /// <typeparam name="T">Type of service/manager</typeparam>
        /// <returns>Instance of service/manager</returns>
        public static T GetService<T>() where T : class, IManage
        {
            return (T)baseManagers.FirstOrDefault(x => x.GetManageType() == typeof(T));
        }

        /// <summary>
        /// Get service/manager that you want to use
        /// </summary>
        /// <param name="type">Type of service/manager</param>
        /// <returns>Instance of service/manager</returns>
        public static IManage GetService(System.Type type)
        {
            return baseManagers.FirstOrDefault(x => x.GetManageType() == type);
        }

        /// <summary>
        /// Set new service/manager to public using
        /// </summary>
        /// <typeparam name="T">Type of your service/manager</typeparam>
        /// <param name="baseManagerInstance">Instance of your service/manager</param>
        /// <returns>Success or fail</returns>
        public static bool SetService<T>(T baseManagerInstance) where T : class, IManage
        {
            if (baseManagerInstance != null)
            {
                if (baseManagers.Exists(x => x.GetManageType() == typeof(T)))
                {
                    Debugger.SetMessage("Manager, that you want to attach, already exist.\nManager type is " + typeof(T).ToString());
                    return false;
                }
                baseManagers.Add(baseManagerInstance);
                baseManagerInstance.SetRemovingFunc(DeleteService<T>);
                CheckCallbacks<T>();
                return true;
            }
            Debugger.SetMessage("Manager, that you want to attach, is null.\nManager type is " + typeof(T).ToString());
            return false;
        }

        /// <summary>
        /// Set new service/manager to public using
        /// </summary>
        /// <param name="baseManagerInstance">Instance of your service/manager</param>
        /// <returns>Success of fail</returns>
        public static bool SetService(IManage baseManagerInstance)
        {
            if (baseManagerInstance != null)
            {
                if (baseManagerInstance.GetType().IsClass)
                {
                    if (baseManagers.Exists(x => x.GetManageType() == baseManagerInstance.GetManageType()))
                    {
                        Debugger.SetMessage("Manager, that you want to attach, already exist.\nManager type is " + baseManagerInstance.GetManageType().ToString());
                        return false;
                    }
                    baseManagers.Add(baseManagerInstance);
                    baseManagerInstance.SetRemovingFunc(() => { DeleteService(baseManagerInstance.GetManageType()); });
                    CheckCallbacks(baseManagerInstance.GetManageType());
                    return true;
                }
                else
                {
                    Debugger.SetMessage("Manager, that you want to attach, is not a class.\nManager type is " + baseManagerInstance.GetType().ToString());
                    return false;
                }
            }
            Debugger.SetMessage("Manager, that you want to attach, is null.\nManager type is " + baseManagerInstance.GetType().ToString());
            return false;
        }

        /// <summary>
        /// Function for set deferred call back to moment ready one of managers/services
        /// </summary>
        /// <typeparam name="T">Type of manager</typeparam>
        /// <param name="deferredCallBackInstance">Instance of callback function</param>
        public static void CreateDeferredCallBack<T>(DeferredCallBack<T> deferredCallBackInstance) where T : class, IManage
        {
            if (deferredCallBackInstance != null)
            {
                T service = GetService<T>();
                if (service != null)
                {
                    deferredCallBackInstance(service);
                    return;
                }
                else
                {
                    if (callBackDictionary.ContainsKey(typeof(T)))
                    {
                        callBackDictionary[typeof(T)].Add((DeferredCallBack<IManage>)(object)deferredCallBackInstance);
                        return;
                    }
                    else
                    {
                        callBackDictionary.Add(typeof(T), new List<DeferredCallBack<IManage>>());
                        callBackDictionary[typeof(T)].Add((DeferredCallBack<IManage>)(object)deferredCallBackInstance);
                        return;
                    }
                }
            }
            else
            {
                Debugger.SetMessage("You try to create null callback. For manager " + typeof(T));
            }
        }

        #endregion public functions

        #region private functions

        private static void DeleteService<T>() where T : class, IManage
        {
            bool isOK = baseManagers.Remove(baseManagers.FirstOrDefault(x => x.GetManageType() == typeof(T)));
            if (!isOK)
            {
                Debugger.SetMessage("Manager, that you want to remove, is not exist.\nManager type is " + typeof(T).ToString());
            }
        }

        private static void DeleteService(System.Type type)
        {
            bool isOK = baseManagers.Remove(baseManagers.FirstOrDefault(x => x.GetManageType() == type));
            if (!isOK)
            {
                Debugger.SetMessage("Manager, that you want to remove, is not exist.\nManager type is " + type.ToString());
            }
        }

        private static void CheckCallbacks<T>() where T : class, IManage
        {
            if (callBackDictionary.ContainsKey(typeof(T)))
            {
                T service = GetService<T>();
                if (service.IsReady)
                {
                    MakeCallBack(service);
                }
                else
                {
                    EventableDelegate anon = () => { };
                    anon = () =>
                    {
                        CheckCallbacks<T>();
                        service.RemoveListener(InstableEventType.onReady, anon);
                    };
                    service.AddListener(InstableEventType.onReady, anon);
                }
            }
        }

        private static void CheckCallbacks(System.Type type)
        {
            if (callBackDictionary.ContainsKey(type))
            {
                IManage service = GetService(type);
                if (service.IsReady)
                {
                    MakeCallBack(service);
                }
                else
                {
                    EventableDelegate anon = () => { };
                    anon = () =>
                    {
                        CheckCallbacks(service.GetManageType());
                        service.RemoveListener(InstableEventType.onReady, anon);
                    };
                    service.AddListener(InstableEventType.onReady, anon);
                }
            }
        }

        private static void MakeCallBack<T>(T service) where T : class, IManage
        {
            DeferredCallBack<IManage>[] deferredCallBacksCopy = new DeferredCallBack<IManage>[callBackDictionary[service.GetManageType()].Count];
            callBackDictionary[service.GetManageType()].CopyTo(deferredCallBacksCopy);
            for (int i = 0; i < deferredCallBacksCopy.Length; i++)
            {
                deferredCallBacksCopy[i](service);
            }
        }

        #endregion private functions
    }
}