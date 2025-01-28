using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Lean.Pool;

public abstract class MultiFactoryBase<TKey, TProduct, TFactory> : BaseService<TFactory>
    where TProduct : MonoBehaviour
    where TFactory : MultiFactoryBase<TKey, TProduct, TFactory>
{
    [SerializeField] protected List<PrefabMapping<TKey, TProduct>> prefabMappings;
    
    protected Dictionary<TKey, TProduct> prefabMap;

    protected override void Awake()
    {
        base.Awake();
        prefabMap = prefabMappings.ToDictionary(x => x.Key, x => x.Prefab);
    }

    public virtual TProduct CreateInstance(TKey key, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        if (!prefabMap.TryGetValue(key, out TProduct prefab))
        {
            Debug.LogError($"{typeof(TProduct).Name} prefab for key {key} not found in {GetType().Name}");
            return null;
        }

        TProduct instance = LeanPool.Spawn(prefab, position, rotation, parent ?? transform);
        return instance;
    }
}

[System.Serializable]
public class PrefabMapping<TKey, TProduct>
{
    public TKey Key;
    public TProduct Prefab;
}