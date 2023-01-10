namespace TodoApi.Cache
{

    public interface IManager
    {
        void Load();
    }
    public abstract class BaseManager : IManager
    {
        public abstract void Load();
    }
}