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
    private bool isEquped;
    public bool isRare;
    public string icon;
    public string name;
    public int cost;
    public bool IsEquped
    {
        get { return isEquped; }
        set
        {
            isEquped = value;
            Save();
        }
    }
    

    public PlayerItem(string item)
    {
        Load(item);
    }

    public PlayerItem(Dictionary<ParamType, float> pparams, Slot slot, bool isRare, float totalPoints)
    {
        this.cost = PointsToCost(totalPoints,isRare);
        this.parameters = pparams;
        this.Slot = slot;
        this.isRare = isRare;
        isEquped = false;
    }

    private int PointsToCost(float points, bool isRare)
    {
        return (int)( points*5*(isRare ? 1.4f : 1) );
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

