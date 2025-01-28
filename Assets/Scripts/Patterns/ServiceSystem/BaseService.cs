using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent, DefaultExecutionOrder(-99)]
public abstract class BaseService<T> : MonoBehaviour, IService where T : IService
{
    public bool IsInitialized { get; private set; } = false;
    
    protected virtual void Awake()
    {
        Init();
    }

    public virtual void Start()
    {
            
    }

    public void Init()
    {
        if (IsInitialized)
        {
            return;
        }

        ServiceSystem.AddService<T>(this);
        Initialize();
        IsInitialized = true;
    }

    private void OnDestroy()
    {
        ServiceSystem.RemoveService<T>(this);
        DeInitialize();
    }

    public virtual void Initialize()
    {
    }

    public virtual void DeInitialize()
    {
    }
}
