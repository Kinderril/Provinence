using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    private Unit target;
    private float rangeAttack;
    private float curRange;
    private Action attackAction;
    private AttackStatus status;
    private float lastHitTime;

    public AttackAction(BaseMonster owner, Unit target ,Action endCallback)
        : base(owner, endCallback)
    {
        this.target = target;
        rangeAttack = owner.curWeapon.range;
        switch (owner.AttackType)
        {
            case AttackType.hitAndrun:
                break;
            case AttackType.distanceFight:
                break;
            case AttackType.closeCombat:
                break;
        }
    }

    public override void Update()
    {
        curRange =( owner.transform.position - target.transform.position ).magnitude;
        bool isInRange = (curRange < rangeAttack); 
            //owner.curWeapon.CanShoot();

    }
}

