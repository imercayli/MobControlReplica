using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    public static T Instance;

    protected virtual void Awake()
    {
        if (Instance)
        {
            Destroy(this);
            return;
        }

        Instance = this as T;
    }
}
