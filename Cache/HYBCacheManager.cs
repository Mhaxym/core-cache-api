using System.Reflection;
using TodoAPi.Cache;

namespace TodoApi.Cache;

public class HYBCacheManager
{
    private Dictionary<string, BaseManager> _MANAGERS { get; set; } = new Dictionary<string, BaseManager>();
    private Dictionary<string, Object> _MANAGER_LOCKS { get; set; } = new Dictionary<string, Object>();

    public static T GetManager<T>(string managerName) where T : BaseManager
    {
        return ManagerSingleton.GetManager().Get<T>(managerName);
    }

    public HYBCacheManager()
    {
        foreach (Type type in
            Assembly.GetAssembly(typeof(BaseManager))
                .GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(BaseManager))))
        {
            _MANAGER_LOCKS[type.Name] = new Object();
        }
    }

    public T Get<T>(string managerName) where T : BaseManager
    {
        if (!_MANAGERS.ContainsKey(managerName))
        {
            lock (_MANAGER_LOCKS[managerName])
            {
                if (!_MANAGERS.ContainsKey(managerName))
                {
                    var manager_instance = Activator.CreateInstance(typeof(T), true);
                    if (manager_instance != null)
                    {
                        _MANAGERS[managerName] = (T)manager_instance;
                        _MANAGERS[managerName].Load();
                    }
                }
            }
        }
        return (T)_MANAGERS[managerName];
    }
}

