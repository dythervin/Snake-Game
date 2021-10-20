using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CollectionExtentions
{
    public static T GetRandom<T>(this IList<T> list)
        => list[Random.Range(0, list.Count)];

    public static T PopRandom<T>(this IList<T> list)
    {
        T obj = list.GetRandom();
        list.Remove(obj);
        return obj;
    }

    public static T GetRandom<T>(this T[] array)
        => array[Random.Range(0, array.Length)];
}
