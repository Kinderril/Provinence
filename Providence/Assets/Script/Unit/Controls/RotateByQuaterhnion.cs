using System;
using UnityEngine;
using System.Collections;

public class RotateByQuaterhnion : MonoBehaviour
{

    private float yy = 0;
    private const float waitTime = 1.6f;
    private const int border = 360;
    public float speed = 10;
    private float timeToOffWait;
    public int side = 0;
    public Vector3 lookDir;
    public Vector3 lastLookDir;
    public bool shallRotate = false;
    public bool shallWait = false;
    public float angle;
    private Quaternion lastQuaternion;
    private Action endLookRotation;
    private Action comeToRotation;


    public void Init(Action endLookRotation, Action comeToRotation)
    {
        this.endLookRotation = endLookRotation;
        this.comeToRotation = comeToRotation;
    }
    void LateUpdate ()
    {
        if (shallRotate)
        {
            yy = lastLookDir.y + Time.deltaTime*speed* side;
            yy = FixAngle(yy);

            lastLookDir = new Vector3(0, yy, 0);
            lastQuaternion = Quaternion.Euler(lastLookDir);
            transform.rotation = lastQuaternion;
            angle = Mathf.Abs(lastLookDir.y - lookDir.y);// Vector3.Angle(lastLookDir, lookDir);
            //Debug.Log("q:" + lastQuaternion + "  angle:" + angle + "  lookDir:" + lookDir + "   lastLookDir:" + lastLookDir);
            if (angle < 4)
            {
                timeToOffWait = waitTime + Time.time;
                shallWait = true;
                shallRotate = false;
                comeToRotation();
            }
        }
        else if (shallWait)
        {
            transform.rotation = lastQuaternion;
            if (timeToOffWait < Time.time)
            {
                shallWait = false;
                endLookRotation();
            }
        }

    }

    private float FixAngle(float a)
    {

        if (a > border)
        {
            a -= border;
        }
        else if (a < 0)
        {
            a += border;
        }
        return a;
    }

//    void Update()
//    {
//        transform.rotation = Quaternion.identity;
//    }


    public bool SetLookDir(Vector3 dir)
    {
        var ang = Vector3.Angle(dir, new Vector3(-1, 0, 0)) ;
        if (dir.z < 0)
        {
            ang *= -1;
            ang -= 57;
        }
        else
        {
           ang -= 57;
        }
        //var ang = Vector3.Angle(dir, new Vector3(1, 0, 0));
        ang = FixAngle(ang);

        lookDir = new Vector3(0,ang,0);
        lastLookDir = transform.rotation.eulerAngles;
        shallRotate = true;
        shallWait = false;
        var a = lookDir.y;
        var b = lastLookDir.y;
        float c;
        bool v = a > b;
        if (v)
        {
            c = a - b;
            if (c > 180)
            {
                side = -1;
            }
            else
            {
                side = 1;
            }
        }
        else
        {
            c = b - a;
            if (c < 180)
            {
                side = -1;
            }
            else
            {
                side = 1;
            }

        }
        return Mathf.Abs(c) < 4;

        //Debug.Log("SetNewDir lookDir:" + lookDir + "   last:" + lastLookDir + "   " + side + "   dir:" + dir);

    }
}
