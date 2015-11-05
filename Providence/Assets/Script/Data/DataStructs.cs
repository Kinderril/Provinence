﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public struct ColorUI
{
    public Color color;
    public ItemId type;
}
[Serializable]
public struct ParameterImage
{
    public ParamType type;
    public string path;
}
[Serializable]
public struct MainParameterImage
{
    public MainParam type;
    public string path;
}
[Serializable]
public struct ItemImage
{
    public ItemId type;
    public string path;
}
[Serializable]
public struct SlotImage
{
    public Slot type;
    public string path;
}
[Serializable]
public struct SpecialAbilityImage
{
    public SpecialAbility type;
    public string path;
}
[Serializable]
public struct TalismanImage
{
    public TalismanType type;
    public string path;
}
public class DataStructs : MonoBehaviour
{
    public MainParameterImage[] MainParametersImages;
    public ParameterImage[] ParametersImages;
    public ItemImage[] ItemImage;
    public SlotImage[] SlotImage;
    public ColorUI[] ColorsOfUI;
    public TalismanImage[] TalismanImage;
    public SpecialAbilityImage[] SpecialAbilityImage;
    public int[] costParameterByLvl;
}

