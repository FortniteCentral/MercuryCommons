using System;
using System.Collections.Generic;
using System.Linq;

namespace MercuryCommons.Utilities;

public static class ReflectionUtilities
{
    public static IList<T> GetSubclassesOfType<T>() where T : class => GetSubclassesOfType<T>(typeof(T));
    public static IList<T> GetSubclassesOfType<T>(Func<Type, bool> func) where T : class => GetSubclassesOfType<T>(typeof(T), func);
    public static IList<T> GetSubclassesOfType<T>(Type type, Func<Type, bool> func = null) where T : class
    {
        var ret = new List<T>();
        var types = type.Assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(T)));
        if (func != null) types = types.Where(func);

        foreach (var t in types)
        {
            var inst = Activator.CreateInstance(t);
            if (inst is T retT) ret.Add(retT);
        }

        return ret;
    }
}