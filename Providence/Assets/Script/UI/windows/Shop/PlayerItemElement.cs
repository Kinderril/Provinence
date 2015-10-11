using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItemElement : MonoBehaviour
{
    public ParameterElement Prefab;
    public Text NameLabel;
    public Text SlotLabel;
    public Image rareImage;
    public Image iconImage;
    public Image equpedImage;
    public Transform layout;
    public void Init(PlayerItem item)
    {
        foreach (var p in item.parameters)
        {
            var element = DataBaseController.Instance.GetItem<ParameterElement>(Prefab);
            element.Init(p.Key, p.Value);
            element.transform.SetParent(layout);
        }
        SlotLabel.text = item.Slot.ToString();
        rareImage.gameObject.SetActive(item.isRare);
        equpedImage.gameObject.SetActive(item.isEquped);
        iconImage.sprite = Resources.Load<Sprite>(item.icon);

    }
}

