using System;
using System.Collections.Generic;
using System.Linq;

public static class EnumerableExtension
{
    public static T GetMaxItem<T>(this IEnumerable<T> enumerable, Func<T, float> selector)
    {
        if(!enumerable.Any())
            throw new ArgumentException("Enumerable doesn't have any items!");

        var maxValue = float.MinValue;
        var maxItem = default(T);
        
        foreach(var item in enumerable)
        {
            var currentValue = selector(item);
            if(maxValue < currentValue)
            {
                maxValue = currentValue;
                maxItem = item;
            }
        }

        return maxItem;
    }

    public static T GetRandom<T>(this IEnumerable<T> values)
    {
        var array = values.ToArray();
        var id = UnityEngine.Random.Range(0, array.Length);
        return array[id];
    }
}

