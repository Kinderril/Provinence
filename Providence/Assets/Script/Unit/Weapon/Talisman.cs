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

