using UnityEngine;

public class SingletonMono<T> : MonoBehaviour where T : SingletonMono<T>
{
    public bool IsDontDestroy = true;

    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {

                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    Debug.LogError("씬 내에 " + typeof(T).ToString() + " 이(가) 존재하지 않습니다.");
#if UNITY_EDITOR
                    Debug.Break();
#endif
                }

            }

            return instance;
        }
    }
    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;

            if (IsDontDestroy)
                DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected virtual void OnDestroy()
    {
        instance = null;
    }
}


