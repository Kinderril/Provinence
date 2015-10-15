using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


public class ParameterElement : MonoBehaviour
{
    public Text label;
    public Image Icon;

    public void Init(ParamType param,float val)
    {
        var spr = Resources.Load<Sprite>(DataBaseController.Instance.ParameterIcon(param));
        Icon.sprite = spr;
        label.text = param + "   " + val.ToString("0");
    }

    public void Init(ItemId param, float val)
    {
        var spr = Resources.Load<Sprite>(DataBaseController.Instance.ItemIcon(param));
        Icon.sprite = spr;
        label.text = param + "   " + val.ToString("0");
    }
}

