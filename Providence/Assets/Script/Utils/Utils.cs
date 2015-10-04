using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class Utils
{
    public static T RandomElement<T>(this List<T> list)
    {
        if (list.Count == 0)
            return default(T);
        return list[UnityEngine.Random.Range(0, list.Count)];
    }

    public static void SetRandomRotation(Transform transform)
    {
        transform.rotation = Quaternion.Euler(0, UnityEngine.Random.Range(-180, 180), 0);
    }

    public static void GroundTransform(Transform transform, float checkDist = 9999f)
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, Vector3.down, out hitInfo, checkDist))
        {
            var t = transform.position;
            float groundOffset = hitInfo.distance;
            transform.position = new Vector3(t.x, t.y - groundOffset, t.z);
        }
    }
}

