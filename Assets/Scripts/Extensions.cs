using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Random useful extension methods.
/// </summary>
public static class Extensions {
    /// <summary>
    /// Returns a random element from a list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <returns></returns>
    public static T Rand<T>(this IList<T> list) {
        return list[UnityEngine.Random.Range(0, list.Count)];
    }

    /// Returns the next n elements from a list.
    public static T[] Next<T>(this IList<T> list, int index, int count) {
        if (list == null || index < 0 || index >= list.Count)
            return new T[0];

        int length = Mathf.Min(count, list.Count - index + 1);
        return list.Skip(index + 1).Take(length).ToArray();
    }
}
