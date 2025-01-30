using System;
using UnityEngine;
using System.Collections.Generic;

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
}
