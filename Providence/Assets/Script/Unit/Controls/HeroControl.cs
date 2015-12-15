using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityStandardAssets.Effects;

public class HeroControl : BaseControl
{
    private const float CONST_SEC = 1.4f;
    private float RemainBackWalkTimeSec = 0;
    public bool isBackDir;
    public Transform SpinTransform;


    public override bool MoveTo(Vector3 v)
    {
        if (v != Vector3.zero)
            TargetDirection = v;
        m_Rigidbody.velocity = v;
        UpdateAnimator(v);
        return true;
    }

    private void SetBackDir(bool value)
    {
        if (value)
        {
                RemainBackWalkTimeSec = CONST_SEC;
            //TargetDirection = -TargetDirection;
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

    void LateUpdate()
    {
        SpinTransform.transform.localRotation = Quaternion.Euler(new Vector3(0,90,0));
       // SpinTransform.Rotate(Vector3.right,1f);
    }

    protected override void UpdateCharacter()
    {
        base.UpdateCharacter();
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
                        m_Rigidbody.velocity = TargetDirection;
                    }
                    SetBackDir(false);
                }
            }
        }
        RotateToTarget(isBackDir ? -TargetDirection : TargetDirection);
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

