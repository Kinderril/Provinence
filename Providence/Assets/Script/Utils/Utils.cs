using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class Utils
{
    public static T RandomElement<T>(this List<T> list)
    {
        return list[UnityEngine.Random.Range(0, list.Count)];
    }
}

