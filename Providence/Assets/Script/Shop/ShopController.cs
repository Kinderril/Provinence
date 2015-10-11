﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class ShopController : Singleton<ShopController>
{
    public void Init()
    {
        Connections.Init();
    }
    
    public void BuyItem(ShopItem shopItem)
    {
        shopItem.execute.Execute(shopItem.Parameter);
    }

    public static int RandomizeLvl(int baseLvl)
    {
        var lvls = new WDictionary<int>(new Dictionary<int, float>()
        {
            { baseLvl -1,3f },
            { baseLvl ,4f },
            { baseLvl +1,3f },
            { baseLvl +2,0.5f },
        });
        return lvls.Random();
    }

    public static Slot RandomSlot()
    {
        var slots = new WDictionary<Slot>(new Dictionary<Slot, float>()
        {
            { Slot.body,3f },
            { Slot.helm,3f },
            { Slot.magic_weapon,3f },
            { Slot.physical_weapon,3f },
        });
        return slots.Random();
    }

    
}

public static class Connections
{
    private static readonly Dictionary<Slot, WDictionary<ParamType>> primary = new Dictionary<Slot, WDictionary<ParamType>>();
    private static readonly Dictionary<Slot, WDictionary<ParamType>> secondary = new Dictionary<Slot, WDictionary<ParamType>>();
    public static void Init()
    {
        primary.Add(Slot.physical_weapon, new WDictionary<ParamType>(new Dictionary<ParamType, float>() { { ParamType.PPower, 1 } }));
        primary.Add(Slot.magic_weapon, new WDictionary<ParamType>(new Dictionary<ParamType, float>() { { ParamType.MPower, 3 } }));
        primary.Add(Slot.body, new WDictionary<ParamType>(new Dictionary<ParamType, float>() { { ParamType.PDef, 3 }, { ParamType.MDef, 1 } }));
        primary.Add(Slot.helm, new WDictionary<ParamType>(new Dictionary<ParamType, float>() { { ParamType.PDef, 1 }, { ParamType.MDef, 3 } }));

        secondary.Add(Slot.physical_weapon, new WDictionary<ParamType>(new Dictionary<ParamType, float>() { { ParamType.PDef, 1 } }));
        secondary.Add(Slot.magic_weapon, new WDictionary<ParamType>(new Dictionary<ParamType, float>() { { ParamType.MDef, 3 } }));
        secondary.Add(Slot.body, new WDictionary<ParamType>(new Dictionary<ParamType, float>() { { ParamType.Hp, 1 }, { ParamType.Speed, 1 } }));
        secondary.Add(Slot.helm, new WDictionary<ParamType>(new Dictionary<ParamType, float>() { { ParamType.Hp, 1 }, { ParamType.PPower, 3 }, { ParamType.MPower, 3 } }));
    }

    public static ParamType GetPrimaryParamType(Slot slot)
    {
        return primary[slot].Random();
    }
    public static ParamType GetSecondaryParamType(Slot slot)
    {
        return secondary[slot].Random();
    }
}

public class WDictionary<T> : Dictionary<float, T>
{
    private float total = 0;
    public WDictionary(Dictionary<T,float> items )
    {
        float curW = 0;
        foreach (var item in items)
        {
            curW += item.Value;
            Add(curW,item.Key);
        }
        total = curW;
    }

    public T Random()
    {
        var res = UnityEngine.Random.Range(0, total);
        foreach (var key in Keys)
        {
            if (key >= res)
            {
                return this[key];
            }
        }
        return default(T);
    }
}

