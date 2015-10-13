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
}

