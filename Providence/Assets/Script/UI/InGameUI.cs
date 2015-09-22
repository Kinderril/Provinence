using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{

    public Slider TImeSlider;
    public Slider HealthSlider;
    public Text moneyField;

    public void Init()
    {
        MainController.Instance.level.OnLeft += OnLeft;
        MainController.Instance.level.OnItemCollected += OnItemCollected;
        MainController.Instance.MainHero.OnGetHit += OnHeroHit;
    }

    private void OnItemCollected(ItemId arg1, float arg2,float delta)
    {
        switch (arg1)
        {
            case ItemId.money:
                moneyField.text = arg2.ToString("00");
                var item = DataBaseController.Instance.GetItem(DataBaseController.Instance.FlyingNumber, moneyField.transform.position);
        item.transform.SetParent(transform);
        Color color = DataBaseController.Instance.GetColor(arg1);
        item.Init(delta, color, (delta > 0) ? "+" : "-");
                break;
            case ItemId.crystal:
                break;
        }
    }

    private void OnHeroHit(float arg1, float arg2,float delta)
    {
        var item = DataBaseController.Instance.GetItem(DataBaseController.Instance.FlyingNumber, HealthSlider.transform.position);
        item.transform.SetParent(transform);
        Color color = DataBaseController.Instance.GetColor(ItemId.health);
        item.Init(delta, color, (delta > 0) ? "-" : "+");
        HealthSlider.value =  arg1/ arg2;
    }

    private void OnLeft(float arg1, float arg2)
    {
        var v = 1f - arg1/arg2;
        //Debug.Log(" val: " + v + "  arg1:" + arg1 + "  " + arg2);
        TImeSlider.value = v;
    }

}
