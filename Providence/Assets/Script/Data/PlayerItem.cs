using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public enum Slot
{
    physical_weapon,
    magic_weapon,
    body,
    helm,
    bonus
}

public class PlayerItem
{
    public Slot Slot;
    public Dictionary<ParamType, float> parameters;
    public bool isEquped;
    public string icon;
    public PlayerItem(string item)
    {
        Load(item);
    }

    public virtual void Load(string item)
    {
        
    }

    public virtual void Save()
    {
        
    }

    public virtual void Activate(Hero hero)
    {
        foreach (var parameter in parameters)
        {
            hero.Parameters.Parameters[parameter.Key] += parameter.Value;
        }
    }
}

