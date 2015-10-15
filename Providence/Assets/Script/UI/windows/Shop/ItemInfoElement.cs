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
    public Image SlotLabel;
    public Image mainIcon;

    public void Init(PlayerItem item)
    {
        SlotLabel.sprite = DataBaseController.Instance.SlotIcon(item.Slot);
        foreach (Transform t in layout)
        {
            Destroy(t.gameObject);
        }

        foreach (var p in item.parameters)
        {
            var element = DataBaseController.Instance.GetItem<ParameterElement>(Prefab);
            element.Init(p.Key, p.Value);
            element.transform.SetParent(layout);
        }

    }

    public void Init(IShopExecute item)
    {
        foreach (Transform t in layout)
        {
            Destroy(t.gameObject);
        }
        NameLabel.text = "Level:" + item.Parameter;
        if (item.CrystalCost > 0)
        {
            var element = DataBaseController.Instance.GetItem<ParameterElement>(Prefab);
            element.Init(ItemId.crystal, item.CrystalCost);
            element.transform.SetParent(layout);
        }
        if (item.MoneyCost > 0)
        {
            var element = DataBaseController.Instance.GetItem<ParameterElement>(Prefab);
            element.Init(ItemId.money, item.MoneyCost);
            element.transform.SetParent(layout);
        }
        mainIcon.sprite = item.icon;
    }
}

