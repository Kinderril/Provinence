using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public enum VisualEffectPosition
{
    core,
    weapon
}

public class VisualEffect : MonoBehaviour
{
    public ParticleSystem pssystem;
    public VisualEffectPosition VisualEffectPosition;

    public void Action(Unit unit)
    {
        switch (VisualEffectPosition)
        {
            case VisualEffectPosition.core:
                transform.SetParent(unit.transform,false);
                break;
            case VisualEffectPosition.weapon:
                transform.SetParent(unit.weaponsContainer, false);
                break;
        }
        pssystem.enableEmission = true;
    }

    public void End()
    {
        Destroy(gameObject);
    }
}

