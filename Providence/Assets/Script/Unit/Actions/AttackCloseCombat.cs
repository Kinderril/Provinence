using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class AttackCloseCombat :AttackAction
{
    public AttackCloseCombat(BaseMonster owner, Unit target, Action endCallback) 
        : base(owner, target, endCallback)
    {
    }

    public void UpdateCloseCombat()
    {
        curRange = (owner.transform.position - target.transform.position).magnitude;
        isInRange = (curRange < rangeAttack);
        if (isInRange)
        {
            if (owner.curWeapon.CanShoot())
            {
                DoShoot();
            }
        }
        else
        {
            MoveToTarget();
        }

    }
}

