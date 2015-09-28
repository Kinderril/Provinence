using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum Slot
{
    physical_weapon,
    magic_weapon,
    body,
    helm
}
public class InventoryItem
{
    public Slot Slot;
    public Dictionary<ParamType, float> parameters;
    public bool isEquped;
}

