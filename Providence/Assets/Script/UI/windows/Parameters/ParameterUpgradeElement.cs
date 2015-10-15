using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


public class ParameterUpgradeElement : MonoBehaviour
{
    public Button UpgradeButton;
    public Text CurrentValue;
    private MainParam type;
    public Image Icon;
    public Text nextLevelCost;

    public void Init(MainParam type)
    {
        this.type = type;
        UpgradeData();

        var spr = Resources.Load<Sprite>(DataBaseController.Instance.MainParameterIcon(type));
        Icon.sprite = spr;
    }

    public void UpgradeData()
    {
        var val = MainController.Instance.PlayerData.MainParameters[type];
        CurrentValue.text = val.ToString();
        nextLevelCost.text = DataBaseController.Instance.costParameterByLvl[val + 1].ToString("0");
        UpgradeButton.interactable = MainController.Instance.PlayerData.CanUpgradeParameter(val + 1);
    }

    public void OnUpgradeClick()
    {
        MainController.Instance.PlayerData.UpgdareParameter(type);
    }
}

