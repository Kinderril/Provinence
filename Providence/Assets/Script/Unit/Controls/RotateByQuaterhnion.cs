using UnityEngine;
using System.Collections;

public class RotateByQuaterhnion : MonoBehaviour
{

    private float yy = 0;
    private const float waitTime = 2f;
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

    void LateUpdate ()
    {
        if (shallRotate)
        {
            yy = lastLookDir.y + Time.deltaTime*speed* side;
            if (yy > border)
            {
                yy -= border;
            }else if (yy < 0)
            {
                yy += border;
            }

            lastLookDir = new Vector3(0, yy, 0);
            lastQuaternion = Quaternion.Euler(lastLookDir);
            transform.rotation = lastQuaternion;
            angle = Mathf.Abs(lastLookDir.y - lookDir.y);// Vector3.Angle(lastLookDir, lookDir);
//            Debug.Log("q:" + q + "  angle:" + angle + "  lookDir:" + lookDir + "   lastLookDir:" + lastLookDir);
            if (angle < 4)
            {
                timeToOffWait = waitTime + Time.time;
                shallWait = true;
                shallRotate = false;
            }
        }
        else if (shallWait)
        {
            transform.rotation = lastQuaternion;
            if (timeToOffWait < Time.time)
            {
                shallWait = false;
            }
        }

    }

//    void Update()
//    {
//        transform.rotation = Quaternion.identity;
//    }


    public void SetLookDir(Vector3 dir)
    {
        lookDir =
           lastLookDir = transform.rotation.eulerAngles;
        shallRotate = true;
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
        Debug.Log("SetNewDir " + lastLookDir + "   " + side + "   " + v);

    }
    public void SetNewDir()
    {
        var rndVec = new Vector3(0, UnityEngine.Random.Range(0, 360), 0);
        SetLookDir(rndVec);
    }
}
