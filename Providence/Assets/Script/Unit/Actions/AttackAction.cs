using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public enum AttackType
{
    hitAndRun,
    distanceFight,
    closeCombat
}

public enum AttackStatus
{
    comeIn,
    comeOut
}

public class AttackAction : BaseAction
{
    protected Unit target;
    protected float rangeAttack;
    protected float curRange;
    protected float lastHitTime;
    protected bool isInRange;
    private bool isMoving = false;
    private Vector3 moveTarget;

    public AttackAction(BaseMonster owner, Unit target ,Action endCallback)
        : base(owner, endCallback)
    {
        this.target = target;
        rangeAttack = owner.curWeapon.range;
    }


    protected void MoveToTarget(Vector3 trg)
    {

        if (((moveTarget-trg).sqrMagnitude > 1 || !isMoving) && owner.Control.Move(trg, false, false))
        {
            moveTarget = trg;
            isMoving = true;
        }
    }


    public override void Update()
    {
        if (isMoving)
        {
            isMoving = !owner.Control.IsPathComplete();
            if (!isMoving)
            {
                Debug.Log("move END");
            }
        }
        curRange = (owner.transform.position - target.transform.position).magnitude;
    }

    protected void DoShoot()
    {
        owner.TryAttack(target.transform.position);
    }
}

