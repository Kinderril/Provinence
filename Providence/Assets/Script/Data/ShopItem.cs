using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class ShopItem
{
    public int CrystalCost;
    public int MoneyCost;
    public int Parameter;
    public IShopExecute execute;
}

