using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum EffectType
{
    doubleDamage,
    slow,
}

public class TimeEffect
{
    public float totalTime;
    private Unit targetUnit;
    private TimerManager.ITimer timer;
    private EffectType EffectType;

    public void Start(Unit targetUnit, EffectType EffectType)
    {
        this.targetUnit = targetUnit;
        timer = MainController.Instance.TimerManager.MakeTimer(TimeSpan.FromSeconds(totalTime));
        timer.OnTimer += OnTimer;
        MainController.Instance.level.OnEndLevel += OnEndLevel;
        targetUnit.OnDead += OnTargetDead;
        switch (EffectType)
        {
            case EffectType.doubleDamage:
                targetUnit.Parameters.Parameters[ParamType.PPower] *= 2f;
                targetUnit.Parameters.Parameters[ParamType.MPower] *= 2f;
                break;
            case EffectType.slow:
                targetUnit.Parameters.Parameters[ParamType.Speed] /= 3f;
                break;
        }
    }

    private void OnTargetDead(Unit obj)
    {
        OnEndLevel();
    }

    private void OnTimer()
    {
        switch (EffectType)
        {
            case EffectType.doubleDamage:
                targetUnit.Parameters.Parameters[ParamType.PPower] /= 2f;
                targetUnit.Parameters.Parameters[ParamType.MPower] /= 2f;
                break;
            case EffectType.slow:

                targetUnit.Parameters.Parameters[ParamType.Speed] *= 3f;
                break;
        }
        OnEndLevel();
    }

    private void OnEndLevel()
    {
        MainController.Instance.level.OnEndLevel -= OnEndLevel;
        timer.Stop();
    }
}

