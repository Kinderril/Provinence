﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public enum EffectType
{
    doubleDamage,
    slow,
    speed,
    freez
}

public class TimeEffect
{
    public float totalTime = 10;
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
        Debug.Log("Effect setted " + EffectType);
        switch (EffectType)
        {
            case EffectType.doubleDamage:
                targetUnit.Parameters.Parameters[ParamType.PPower] *= 2f;
                targetUnit.Parameters.Parameters[ParamType.MPower] *= 2f;
                break;
            case EffectType.slow:
                targetUnit.Parameters.Parameters[ParamType.Speed] /= 3f;
                break;
            case EffectType.freez:

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
            case EffectType.freez:

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

