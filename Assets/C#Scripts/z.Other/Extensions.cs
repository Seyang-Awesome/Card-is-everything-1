using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class MyExtensions
{
    public static Vector2 MousePosition => Camera.main.ScreenToWorldPoint(Input.mousePosition);

    public static List<T> Shuffled<T>(this List<T> list)
    {
        return list.OrderBy(x => Random.Range(0, 1)).ToList();
    }
}