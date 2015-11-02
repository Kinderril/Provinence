using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Talisman
{
    public TalismanItem sourseItem;
    public int currentPower;


    public void Use()
    {
        switch (sourseItem.TalismanType)
        {
                
        }
    }

    public bool CanUse()
    {
        return currentPower >= sourseItem.costShoot;
    }
}

