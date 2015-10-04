using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class AttackDistance : AttackAction
{
    private float closeRange;
    private float farRange;

    public AttackDistance(BaseMonster owner, Unit target, Action endCallback) 
        : base(owner, target, endCallback)
    {
        closeRange = owner.curWeapon.Parameters.range * 0.75f;
        farRange = owner.curWeapon.Parameters.range * 1.25f;
    }

    public override void Update()
    {
        base.Update();
        UpdateDistance();
    }

    public void UpdateDistance()
    {
        base.Update();
        bool isFar = curRange < farRange;
        //isInRange = isClose && isFar;
        //Debug.Log("In Range: " + isClose + "  " + isFar + "  " + curRange + "  " + closeRange + "   " + farRange);
        if (!isFar)
        {
            bool isClose = curRange < closeRange;
            if (owner.curWeapon.CanShoot())
            {
                DoShoot();
            }
            if (isClose)
            {
                ComeOutTarget();
            }
        }
        else
        {
            ComeToTarget();
        }

    }

    private void ComeOutTarget()
    {
        Vector3 t = Vector3.back;
        MoveToTarget(t);

    }

    private void ComeToTarget()
    {
        MoveToTarget(target.transform.position);
    }

    private void Walk()
    {

    }
}

