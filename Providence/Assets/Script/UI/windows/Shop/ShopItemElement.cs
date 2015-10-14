using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


public class ShopItemElement : MonoBehaviour
{
    public Image icon;
    public ShopItem shopExecute;
    public Text lvlField;
    public Image overlay;
    private bool isOpen = false;
    private Action<ShopItem> callback;

    public void Init(ShopItem shopExecute, Action<ShopItem> callback)
    {
        this.shopExecute = shopExecute;
        this.callback = callback;
        icon.sprite = shopExecute.execute.Icon;
        lvlField.text = "Level:" + shopExecute.execute.value;
        isOpen = MainController.Instance.PlayerData.Level >= shopExecute.execute.value;
        overlay.gameObject.SetActive(isOpen);
    }

    public void OnClick()
    {
        if (isOpen)
            callback(shopExecute);
    }
}

