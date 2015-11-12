using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class HeroShopBonusItem : IShopExecute
{
    public override void Execute(int id)
    {
        var bonus = ShopController.RandomBonus();
        BonusItem b = new BonusItem(bonus,id*2,3);
        MainController.Instance.PlayerData.AddItem(b);
        base.Execute(id);
    }
}

