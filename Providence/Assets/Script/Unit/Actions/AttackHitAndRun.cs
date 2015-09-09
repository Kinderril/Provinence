using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class AttackHitAndRun : AttackAction
{
    private AttackStatus status;
    private Vector3 backPosition;

    public AttackHitAndRun(BaseMonster owner, Unit target, Action endCallback) : base(owner, target, endCallback)
    {
    }

    public void UpdateHitAndRun()
    {
        if (status == AttackStatus.comeIn)
        {
            curRange = (owner.transform.position - target.transform.position).magnitude;
            isInRange = (curRange < rangeAttack);
            if (isInRange)
            {
                DoShoot();
                status = AttackStatus.comeOut;
                backPosition = ((BaseMonster)owner).bornPosition;
            }
            else
            {
                MoveToTarget();
            }
        }
        else
        {
            curRange = (owner.transform.position - backPosition).magnitude;
            isInRange = (curRange < rangeAttack);
            if (isInRange)
            {
                status = AttackStatus.comeIn;
            }
            else
            {
                MoveToTarget();
            }

        }
    }
}

