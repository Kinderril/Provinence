﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class HeroShopRandomItem : IShopExecute
{
    public override void Execute(int level)
    {
        var slot = ShopController.RandomSlot();
        var levelResult = ShopController.RandomizeLvl(level);
        Switcher(slot, levelResult);
    }

    private void Switcher(Slot slot, int levelResult)
    {
        var totalPoints = GetPointsByLvl(levelResult)*GetSlotCoef(slot);
        float diff = Utils.RandomNormal(0.5f, 1f);
        //Debug.Log("iffff " + diff);
        bool isRare = diff > 0.95f;
        totalPoints *= diff;
        float contest = UnityEngine.Random.Range(0.65f, 1f);
        if (contest > 0.9f)
            contest = 1f;
        var pparams = new Dictionary<ParamType,float>();
        var primary = Connections.GetPrimaryParamType(slot);
        var primaryValue = totalPoints*contest;
        pparams.Add(primary,primaryValue);
        if (contest < 0.95f)
        {
            var secondary = Connections.GetSecondaryParamType(slot);
            var secondaryValue = totalPoints*(1f - contest);
            pparams.Add(secondary, secondaryValue);
        }
        PlayerItem item = new PlayerItem(pparams,slot,isRare, totalPoints);
        MainController.Instance.PlayerData.AddItem(item);
    }

    private int GetPointsByLvl(int lvl)
    {
        return lvl*4 + 20;
    }

    private float GetSlotCoef(Slot slot)
    {
        float val = 1f;
        switch (slot)
        {
            case Slot.physical_weapon:
                val = 1.1f;
                break;
            case Slot.magic_weapon:
                val = 1.3f;
                break;
            case Slot.body:
                val = 1f;
                break;
            case Slot.helm:
                val = 0.8f;
                break;
        }
        return val;
    }
}

