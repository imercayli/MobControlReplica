using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ServiceSystem
{
    private static Dictionary<Type, object> services = new Dictionary<Type, object>();
    
    public static void AddService<T>(object service) where T : IService
    {
        if (services.ContainsKey(typeof(T)))
        {
            Debug.LogError(typeof(T) + " service is already registered.");
            return;
        }

        services.Add(typeof(T), service);
    }

    public static T GetService<T>() where T : IService
    {
        var isExists = services.TryGetValue(typeof(T), out object value);

        if (isExists)
        {
            return (T)value;
        }

        var service = GameObject.FindObjectOfType<BaseService<T>>();
        if (service == null)
        {
            Debug.LogError("There is no service : " + typeof(T));
            return default;
        }

        service.Init();
        services.TryGetValue(typeof(T), out value);
        return (T)value;
    }

    public static bool HasService<T>() where T : IService
    {
        return services.TryGetValue(typeof(T), out object value);
    }

    public static void RemoveService<T>(object service) where T : IService
    {
        if (services.ContainsKey(typeof(T)))
        {
            services.Remove(typeof(T));
        }
    }
}
