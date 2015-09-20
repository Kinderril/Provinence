using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


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

    public void UpdateDistance()
    {
        base.Update();
        isInRange = curRange > closeRange && curRange < farRange;
        if (isInRange)
        {
            if (owner.curWeapon.CanShoot())
            {
                DoShoot();
                Walk();
            }
            else
            {
                Walk();
            } 
        }
        else
        {
            
        }

    }

    private void Walk()
    {

    }
}

