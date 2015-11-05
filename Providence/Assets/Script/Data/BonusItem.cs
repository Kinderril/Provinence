using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum Bonustype
{
    damage,
    money
}

public class BonusItem : BaseItem
{
    public float power;
    public Bonustype Bonustype;
    public const char FIRSTCHAR = '+';
    public override char FirstChar()
    {
        return FIRSTCHAR;
    }

    public override void Activate(Hero hero)
    {

        switch (Bonustype)
        {
            case Bonustype.damage:
                hero.damageBonusFromItem = power;
                break;
            case Bonustype.money:
                hero.moneyBonusFromItem = power;
                break;
        }
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

