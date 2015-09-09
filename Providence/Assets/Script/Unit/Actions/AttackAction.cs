using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public enum AttackType
{
    hitAndrun,
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

    public AttackAction(BaseMonster owner, Unit target ,Action endCallback)
        : base(owner, endCallback)
    {
        this.target = target;
        rangeAttack = owner.curWeapon.range;
    }


    protected void MoveToTarget()
    {
        owner.Control.Move(target.transform.position, false, false);
    }


    public override void Update()
    {
        curRange = (owner.transform.position - target.transform.position).magnitude;
    }

    protected void DoShoot()
    {

    }
}

