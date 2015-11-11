using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class Talisman
{
    public TalismanItem sourseItem;
    public float currentPower;
    public Action<bool,float> OnReady;

    public Talisman(TalismanItem sourseItem,int countTalismans)
    {
        this.sourseItem = sourseItem;
        MainController.Instance.level.OnItemCollected += (id, f, delta) =>
        {
            if (id == ItemId.energy)
            {
                AddEnergy(delta / countTalismans);
            }
        };
    }
    public void Use()
    {
        Debug.Log("Use!!! " + sourseItem.TalismanType);
        switch (sourseItem.TalismanType)
        {
            case  TalismanType.speed:
                TimeEffect effectDouble = new TimeEffect();
                effectDouble.Start(MainController.Instance.level.MainHero, EffectType.speed);
                break;
            case TalismanType.massPush:
                break;
            case TalismanType.firewave:
                break;
            case TalismanType.massFreez:
                var targets = Map.Instance.GetEnimiesInRadius(80);
                foreach (var baseMonster in targets)
                {
                    TimeEffect freezEffect = new TimeEffect();
                    freezEffect.Start(MainController.Instance.level.MainHero, EffectType.freez);
                }
                break;
            case TalismanType.heal:
                MainController.Instance.level.MainHero.GetHeal(currentPower);
                break;
            case TalismanType.doubleDamage:
                TimeEffect effect = new TimeEffect();
                effect.Start(MainController.Instance.level.MainHero, EffectType.doubleDamage);
                break;
        }
        AddEnergy(sourseItem.costShoot);
        DoCallback();
    }

    public void AddEnergy(float val)
    {
        currentPower = Mathf.Clamp(currentPower - val, 0, (float)sourseItem.costShoot + 1);
        Debug.Log("add energy " + currentPower + "/" + sourseItem.costShoot);
        DoCallback();
    }

    private void DoCallback()
    {
        if (OnReady != null)
        {
            OnReady(CanUse(), currentPower/ (float)sourseItem.costShoot);
        }
    }

    public bool CanUse()
    {
        return currentPower >= sourseItem.costShoot;
    }
}

