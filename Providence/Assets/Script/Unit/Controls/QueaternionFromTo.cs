using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class QueaternionFromTo : MonoBehaviour
{
    public Vector3 to;
    public Vector3 from;
    private Quaternion qTo;
    private Quaternion qFrom;
    private Quaternion qCur;
    private float time;
    public float speed;

    void Start()
    {
        Debug.Log("1:" + from + "     " + to);
        //        ///**/Debug.Log("2:" + to + "     " + transform.eulerAngles);
//        ;
        qFrom = Quaternion.LookRotation(from);
        qTo = Quaternion.FromToRotation(from, to);
        transform.rotation = qFrom;
//        var q = Quaternion.AngleAxis(to.y, Vector3.up);
//        Quaternion.
        Debug.Log("3:" + qTo + "       " + qTo.eulerAngles);

//        transform.rotation = q;
    }

    void Update()
    {
//        time += Time.deltaTime*speed;
//        qCur = Quaternion.Lerp(qFrom, qTo, time);
//        transform.rotation = qCur;
//        Debug.Log(time + "    " + qCur);
    }
}

