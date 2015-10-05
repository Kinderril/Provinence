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
        rangeAttack = owner.curWeapon.Parameters.range;
    }


    protected void MoveToTarget(Vector3 trg)
    {
        trg = new Vector3(trg.x,owner.transform.position.y, trg.z);
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

            }
        }
        curRange = ((BaseMonster)owner).mainHeroDist;
    }

    protected void DoShoot()
    {
        owner.TryAttack(target.transform.position);
        owner.OnShootEnd += OnShootEnd;
    }

    protected virtual void OnShootEnd(Unit obj)
    {
        owner.OnShootEnd -= OnShootEnd;
    }

    public override void End(string msg = " end action ")
    {
        owner.OnShootEnd -= OnShootEnd;
        base.End(msg);
    }
}

