using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor.iOS.Xcode;
using UnityEngine;
using UnityEngine.UI;


public class WindowPersonage : BaseWindow
{
    public ParameterUpgradeElement PrefabParameterUpgrade;
    public GameParameterElement PrefabGameElement;
    private List<ParameterUpgradeElement> elements = new List<ParameterUpgradeElement>();
    private List<GameParameterElement> elementsParams = new List<GameParameterElement>();
    public Transform layout;
    public Transform layoutGameElements;
    public Text moneyField;
    public Text crystalField;

    public override void Init()
    {
        base.Init();
        moneyField.text = MainController.Instance.PlayerData.playerInv[ItemId.money].ToString("0");
        crystalField.text = MainController.Instance.PlayerData.playerInv[ItemId.crystal].ToString("0");
        elements.Clear();
        elementsParams.Clear();
        LoadParameters();
        LoadTotalParameters();
        MainController.Instance.PlayerData.OnParametersChange += OnParametersChange;
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
    private void OnParametersChange(Dictionary<MainParam, int> obj)
    {
        foreach (var parameterUpgradeElement in elements)
        {
            parameterUpgradeElement.UpgradeData();
        }
        foreach (var parameterUpgradeElement in elementsParams)
        {
            parameterUpgradeElement.UpgradeData();
        }
    }

    private void LoadTotalParameters()
    {
        foreach (ParamType v in Enum.GetValues(typeof(ParamType)))
        {
            var item = DataBaseController.Instance.GetItem(PrefabGameElement);
            item.Init(v);
            item.gameObject.transform.SetParent(layoutGameElements);
            elementsParams.Add(item);
        }
    }

    private void LoadParameters()
    {
        var baseParams = MainController.Instance.PlayerData.MainParameters;
        foreach (var baseParam in baseParams)
        {
            var item = DataBaseController.Instance.GetItem(PrefabParameterUpgrade);
            item.Init(baseParam.Key);
            item.gameObject.transform.SetParent(layout);
            elements.Add(item);
        }
    }

    public override void Close()
    {
        base.Close();
        ClearTransform(layout);
        ClearTransform(layoutGameElements);
        MainController.Instance.PlayerData.OnParametersChange -= OnParametersChange;
        MainController.Instance.PlayerData.OnCurrensyChanges -= OnCurrensyChanges;
    }
}

