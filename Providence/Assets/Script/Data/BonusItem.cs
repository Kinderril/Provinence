using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum Bonustype
{
    damage,
    money
}

public class BonusItem : PlayerItem
{
    private float val;
    private Bonustype bonustype;
    public BonusItem(string item)
        : base(item)
    {

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
}

