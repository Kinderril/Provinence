using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class SpeedEffect : TimeEffect
{

    public SpeedEffect(Unit targetUnit) 
        : base(targetUnit)
    {
        targetUnit.Parameters.Parameters[ParamType.Speed] *= 2f;
        effect = DataBaseController.Instance.GetEffect(EffectType.speed, targetUnit.transform);
        effect.Action();
    }

    protected override void OnTimer()
    {
        effect.End();
        targetUnit.Parameters.Parameters[ParamType.Speed] /= 2f;
        base.OnTimer();
    }
}

