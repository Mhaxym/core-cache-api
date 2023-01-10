

using TodoApi.Cache;

namespace TodoAPi.Cache;

public sealed class ManagerSingleton
{
    private static volatile HYBCacheManager? _manager;
    private static readonly object _syncLock = new object();
    public static HYBCacheManager GetManager()
    {
        if (_manager == null)
        {
            lock (_syncLock)
            {
                if (_manager == null)
                {
                    _manager = new HYBCacheManager();
                }
            }
        }
        return _manager;
    }
}