using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class WindowInGame : BaseWindow
{

    public Slider TImeSlider;
    public Slider HealthSlider;
    public Text moneyField;
    public WeaponChooserView WeaponChooser;
    public UIMain UiControls;
    public Transform hitTransform;
    public List<TalismanButton> TalismanButtons; 

    public override void Init()
    {
        base.Init();
        UiControls.Init();
        MainController.Instance.level.OnLeft += OnLeft;
        MainController.Instance.level.OnItemCollected += OnItemCollected;
        MainController.Instance.level.MainHero.OnGetHit += OnHeroHit;
        MainController.Instance.level.MainHero.OnWeaponChanged += OnWeaponChanged;
        WeaponChooser.Init();
        UiControls.Enable(true);
        int index = 0;
        foreach (var talic in MainController.Instance.PlayerData.GetAllWearedItems().Where(x =>x.Slot == Slot.Talisman))
        {
            var talismain = talic as TalismanItem;
            TalismanButtons[0].Init(talismain);
            index++;
        }
        for (int i = index; i < TalismanButtons.Count; i++)
        {
            TalismanButtons[i].gameObject.SetActive(false);
        }
    }

    private void OnWeaponChanged(Weapon obj)
    {
        WeaponChooser.SetWeapon(obj);
    }

    public void EndGame()
    {
        UiControls.Enable(false);
    }

    public override void Close()
    {
        base.Close();
        UiControls.Enable(false);
        MainController.Instance.level.OnLeft -= OnLeft;
        MainController.Instance.level.OnItemCollected -= OnItemCollected;
        MainController.Instance.level.MainHero.OnGetHit -= OnHeroHit;
        MainController.Instance.level.MainHero.OnWeaponChanged -= OnWeaponChanged;
    }

    private void OnItemCollected(ItemId arg1, float arg2,float delta)
    {
        FlyingNumbers item;
        switch (arg1)
        {
            case ItemId.money:
                moneyField.text = arg2.ToString("00");
                item = DataBaseController.Instance.GetItem(DataBaseController.Instance.FlyingNumber, moneyField.transform.position);
        item.transform.SetParent(transform);
                item.Init(delta, DataBaseController.Instance.GetColor(arg1), (delta > 0) ? "+" : "-");
                break;
            case ItemId.crystal:
                break;
            case ItemId.energy:
                item = DataBaseController.Instance.GetItem(DataBaseController.Instance.FlyingNumber, moneyField.transform.position);
                item.transform.SetParent(transform);
                item.Init(delta, DataBaseController.Instance.GetColor(arg1), (delta < 0) ? "+" : "-");

                break;
        }
    }

    private void OnHeroHit(float arg1, float arg2,float delta)
    {
        var item = DataBaseController.Instance.GetItem(DataBaseController.Instance.FlyingNumber, hitTransform.position);
        item.transform.SetParent(transform);
        Color color = DataBaseController.Instance.GetColor(ItemId.health);
        item.Init(delta, color, (delta > 0) ? "-" : "+");
        HealthSlider.value =  arg1/ arg2;
    }

    private void OnLeft(float arg1, float arg2)
    {
        var v = 1f - arg1/arg2;
        TImeSlider.value = v;
    }

}
