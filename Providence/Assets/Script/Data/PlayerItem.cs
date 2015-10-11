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
    public bool isRare;
    public string icon;
    public string name;
    public PlayerItem(string item)
    {
        Load(item);
    }

    public PlayerItem(Dictionary<ParamType, float> pparams, Slot slot, bool isRare)
    {
        this.parameters = pparams;
        this.Slot = slot;
        this.isRare = isRare;
        isEquped = false;
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

