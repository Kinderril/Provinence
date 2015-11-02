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
    private float val;
    private Bonustype bonustype;
    public const char FIRSTCHAR = '+';
    public override char FirstChar()
    {
        return FIRSTCHAR;
    }

    public override void Activate(Hero hero)
    {

        switch (bonustype)
        {
            case Bonustype.damage:
                hero.damageBonusFromItem = val;
                break;
            case Bonustype.money:
                hero.moneyBonusFromItem = val;
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

