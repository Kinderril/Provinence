using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class ShopController : Singleton<ShopController>
{
    private List<Slot> allSlots = new List<Slot>(); 
    public void Init()
    {
        foreach (Slot slot in Enum.GetValues(typeof(Slot)))
        {
            allSlots.Add(slot);
        }
    }

    public Slot RandomSlot()
    {
        return allSlots.RandomElement();
    }

    public void BuyItem(ShopItem shopItem)
    {
        shopItem.execute.Execute(shopItem.Parameter);
    }
}
