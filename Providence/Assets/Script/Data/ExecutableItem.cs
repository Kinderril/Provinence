using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum ExecutableType
{
    weaponUpdate,
    powerUpdate,
    armorUpdate,
    healthUpdate,
    
}
public class ExecutableItem : BaseItem
{
    public ExecutableType ExecutableType;

    public ExecutableItem()
    {
        Slot = Slot.none;
    }

    public override char FirstChar()
    {
        return '©';
    }

    public override void Activate(Hero hero)
    {

    }

    public override string Save()
    {
        return "";
    }
}

