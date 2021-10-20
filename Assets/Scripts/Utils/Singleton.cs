using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T _instance;
    private static bool _initialized;
    public static T Instance
    {
        get
        {
            if (_initialized)
                return _instance;

            Instance = FindObjectOfType<T>(true);
            return _instance;
        }

        set
        {
            _instance = value;
            _initialized = value != null;
        }
    }

    protected virtual void Awake()
    {
        if (!_initialized)
        {
            Instance = (T)this;
        }
        else
        {
            if (_instance != this)
            {
                Debug.LogWarning("[Singleton] Trying to instantiate a second instance of a singleton class.");
                Destroy(this);
            }
        }
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            Instance = null;
        }
    }
}
