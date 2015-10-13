using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class WindowShop : BaseWindow
{

    public Transform layout;
    public PlayerItemElement Prefab;
    public ItemInfoElement ItemInfoElement;
    private List<PlayerItemElement> PlayerItemElements;
    public RectTransform deletePlace;
    public RectTransform equipPlace;
    public Text moneyField;
    public Text crystalField;

    public void OnBuySimpleChest()
    {
        ShopController.Instance.BuyItem(DataBaseController.Instance.stubPrefab);
    }
    public override void Init()
    {
        base.Init();
        PlayerItemElements = new List<PlayerItemElement>();
        List<PlayerItem> items = MainController.Instance.PlayerData.GetAllItems();
        foreach (var playerItem in items)
        {
            var element = DataBaseController.Instance.GetItem<PlayerItemElement>(Prefab);
            element.Init(playerItem, OnSelected, OnIsOnWhat);
            element.transform.SetParent(layout);
            PlayerItemElements.Add(element);
        }
        MainController.Instance.PlayerData.OnNewItem += OnNewItem;
        MainController.Instance.PlayerData.OnItemEquiped += OnItemEquiped;
        MainController.Instance.PlayerData.OnItemSold += OnItemSold;
        MainController.Instance.PlayerData.OnCurrensyChanges += OnCurrensyChanges;
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
        var element = DataBaseController.Instance.GetItem<PlayerItemElement>(Prefab);
        element.Init(playerItem, OnSelected, OnIsOnWhat);
        element.transform.SetParent(layout);
    }

    public override void Close()
    {
        MainController.Instance.PlayerData.OnNewItem -= OnNewItem;
        MainController.Instance.PlayerData.OnItemEquiped -= OnItemEquiped;
        MainController.Instance.PlayerData.OnItemSold -= OnItemSold;
        MainController.Instance.PlayerData.OnCurrensyChanges -= OnCurrensyChanges;
        PlayerItemElements.Clear();
        foreach (Transform child in layout.transform)
        {
            Destroy(child.gameObject);
        }
        base.Close();
    }
}

