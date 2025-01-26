using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolElement : MonoBehaviour
{
    private Action<PoolElement> _releaseCallback;
    private bool _destroyOnRelease;

    public void Initialize(Action<PoolElement> releaseCallback, bool destroyOnRelease)
    {
        _releaseCallback = releaseCallback;
        _destroyOnRelease = destroyOnRelease;
    }

    public void Release()
    {
        _releaseCallback?.Invoke(this);
            
        if (_destroyOnRelease) Destroy(gameObject);
    }
}
