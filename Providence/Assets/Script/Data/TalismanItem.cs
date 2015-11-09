using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public enum TalismanType
{
    firewave,
    massPush,
    massFreez,
    heal,
    doubleDamage,
    speed,
    megaArmor
}

public class TalismanItem : BaseItem
{
    public float power;
    public TalismanType TalismanType;
    public float costShoot;
    public const char FIRSTCHAR = '=';


    public TalismanItem(int totalPoints, TalismanType type)
    {
        this.power = totalPoints;
        this.TalismanType = type;
        costShoot = power*1.5f;
    }

    public override char FirstChar()
    {
        return FIRSTCHAR;
    }

    public override void Activate(Hero hero)
    {
        
    }

    public override string Save()
    {
        return "";
    }

    public static BaseItem Creat(string subStr)
    {
        return null;
    }
}

