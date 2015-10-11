using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class WindowShop : BaseWindow
{

    public Transform layout;
    public PlayerItemElement Prefab;

    public void OnBuySimpleChest()
    {
        ShopController.Instance.BuyItem(DataBaseController.Instance.stubPrefab);
    }
    public override void Init()
    {
        base.Init();
        MainController.Instance.PlayerData.OnNewItem += OnNewItem;
        List<PlayerItem> items = MainController.Instance.PlayerData.GetAllItems();
        foreach (var playerItem in items)
        {
            var element = DataBaseController.Instance.GetItem<PlayerItemElement>(Prefab);
            element.Init(playerItem);
            element.transform.SetParent(layout);
        }
    }

    private void OnNewItem(PlayerItem playerItem)
    {
        var element = DataBaseController.Instance.GetItem<PlayerItemElement>(Prefab);
        element.Init(playerItem);
        element.transform.SetParent(layout);
    }

    public override void Close()
    {
        MainController.Instance.PlayerData.OnNewItem -= OnNewItem;
        foreach (Transform child in layout.transform)
        {
            Destroy(child.gameObject);
        }
        base.Close();
    }
}

