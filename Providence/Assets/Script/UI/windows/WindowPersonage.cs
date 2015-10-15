using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor.iOS.Xcode;
using UnityEngine;


public class WindowPersonage : BaseWindow
{
    public ParameterUpgradeElement ParameterUpgradePrefab;
    public GameParameterElement GameElementPrefab;
    private List<ParameterUpgradeElement> elements = new List<ParameterUpgradeElement>();
    private List<GameParameterElement> elementsParams = new List<GameParameterElement>();
    public Transform layout;
    public Transform layoutGameElements;

    public override void Init()
    {
        base.Init();
        elements.Clear();
        elementsParams.Clear();
        LoadParameters();
        LoadTotalParameters();
        MainController.Instance.PlayerData.OnParametersChange += OnParametersChange;
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

    public void OnToMission()
    {
        WindowManager.Instance.OpenWindow(MainState.start);
    }
    public void OnToShop()
    {
        WindowManager.Instance.OpenWindow(MainState.shop);
    }

    private void LoadTotalParameters()
    {
        foreach (ParamType v in Enum.GetValues(typeof(ParamType)))
        {
            var item = DataBaseController.Instance.GetItem(GameElementPrefab);
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
            var item = DataBaseController.Instance.GetItem(ParameterUpgradePrefab);
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
    }
}

