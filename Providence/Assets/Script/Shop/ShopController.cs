using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class ShopController : Singleton<ShopController>
{
    public void BuyItem(ShopItem shopItem)
    {
        shopItem.execute.Execute(shopItem.Parameter);
    }
}
