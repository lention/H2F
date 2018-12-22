using System;
using System.Collections.Generic;
using System.Text;

namespace H2F.Standard .Common.Collections.Extensions
{
    public static class DictionnaryExtensions
    {
        public static bool TryGetValue<T>(this Dictionary<string, object> dictionary, string key, out T value)
        {
            object valuObj;
            if (dictionary.TryGetValue(key,out valuObj) && valuObj is T)
            {
                value = (T)valuObj;
                return true;
            }

            value = default(T);
            return false;
        }

        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            TValue obj;
            return dictionary.TryGetValue(key, out obj) ? obj : default(TValue);
        }

        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TKey, TValue> factory)
        {
            TValue obj;
            if (dictionary.TryGetValue(key, out obj))
            {
                return obj;
            }

            return dictionary[key] = factory(key);
        }

        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> factory)
        {
            return dictionary.GetOrAdd(key, k => factory());
        }
    }
}
