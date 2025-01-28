using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public abstract class FactoryBase<TProduct, TFactory> : BaseService<TFactory>
    where TProduct : MonoBehaviour
    where TFactory : FactoryBase<TProduct, TFactory>
{
    [SerializeField] protected TProduct prefab;

    public virtual TProduct CreateInstance(Vector3 position, Quaternion rotation, Transform parent = null)
    {
        if (prefab == null)
        {
            Debug.LogError($"{typeof(TProduct).Name} prefab not assigned in {GetType().Name}");
            return null;
        }

        TProduct instance = LeanPool.Spawn(prefab, position, rotation, parent ?? transform);
        return instance;
    }
}
