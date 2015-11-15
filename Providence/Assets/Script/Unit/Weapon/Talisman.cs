using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class Talisman
{
    public TalismanItem sourseItem;
    public float currentEnergy;
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
                TimeEffect.Creat(MainController.Instance.level.MainHero, EffectType.speed);
                break;
            case TalismanType.massPush:
                break;
            case TalismanType.firewave:
                var targets2 = Map.Instance.GetEnimiesInRadius(80);
                foreach (var baseMonster in targets2)
                {
                    TimeEffect.Creat(baseMonster, EffectType.fire, sourseItem.power);
                }
                break;
            case TalismanType.massFreez:
                var targets = Map.Instance.GetEnimiesInRadius(80);
                foreach (var baseMonster in targets)
                {
                    TimeEffect.Creat(baseMonster, EffectType.freez);
                }
                break;
            case TalismanType.heal:
                MainController.Instance.level.MainHero.GetHeal(currentEnergy);
                break;
            case TalismanType.doubleDamage:
                TimeEffect.Creat(MainController.Instance.level.MainHero, EffectType.doubleDamage);
                break;
        }
        AddEnergy(sourseItem.costShoot);
        DoCallback();
    }

    public void AddEnergy(float val)
    {
        currentEnergy = Mathf.Clamp(currentEnergy - val, 0, (float)sourseItem.costShoot + 1);
        //Debug.Log("add energy " + currentEnergy + "/" + sourseItem.costShoot);
        DoCallback();
    }

    private void DoCallback()
    {
        if (OnReady != null)
        {
            OnReady(CanUse(), currentEnergy/ (float)sourseItem.costShoot);
        }
    }

    public bool CanUse()
    {
        return currentEnergy >= sourseItem.costShoot;
    }
}

