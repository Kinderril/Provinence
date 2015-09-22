using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class Utils
{
    public static T RandomElement<T>(this List<T> list)
    {
        return list[UnityEngine.Random.Range(0, list.Count)];
    }

    public static void SetRandomRotation(Transform transform)
    {
        transform.rotation = Quaternion.Euler(0, UnityEngine.Random.Range(-180, 180), 0);
    }
}

