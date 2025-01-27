using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Lean.Pool;

public abstract class MultiFactoryBase<TKey, T> : MonoBehaviour where T : Behaviour
{
    [SerializeField] protected List<PrefabMapping<TKey, T>> prefabMappings;

    protected Dictionary<TKey, T> prefabMap;

    protected virtual void Awake()
    {
        prefabMap = prefabMappings.ToDictionary(x => x.Key, x => x.Prefab);
    }

    public virtual T CreateInstance(TKey key, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        if (!prefabMap.TryGetValue(key, out T prefab))
        {
            Debug.LogError($"{typeof(T).Name} prefab for key {key} not found in {GetType().Name}");
            return null;
        }

        T instance = LeanPool.Spawn(prefab, position, rotation, parent ?? transform);
        return instance;
    }
}

[System.Serializable]
public class PrefabMapping<TKey, T>
{
    public TKey Key;
    public T Prefab;
}