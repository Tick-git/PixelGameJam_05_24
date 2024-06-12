using UnityEngine;

public class PersistentSingleton<T> : MonoBehaviour where T : Component
{
    public bool AutoUnparentOnAwake = true;

    protected static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();

                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    _instance = obj.AddComponent<T>();
                }
            }

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        InitializeSingleton();
    }

    void InitializeSingleton()
    {
        if (!Application.isPlaying) return;

        if(AutoUnparentOnAwake)
        {
            transform.SetParent(null);
        }
        
        if(_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            if(Instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}
