﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;


public class WindowMission : BaseWindow
{
    public Text moneyField;
    public Text crystalField;
    public override void Init()
    {
        base.Init();
        var items = MainController.Instance.PlayerData.playerInv;
        foreach (var item in items)
        {
            Text t = null;
            switch (item.Key)
            {
                case ItemId.money:
                    t = moneyField;
                    break;
                case ItemId.crystal:
                    t = crystalField;
                    break;
            }
            if (t != null)
                t.text = "+" + item.Value;
        }
    }

    public void OnPlayClick()
    {
        WindowManager.Instance.OpenWindow(MainState.play);
    }

    public void OnShopClick()
    {
        WindowManager.Instance.OpenWindow(MainState.shop);
    }
}

