using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace MercuryCommons.Utilities;

public static class ReflectionUtilities
{
    public static IList<T> GetSubclassesOfType<T>(IServiceProvider provider = null) where T : class => GetSubclassesOfType<T>(typeof(T), null, provider);
    public static IList<T> GetSubclassesOfType<T>(Func<Type, bool> func, IServiceProvider provider = null) where T : class => GetSubclassesOfType<T>(typeof(T), func, provider);
    public static IList<T> GetSubclassesOfType<T>(Type type, Func<Type, bool> func = null, IServiceProvider provider = null) where T : class
    {
        var ret = new List<T>();
        var types = type.Assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(T)) && !t.IsAbstract);
        if (func != null) types = types.Where(func);

        foreach (var t in types)
        {
            var inst = provider != null ? ActivatorUtilities.CreateInstance(provider, t) : Activator.CreateInstance(t);
            if (inst is T retT) ret.Add(retT);
        }

        return ret;
    }
}