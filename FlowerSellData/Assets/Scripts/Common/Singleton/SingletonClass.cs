
public class SingletonClass<T> where T : new()
{
    private static readonly object _lock = new object();
    private static T instance = default(T);

    public static T Instance
    {
        get
        {
            lock (_lock)
            {
                if (instance == null)
                {
                    instance = new T();
                }
                return instance;
            }
        }
    }

}
