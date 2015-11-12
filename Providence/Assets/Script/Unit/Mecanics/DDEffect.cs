using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class DDEffect : TimeEffect
{
    public DDEffect(Unit targetUnit) 
        : base(targetUnit)
    {
        targetUnit.Parameters.Parameters[ParamType.MPower] *= 2f;
        targetUnit.Parameters.Parameters[ParamType.PPower] *= 2f;
        effect = DataBaseController.Instance.GetEffect(EffectType.doubleDamage, targetUnit.transform);
        effect.Action();
    }

    protected override void OnTimer()
    {
        effect.End();
        targetUnit.Parameters.Parameters[ParamType.MPower] /= 2f;
        targetUnit.Parameters.Parameters[ParamType.PPower] /= 2f;
        base.OnTimer();
    }
}

