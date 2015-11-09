﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class WindowShop : BaseWindow
{

    public Transform layoutMyInventory;
    public Transform layoutShopItems;
    public PlayerItemElement PrefabPlayerItemElement;
    public ShopItemElement PrefabShopItemElement;
    public ItemInfoElement ItemInfoElement;
    private List<PlayerItemElement> PlayerItemElements;
    public Text moneyField;
    public Text crystalField;
    private IShopExecute selectedShopElement;
    private BaseItem selectedPlayerItem;
    public AllParametersContainer AllParametersContainer;
    public Button BuyButton;
    public Button SellButton;
    public Button EquipButton;
    public Button UnEquipButton;

    public override void Init()
    {
        base.Init();
        ItemInfoElement.SetCallBack(OnItemInit);
        AllParametersContainer.Init();
        moneyField.text = MainController.Instance.PlayerData.playerInv[ItemId.money].ToString("0");
        crystalField.text = MainController.Instance.PlayerData.playerInv[ItemId.crystal].ToString("0");
        PlayerItemElements = new List<PlayerItemElement>();
        List<BaseItem> items = MainController.Instance.PlayerData.GetAllItems();
        Debug.Log("items count = " + items.Count);
        foreach (var playerItem in items)
        {
            var element = DataBaseController.Instance.GetItem<PlayerItemElement>(PrefabPlayerItemElement);
            element.Init(playerItem, OnSelected);
            element.transform.SetParent(layoutMyInventory);
            PlayerItemElements.Add(element);
        }
        var allSell = DataBaseController.Instance.allShopElements;
        foreach (var shopExecute in allSell)
        {
            var element = DataBaseController.Instance.GetItem<ShopItemElement>(PrefabShopItemElement);
            element.Init(shopExecute,OnShopSelected);
            element.transform.SetParent(layoutShopItems);
        }
        
        MainController.Instance.PlayerData.OnNewItem += OnNewItem;
        MainController.Instance.PlayerData.OnItemEquiped += OnItemEquipedCallback;
        MainController.Instance.PlayerData.OnItemSold += OnItemSoldCallback;
        MainController.Instance.PlayerData.OnCurrensyChanges += OnCurrensyChanges;
    }

    private void OnItemInit(ItemOwner obj)
    {
        Debug.Log(obj);
        bool val = false;
        switch (obj)
        {
            case ItemOwner.Shop:
                val = true;
                break;
        }
        BuyButton.gameObject.SetActive(val);
        EquipButton.gameObject.SetActive(!val);
        UnEquipButton.gameObject.SetActive(!val);
        SellButton.gameObject.SetActive(!val);
    }

    public void OnBuySimpleChest()
    {
        if (selectedShopElement != null && selectedShopElement.CanBuy && EnoughtMoney(selectedShopElement))
            ShopController.Instance.BuyItem(selectedShopElement);
    }

    public void OnUnequipItem()
    {
        if (selectedPlayerItem != null)
        {
            MainController.Instance.PlayerData.EquipItem(selectedPlayerItem,false);
        }
    }

    private bool EnoughtMoney(IShopExecute selectedShopElement)
    {
        bool haveMoney = MainController.Instance.PlayerData.CanPay(ItemId.money,selectedShopElement.MoneyCost);
        bool haveCrystal = MainController.Instance.PlayerData.CanPay(ItemId.crystal, selectedShopElement.CrystalCost);
        return haveMoney && haveCrystal;
    }
    private void OnShopSelected(IShopExecute obj)
    {
        selectedShopElement = obj;
        ItemInfoElement.Init(selectedShopElement);
    }

    private void OnCurrensyChanges(ItemId arg1, int arg2)
    {
        switch (arg1)
        {
            case ItemId.money:
                moneyField.text = arg2.ToString("0");
                break;
            case ItemId.crystal:
                crystalField.text = arg2.ToString("0");
                break;
        }
    }

    private void OnItemSoldCallback(BaseItem obj)
    {
        Debug.Log("OnItemSold");
        var item = PlayerItemElements.FirstOrDefault(x => x.PlayerItem == obj);
        if (item != null)
        {
            Destroy(item.gameObject);
        }
    }

    public void OnEquip()
    {
        if (selectedPlayerItem == null)
        {
            Debug.Log("no one selected");
            return;
        }
        MainController.Instance.PlayerData.EquipItem(selectedPlayerItem);
    }


    public void OnSell()
    {
        MainController.Instance.PlayerData.Sell(selectedPlayerItem);
    }

    private void OnItemEquipedCallback(BaseItem obj,bool val)
    {
        Debug.Log("OnItemEquiped");
        var item = PlayerItemElements.FirstOrDefault(x => x.PlayerItem == obj);
        if (item != null)
        {
            item.Equip(val);
        }
        AllParametersContainer.UpgradeValues();
    }

    public void OnSelected(PlayerItemElement item)
    {
        selectedPlayerItem = item.PlayerItem;
        ItemInfoElement.Init(selectedPlayerItem);
    }

    private void OnNewItem(BaseItem playerItem)
    {
        var element = DataBaseController.Instance.GetItem<PlayerItemElement>(PrefabPlayerItemElement);
        element.Init(playerItem, OnSelected);
        element.transform.SetParent(layoutMyInventory);
        PlayerItemElements.Add(element);
    }

    public override void Close()
    {
        MainController.Instance.PlayerData.OnNewItem -= OnNewItem;
        MainController.Instance.PlayerData.OnItemEquiped -= OnItemEquipedCallback;
        MainController.Instance.PlayerData.OnItemSold -= OnItemSoldCallback;
        MainController.Instance.PlayerData.OnCurrensyChanges -= OnCurrensyChanges;
        PlayerItemElements.Clear();
        ClearTransform(layoutMyInventory);
        ClearTransform(layoutShopItems);
        base.Close();
    }
}

