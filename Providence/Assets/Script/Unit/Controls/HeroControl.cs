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
    
    public void Init(Action comeToRotation)
    {
        SpinTransform.Init(OnLookEnd, comeToRotation);
    }

    private void OnLookEnd()
    {
        Debug.Log("OnLookEnd");
        SpinTransform.SetLookDir(targetDirection);
//        Animator.SetBool(ANIM_ATTACK, false);
    }

//    public override void PlayAttack()
//    {
//        Animator.SetBool(ANIM_ATTACK,true);
//    }

    public override bool MoveTo(Vector3 v)
    {
        if (v != Vector3.zero)
        {
            if (isBackDir)
            {
                var ang = Vector3.Angle(v, lookDir);
                if (ang < 90)
                {
                    SetBackDir(false);
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
//            Debug.Log("SetToDirection " + "  SpinTransform.IsRotating: " + SpinTransform.IsRotating + "   dir:" + dir + "   SpinTransform.Side:" + SpinTransform.Side);
            if (SpinTransform.IsRotating)
            {
                base.SetToDirection(dir);
            }
            else
            {

                base.SetToDirection(dir);
            }
        }
    }

    private void SetBackDir(bool value)
    {
//        Debug.Log("SetBackDir " + value);
        if (value)
        {
            RemainBackWalkTimeSec = CONST_SEC_WALK;
        }
        if (m_Rigidbody.velocity.sqrMagnitude > 0.1f)
        {
            isBackDir = value;
//            if (isBackDir != value)
//            {

//                SetToDirection(targetDirection);
//                float ang;
//                var side = RotateByQuaterhnion.CalcSide(targetDirection, lookDir, out ang);
//                isBackDir = value;
//                if (ang > 2)
//                {
//                    SetToDirection(targetDirection, side);
//                }

//            }

        }
        else
        {
            isBackDir = false;
        }
    }


    private bool SetLookDir(Vector3 dir)
    {
        Debug.Log("Look with spin");
        var doRotate = SpinTransform.SetLookDir(dir);
        lookDir = dir;
        if (m_Rigidbody.velocity.sqrMagnitude < 0.1f)
        {
            SetToDirection(dir);
        }
        return doRotate;
        //lookDir = new Vector3(dir.x,SpinTransform.position.y ,dir.z);
        //TimeToGoToDefaultLook = CONST_SEC_LOOK + Time.time;
        //useLookDir = true;
    }

    protected override void UpdateCharacter()
    {
        base.UpdateCharacter();
//        SpinTransform.UpdateRotate();
        CheckRemainBackDir();
        RotateToTarget();
    }

    private void CheckRemainBackDir()
    {
        if (RemainBackWalkTimeSec > 0)
        {
            RemainBackWalkTimeSec -= Time.deltaTime;
            if (RemainBackWalkTimeSec < 0)
            {
                if (m_Rigidbody.velocity.sqrMagnitude < 0.1f)
                {
                    SetBackDir(false);
                }
                isBackDir = false;
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
        if (angel > 90)
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

