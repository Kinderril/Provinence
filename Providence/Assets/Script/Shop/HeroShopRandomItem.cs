using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class HeroShopRandomItem : IShopExecute
{
    public override void Execute(int level)
    {
        var slot = ShopController.Instance.RandomSlot();
        switch (slot)
        {
            case Slot.physical_weapon:
                break;
            case Slot.magic_weapon:
                break;
            case Slot.body:
                break;
            case Slot.helm:
                break;
            case Slot.bonus:
                Execute(level);
                return;

        }
        //TODO creae random item
    }
}

