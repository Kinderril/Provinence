using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public enum SpecialAbility
{
    none = 0,
    penetrating,
    AOE,
    Critical,
    homing,
    push,
    slow,
    removeDefence,
    vampire,
    chain,
    clear,
}

public enum Slot
{
    physical_weapon,
    magic_weapon,
    body,
    helm,
    bonus,
    Talisman
}

public class PlayerItem : BaseItem
{
    public Dictionary<ParamType, float> parameters;
    public SpecialAbility specialAbilities = SpecialAbility.none; 
    public bool isRare;
    public const char FIRSTCHAR = '%';


    public PlayerItem(Dictionary<ParamType, float> pparams, Slot slot, bool isRare, float totalPoints)
    {
        this.cost = PointsToCost(totalPoints,isRare);
        this.parameters = pparams;
        this.Slot = slot;
        this.isRare = isRare;
        isEquped = false;
    }
    public PlayerItem(Dictionary<ParamType, float> pparams, Slot slot, bool isRare, int cost,bool isEquiped,string name,string icon)
    {
        this.cost = cost;
        this.parameters = pparams;
        this.Slot = slot;
        this.name = name;
        this.isRare = isRare;
        this.isEquped = isEquiped;
        this.icon = icon;
        string p = "";
        p += "Slot:" + Slot;
        foreach (var pparam in pparams)
        {
            p += pparam.Key + "{" + pparam.Value + "}";
        }
        Debug.Log("Weapon loaded :  " + p);
    }

    private int PointsToCost(float points, bool isRare)
    {
        return (int)( points*5*(isRare ? 1.4f : 1) );
    }
    
    public override string Save()
    {
        StringBuilder par = new StringBuilder();
        foreach (var parameter in parameters)
        {
            par.Append((int)parameter.Key);
            par.Append(DPAR);
            par.Append(parameter.Value);
            par.Append(DELEM);
        }
        StringBuilder ss = new StringBuilder();
        ss.Append((int)Slot);
        ss.Append(DELEM);
        ss.Append(isRare.ToString());
        ss.Append(DELEM);
        ss.Append(icon.ToString());
        ss.Append(DELEM);
        ss.Append(name.ToString());
        ss.Append(DELEM);
        ss.Append(cost.ToString());
        ss.Append(DELEM);
        ss.Append(isEquped.ToString());
        ss.Append(DELEM);
        StringBuilder specials = new StringBuilder();
        specials.Append((int)specialAbilities);
        var result = par.ToString() + MDEL + ss.ToString() + MDEL + specials.ToString();
        Debug.Log("ITEM SAVE STRING :" + result);
        return result;
    }

    public override char FirstChar()
    {
        return FIRSTCHAR;
    }

    public override void Activate(Hero hero)
    {
        foreach (var parameter in parameters)
        {
            hero.Parameters.Parameters[parameter.Key] += parameter.Value;
        }
    }

    public static PlayerItem Creat(string item)
    {
        Debug.Log("Creat from:   " + item);
        var Part1 = item.Split(MDEL);
        var Part2 = Part1[1].Split(DELEM);
        var Part3 = Part1[2].Split(DELEM);
        Slot slot = (Slot) Convert.ToInt32(Part2[0]);
        bool isRare = Convert.ToBoolean(Part2[1]);
        string icon = Part2[2];
        string name = Part2[3];
        int cost = Convert.ToInt32(Part2[4]);
        bool isEquped = Convert.ToBoolean(Part2[5]);

        var firstPart = Part1[0].Split(DELEM);
        Dictionary<ParamType, float> itemParameters = new Dictionary<ParamType, float>();
        Debug.Log(">>>mainPart[1]   " + Part1[0]);
        foreach (var s in firstPart)
        {
            Debug.Log(">>>>>   " + s);
            if (s.Length < 3)
                break;
            var pp = s.Split(DPAR);
            ParamType type = (ParamType)Convert.ToInt32(pp[0]);
            float value = Convert.ToSingle(pp[1]);
            itemParameters.Add(type,value);
        }
        PlayerItem playerItem = new PlayerItem(itemParameters,slot,isRare,cost,isEquped,name,icon);
        var spec = (SpecialAbility)Convert.ToInt32(Part3);
        playerItem.specialAbilities = spec;
        return playerItem;
    }
}

