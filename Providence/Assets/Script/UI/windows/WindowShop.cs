using System;
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
    public RectTransform deletePlace;
    public RectTransform equipPlace;
    public Text moneyField;
    public Text crystalField;
    private IShopExecute selectedShopElement;
    private PlayerItem selectedPlayerItem;

    public override void Init()
    {
        base.Init();
        moneyField.text = MainController.Instance.PlayerData.playerInv[ItemId.money].ToString("0");
        crystalField.text = MainController.Instance.PlayerData.playerInv[ItemId.crystal].ToString("0");
        PlayerItemElements = new List<PlayerItemElement>();
        List<PlayerItem> items = MainController.Instance.PlayerData.GetAllItems();
        foreach (var playerItem in items)
        {
            var element = DataBaseController.Instance.GetItem<PlayerItemElement>(PrefabPlayerItemElement);
            element.Init(playerItem, OnSelected, OnIsOnWhat);
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
        MainController.Instance.PlayerData.OnItemEquiped += OnItemEquiped;
        MainController.Instance.PlayerData.OnItemSold += OnItemSold;
        MainController.Instance.PlayerData.OnCurrensyChanges += OnCurrensyChanges;
    }

    public void OnBuySimpleChest()
    {
        if (selectedShopElement != null && selectedShopElement.CanBuy && EnoughtMoney(selectedShopElement))
            ShopController.Instance.BuyItem(selectedShopElement);
    }

    private bool EnoughtMoney(IShopExecute selectedShopElement)
    {
        return true;
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

    private void OnItemSold(PlayerItem obj)
    {
        Debug.Log("OnItemSold");
        var item = PlayerItemElements.FirstOrDefault(x => x.PlayerItem == obj);
        if (item != null)
        {
            Destroy(item.gameObject);
        }
    }

    private UnderUi OnIsOnWhat(Vector2 arg1)
    {
        var dc = arg1 - new Vector2(deletePlace.position.x, deletePlace.position.y);
        if (deletePlace.rect.Contains(dc))
        {
            return UnderUi.delete;
        }
        if (equipPlace.rect.Contains(arg1))
        {
            return UnderUi.equip;
        }
        return UnderUi.none;
    }

    private void OnItemEquiped(PlayerItem obj,bool val)
    {
        Debug.Log("OnItemEquiped");
        var item = PlayerItemElements.FirstOrDefault(x => x.PlayerItem == obj);
        if (item != null)
        {
            item.Equip(val);
        }

    }

    public void OnSelected(PlayerItemElement item)
    {
        ItemInfoElement.Init(item.PlayerItem);
    }

    private void OnNewItem(PlayerItem playerItem)
    {
        var element = DataBaseController.Instance.GetItem<PlayerItemElement>(PrefabPlayerItemElement);
        element.Init(playerItem, OnSelected, OnIsOnWhat);
        element.transform.SetParent(layoutMyInventory);
    }

    public override void Close()
    {
        MainController.Instance.PlayerData.OnNewItem -= OnNewItem;
        MainController.Instance.PlayerData.OnItemEquiped -= OnItemEquiped;
        MainController.Instance.PlayerData.OnItemSold -= OnItemSold;
        MainController.Instance.PlayerData.OnCurrensyChanges -= OnCurrensyChanges;
        PlayerItemElements.Clear();
        ClearTransform(layoutMyInventory);
        ClearTransform(layoutShopItems);
        base.Close();
    }
}

