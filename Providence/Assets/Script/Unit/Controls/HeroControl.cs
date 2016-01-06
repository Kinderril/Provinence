﻿using System;
using System.Collections.Generic;
using System.Linq;
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
    public bool isBackDir;
    public RotateByQuaterhnion SpinTransform;

    public void Init(Action comeToRotation)
    {
        SpinTransform.Init(OnLookEnd, comeToRotation);
    }

    private void OnLookEnd()
    {

        Animator.SetBool(ANIM_ATTACK, false);
    }

    public override void PlayAttack()
    {
        Animator.SetBool(ANIM_ATTACK,true);
    }

    public override bool MoveTo(Vector3 v)
    {
        if (v != Vector3.zero)
            TargetDirection = v;
        if (isBackDir)
        {
            v = v * CPNST_BACK_WALK;
        }

        m_Rigidbody.velocity = v;
        UpdateAnimator(v);
        return true;
    }

    private void SetBackDir(bool value)
    {
        if (value)
        {
            RemainBackWalkTimeSec = CONST_SEC_WALK;
        }
        if (m_Rigidbody.velocity.sqrMagnitude > 0.1f)
        {
            isBackDir = value;
        }
        else
        {
            if (value)
            {
                TargetDirection = new Vector3(-TargetDirection.x, 0, -TargetDirection.z);
            }
        }
    }


    /*
    void LateUpdate()
    {
        if (useLookDir)
        {
            RotateToTarget(SpinTransform,lookDir);


            if (Time.time > TimeToGoToDefaultLook)
            {
                useLookDir = false;
            }
        }
    }
    */
    public bool SetLookDir(Vector3 dir)
    {
        
        if (m_Rigidbody.velocity.sqrMagnitude < 0.1f)
        {

            TargetDirection = dir;
        }
        return SpinTransform.SetLookDir(dir);
        //lookDir = new Vector3(dir.x,SpinTransform.position.y ,dir.z);
        //TimeToGoToDefaultLook = CONST_SEC_LOOK + Time.time;
        //useLookDir = true;
    }

    protected override void UpdateCharacter()
    {
        base.UpdateCharacter();
        CheckRemainBackDir();
        RotateToTarget(transform,isBackDir ? -TargetDirection : TargetDirection);
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
                    if (isBackDir)
                    {
                        TargetDirection -= TargetDirection;
//                        m_Rigidbody.velocity = TargetDirection;
                    }
                    SetBackDir(false);
                }
                isBackDir = false;
            }
        }
    }
    
    public void SetDir(Vector3 nDir)
    {
        var angel = Vector3.Angle(nDir, TargetDirection);
        if (angel > 90)
        {
            SetBackDir(true);
        }
        else
        {
            SetBackDir(false);
        }
        
    }
}

