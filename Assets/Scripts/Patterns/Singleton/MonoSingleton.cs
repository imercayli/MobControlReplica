using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(T).Name);
                    _instance = singletonObject.AddComponent<T>();
                }
            }
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            Initialize();
        }
        else
        {
            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    protected virtual void OnDestroy()
    {
        if (_instance == this)
        {
            Deinitialize();
            _instance = null;
        }
    }

    protected virtual void Initialize()
    {
        
    }

    protected virtual void Deinitialize()
    {
       
    }
}
