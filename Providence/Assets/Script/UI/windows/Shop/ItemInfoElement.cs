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
    public Image SpecIcon;

    public void Init(BaseItem item)
    {
        SlotLabel.sprite = DataBaseController.Instance.SlotIcon(item.Slot);
        foreach (Transform t in layout)
        {
            Destroy(t.gameObject);
        }
        var playerItem = item as PlayerItem;
        if (playerItem != null)
        {
            foreach (var p in playerItem.parameters)
            {
                var element = DataBaseController.Instance.GetItem<ParameterElement>(Prefab);
                element.Init(p.Key, p.Value);
                element.transform.SetParent(layout);
            }
            var haveSpec = playerItem.specialAbilities != SpecialAbility.none;
            SpecIcon.gameObject.SetActive(haveSpec);
            if (haveSpec)
            {
                SpecIcon.sprite = DataBaseController.Instance.SpecialAbilityIcon(playerItem.specialAbilities);
            }
            mainIcon.sprite = Resources.Load<Sprite>("sprites/PlayerItems/" + playerItem.icon);
            return;
        }
        var talismanItem = item as TalismanItem;
        if (talismanItem != null)
        {
            mainIcon.sprite = DataBaseController.Instance.TalismanIcon(talismanItem.TalismanType);
            var element = DataBaseController.Instance.GetItem<ParameterElement>(Prefab);
            element.Init(ParamType.PPower, talismanItem.power);
            element.Init(ParamType.MDef, talismanItem.costShoot);
            element.transform.SetParent(layout);
        }
        var bonusItem = item as BonusItem;
        if (bonusItem != null)
        {
//            mainIcon.sprite = DataBaseController.Instance.TalismanIcon(bonusItem.Bonustype);
            var element = DataBaseController.Instance.GetItem<ParameterElement>(Prefab);
            element.Init(ParamType.PPower, bonusItem.power);

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

