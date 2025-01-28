using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public abstract class FactoryBase<T> : MonoBehaviour where T: MonoBehaviour
{
    [SerializeField] protected T prefab;
    
    public virtual T CreateInstance(Vector3 position,Quaternion rotation,Transform parent=null)
    {
        if (prefab == null)
        {
            Debug.LogError($"{typeof(T).Name} prefab not assigned in {GetType().Name}");
            return null;
        }
        
        T instance = LeanPool.Spawn(prefab,position, rotation, parent ?? transform);
        return instance;
    }
}
