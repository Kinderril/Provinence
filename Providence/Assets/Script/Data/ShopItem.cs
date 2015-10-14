using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public class ShopItem
{
    public int CrystalCost;
    public int MoneyCost;
    public int Parameter;
    public Sprite icon;
    public IShopExecute execute;
}

