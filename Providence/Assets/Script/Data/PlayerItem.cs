using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public enum SpecialAbility
{
    none,
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

public class PlayerItem
{
    private const char DELEM = '/';
    private const char DPAR = '{';
    private const char MDEL = '>';
    public Slot Slot;
    public Dictionary<ParamType, float> parameters;
    private bool isEquped;
    public bool isRare;
    public string icon = "";
    public string name= "";
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
    
    public virtual string Save()
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
        var result = par.ToString() + MDEL + ss.ToString();
        Debug.Log("ITEM SAVE STRING :" + result);
        return result;
    }

    public virtual void Activate(Hero hero)
    {
        foreach (var parameter in parameters)
        {
            hero.Parameters.Parameters[parameter.Key] += parameter.Value;
        }
    }

    public static PlayerItem Creat(string item)
    {
        Debug.Log("Creat from:   " + item);

        var mainPart = item.Split(MDEL);

        var secondPart = mainPart[1].Split(DELEM);
        Slot slot = (Slot) Convert.ToInt32(secondPart[0]);
        bool isRare = Convert.ToBoolean(secondPart[1]);
        string icon = secondPart[2];
        string name = secondPart[3];
        int cost = Convert.ToInt32(secondPart[4]);
        bool isEquped = Convert.ToBoolean(secondPart[5]);

        var firstPart = mainPart[0].Split(DELEM);
        Dictionary<ParamType, float> itemParameters = new Dictionary<ParamType, float>();
        Debug.Log(">>>mainPart[1]   " + mainPart[0]);
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

        return playerItem;
    }
}

