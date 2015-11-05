using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Talisman
{
    public TalismanItem sourseItem;
    public float currentPower;
    public Action<bool,float> OnReady;

    public Talisman(TalismanItem sourseItem)
    {
        this.sourseItem = sourseItem;
        MainController.Instance.level.OnItemCollected += (id, f, delta) =>
        {
            if (id == ItemId.energy)
            {
                AddEnergy(delta);
            }
        };
    }
    public void Use()
    {
        switch (sourseItem.TalismanType)
        {
            case TalismanType.firewave:
                break;
            case TalismanType.chain:
                break;
            case TalismanType.massSlow:
                break;
            case TalismanType.heal:
                break;
            case TalismanType.doubleDamage:
                TimeEffect effect = new TimeEffect();
                effect.Start(MainController.Instance.level.MainHero, EffectType.doubleDamage);
                break;
        }
        AddEnergy(-sourseItem.costShoot);
        DoCallback();
    }

    public void AddEnergy(float val)
    {
        currentPower += val;
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

