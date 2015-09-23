﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public struct inventotyItem
{
    public ItemId id;
    public int count;

    public inventotyItem(ItemId id)
    {
        this.id = id;
        count = 0;
    }
}
[Serializable]
public class DictionaryOfItemAndInt : SerializableDictionary<ItemId, int> { }

public class Level
{
    private float powerLeft;
    private float maxpower = 120;
    public Action<float, float> OnLeft;
    public Action<ItemId,float,float> OnItemCollected;
    private DictionaryOfItemAndInt inventory;
    public int difficult = 1;

    public void Init()
    {
        inventory = new DictionaryOfItemAndInt();
        foreach (ItemId suit in Enum.GetValues(typeof(ItemId)))
        {
            inventory.Add(suit,0);
        }
    }


    public void AddItem(ItemId type, int value)
    {
        Debug.Log("OnItemCollected");
        switch (type)
        {
            case ItemId.money:
            case ItemId.crystal:
                inventory[type] += value;
                ActivaAction(type, value);
                break;
            case ItemId.energy:
                powerLeft = Mathf.Clamp(powerLeft + value, -1, maxpower);
                ActionPOwerLeft();
                ActivaAction(type, value);
                break;
            case ItemId.health:
                break;
        }
        
    }

    private void ActivaAction(ItemId i,int delta)
    {
        if (OnItemCollected != null)
        {
            OnItemCollected(i, inventory[i], delta);
        }
        
    }

    private void ActionPOwerLeft()
    {
        if (OnLeft != null)
        {
            OnLeft(powerLeft, maxpower);
        }
    }

    public void Update()
    {
        powerLeft += Time.deltaTime;
        ActionPOwerLeft();
    }
}

