using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


public class ItemInfoElement : MonoBehaviour
{
    public ParameterElement Prefab;
    public Text NameLabel;
    public Transform layout;
    public Text SlotLabel;
    public Image mainIcon;

    public void Init(PlayerItem item)
    {
        SlotLabel.text = item.Slot.ToString();
        foreach (var p in item.parameters)
        {
            var element = DataBaseController.Instance.GetItem<ParameterElement>(Prefab);
            element.Init(p.Key, p.Value);
            element.transform.SetParent(layout);
        }

    }

    public void Init(ShopItem item)
    {
        NameLabel.text = "Level:" + item.Parameter;
        if (item.CrystalCost > 0)
        {
            var element = DataBaseController.Instance.GetItem<ParameterElement>(Prefab);
            element.Init(ItemId.crystal, item.CrystalCost);
        }
        if (item.MoneyCost > 0)
        {
            var element = DataBaseController.Instance.GetItem<ParameterElement>(Prefab);
            element.Init(ItemId.money, item.MoneyCost);
        }
        mainIcon.sprite = item.icon;
    }
}

