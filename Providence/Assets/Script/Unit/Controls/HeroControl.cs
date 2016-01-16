using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using UnityEngine;
using UnityStandardAssets.Effects;

public class HeroControl : BaseControl
{
    private const float CPNST_BACK_WALK = 0.62f;
    private const float CONST_SEC_WALK = 1.4f;
    private const float CONST_SEC_LOOK = 1.4f;
    private float RemainBackWalkTimeSec = 0;
    private float TimeToGoToDefaultLook;
    private bool useLookDir = false;
    private Vector3 lookDir;
    private Vector3 lastMoveDir;
    public bool isBackDir;
    public QueaternionFromTo SpinTransform;
    public GameObject DebuGameObject;
    
    public void Init(Action comeToRotation)
    {
        SpinTransform.Init(OnLookEnd, comeToRotation);
    }

    private void OnLookEnd()
    {
//        Debug.Log("OnLookEnd");
//        SpinTransform.SetLookDir(targetDirection);
//        Animator.SetBool(ANIM_ATTACK, false);
    }

    public override bool MoveTo(Vector3 v)
    {
        if (v != Vector3.zero)
        {
            var ang = Quaternion.Angle(Quaternion.LookRotation(v), SpinTransform.qTo);
            if (IsMoving() && SpinTransform.IsWaiting)
            {
                if (ang > 110)
                {
                    SetBackDir(true);
                }
                if (isBackDir)
                {
                    v = v * CPNST_BACK_WALK;
                    SetToDirection(-v);
                }
                else
                {
                    SetToDirection(v);
                }
            }
            else  if (isBackDir)
            {
                if (ang < 110)
                {
                    SetBackDir(false,"diffff a");
                }
//                Debug.Log("dir back1 " + (-v));
                v = v * CPNST_BACK_WALK;
                SetToDirection(-v);
            }
            else
            {
                SetToDirection(v);
            }
        }
        m_Rigidbody.velocity = v;
        UpdateAnimator(v);
        return true;
    }

    public override void SetToDirection(Vector3 dir)
    {
        if (lastMoveDir != dir)
        {
            lastMoveDir = dir;
            base.SetToDirection(dir);
        }
    }

    private void SetBackDir(bool value,string cause = "")
    {
        //CHeck on look. maybe we wait here
//        Debug.Log("SetBackDir " + value);
//        if (value == isBackDir)
//
//        if (SpinTransform.IsWaiting)
//        {
//            
//        }

        if (value)
        {
            RemainBackWalkTimeSec = CONST_SEC_WALK;
        }
        if (IsMoving())
        {
            isBackDir = value;
        }
        else
        {
            isBackDir = false;
        }
//        Debug.Log("Set backl dir " + value + "   " + Time.time + "   cause:" + cause);
    }

    private bool IsMoving()
    {
        return m_Rigidbody.velocity.sqrMagnitude > 0.1f;
    }
    
    private bool SetLookDir(Vector3 dir)
    {
//        Debug.Log("Look with spin -----  >>>>");
        var doRotate = SpinTransform.SetLookDir(dir);
//        Debug.Log("Look with spin ----- <<<<<");
//        lookDir = dir;
        if (m_Rigidbody.velocity.sqrMagnitude < 0.1f)
        {
            SetToDirection(dir);
        }
        return doRotate;
    }

    protected override void UpdateCharacter()
    {
        base.UpdateCharacter();
//        SpinTransform.UpdateRotate();
        CheckRemainBackDir();
        if (DebuGameObject != null)
        {
            DebuGameObject.SetActive(isBackDir);
        }
    }

    private void CheckRemainBackDir()
    {
        if (RemainBackWalkTimeSec > 0)
        {
            RemainBackWalkTimeSec -= Time.deltaTime;
            if (RemainBackWalkTimeSec < 0)
            {
//                if (m_Rigidbody.velocity.sqrMagnitude < 0.1f)
//                {
                    SetBackDir(false);
//                }
//                isBackDir = false;
            }
        }
    }

//    public bool IsNearDirection(Vector3 nDir)
//    {
//        return SetLookDir(nDir);
//    }
//    
    public void SetDir(Vector3 nDir,bool withLook)
    {
        var angel = Vector3.Angle(nDir, TargetDirection);
        if (angel > 115)
        {
            SetBackDir(true);
        }
        else
        {
            if (!isBackDir)
            {
                SetBackDir(false);
            }
            else
            {
                SetBackDir(true);
            }
        }
        SetLookDir(nDir);
    }
}

