using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class QueaternionFromTo : MonoBehaviour
{
//    public Vector3 to;
//    public Vector3 from;
    private Quaternion qTo;
    private Quaternion qFrom;
    private Quaternion qCur;
    private float time;
    public float totalSpeed;
    private float curSpeed;
    private float ang;
    private float remainAngel;
    private bool isRotating;
    public QueaternionFromTo BlockingFromTo;
    public float waitTimeSec;
    private Action endLookRotation;
    private Action comeToRotation;
    private bool isWaiting;
    private float endWAitTime;

    public bool IsRotating
    {
        get { return isRotating; }
    }

    public float RemainAngel
    {
        get { return remainAngel; }
    }

    private void Start()
    {

    }

    public bool SetLookDir(Vector3 dir)
    {
        qFrom = transform.rotation;
        qTo = Quaternion.LookRotation(dir);
        ang = Quaternion.Angle(qFrom, qTo);
        time = 0;
        if (ang > 2)
        {
            curSpeed = totalSpeed /ang;
            
            isRotating = true;
            isWaiting = false;
        }
        return IsRotating;
    }

    private void LateUpdate()
    {
        if (isRotating)
        {
            time += Time.deltaTime * curSpeed;
            if (NoBlocking())
            {
                if (time >= 1f)
                {
                    time = 1f;
//                    Debug.Log("ENd tor " + curSpeed);
                    comeToRotation();
                    isRotating = false;
                    StartWait();
                }
                remainAngel = (1f - time)*ang;
                qCur = Quaternion.Lerp(qFrom, qTo, time);
                transform.rotation = qCur;
            }
        }
        else if (isWaiting)
        {
            if (NoBlocking())
            {
                transform.rotation = qCur;
                if (Time.time > endWAitTime)
                {
                    isWaiting = false;
                    if (endLookRotation != null)
                    {
                        endLookRotation();
                    }
                }
            }
            else
            {
                isWaiting = false;
            }
        }
    }

    private bool NoBlocking()
    {
        return !(BlockingFromTo != null && BlockingFromTo.IsRotating && BlockingFromTo.RemainAngel > 45);
    }

    private void StartWait()
    {
        if (waitTimeSec > 0)
        {
            isWaiting = true;
            endWAitTime = waitTimeSec + Time.time;
        }
    }

    public void Init(Action onLookEnd, Action onComeRotation)
    {
        this.comeToRotation = onComeRotation;
        this.endLookRotation = onLookEnd;
    }

    public bool ShallRotate(Vector3 dir)
    {
        qFrom = transform.rotation;
        qTo = Quaternion.LookRotation(dir);
        ang = Quaternion.Angle(qFrom, qTo);
        return ang < 4;
    }

}