using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class UnitParameters : ScriptableObject
{
    public float Speed = 5;
    public float Power = 1;
    public int MaxHp = 15;
    public float magicResist = 1f;
    public float physicResist = 1f;

    public UnitParameters Copy()
    {
        var p = new UnitParameters();
        p.Speed = Speed;
        p.Power = Power;
        p.MaxHp = MaxHp;
        p.magicResist = magicResist;
        p.physicResist = physicResist;
        return p;

    }
}

