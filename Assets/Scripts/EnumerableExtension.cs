using System;
using System.Collections.Generic;
using System.Linq;

public static class EnumerableExtension
{
    public static T GetMinItem<T>(this IEnumerable<T> enumerable, Func<T, float> selector)
    {
        if(!enumerable.Any())
            throw new ArgumentException("Enumerable doesn't have any items!");

        var minValue = float.MaxValue;
        var minItem = default(T);
        
        foreach(var item in enumerable)
        {
            var currentValue = selector(item);
            if(minValue > currentValue)
            {
                minValue = currentValue;
                minItem = item;
            }
        }

        return minItem;
    }

    public static T GetRandom<T>(this IEnumerable<T> values)
    {
        var array = values.ToArray();
        var id = UnityEngine.Random.Range(0, array.Length);
        return array[id];
    }
}

