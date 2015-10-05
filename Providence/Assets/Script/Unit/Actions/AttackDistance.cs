using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class AttackDistance : AttackAction
{
    private float closeRange;
    private float farRange;
    private bool isAttack = false;

    public AttackDistance(BaseMonster owner, Unit target, Action endCallback) 
        : base(owner, target, endCallback)
    {
        closeRange = owner.curWeapon.Parameters.range * 0.5f;
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
        if (!isAttack)
        {
            bool isFar = curRange < farRange;
            //Debug.Log("In Range: " + isClose + "  " + isFar + "  " + curRange + "  " + closeRange + "   " + farRange);
            if (!isFar)
            {
                bool isClose = curRange < closeRange;
                if (owner.curWeapon.CanShoot())
                {
                    Stop();
                    isAttack = true;
                    DoShoot();
                    return;
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
    }

    protected override void OnShootEnd(Unit obj)
    {
        base.OnShootEnd(obj);
        isAttack = false;
    }

    private void Stop()
    {
        owner.Control.Stop();
    }

    private void ComeOutTarget()
    {
        Vector3 dirToEscape = owner.transform.position - target.transform.position;
        MoveToTarget(dirToEscape.normalized * closeRange + owner.transform.position);
        
    }

    private void ComeToTarget()
    {
        Vector3 dirToTrg = target.transform.position - owner.transform.position ;
        //no so close
        MoveToTarget(owner.transform.position + dirToTrg.normalized * owner.curWeapon.Parameters.range);
    }
}

