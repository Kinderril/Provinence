using System;
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

    public float PowerLeft
    {
        get { return powerLeft; }
        set
        {
            powerLeft = Mathf.Clamp(PowerLeft, -1, maxpower);
            powerLeft = value;
            if (OnLeft != null)
            {
                OnLeft(powerLeft, maxpower);
            }
        }
    }

    public void AddItem(ItemId type, int value)
    {
        Debug.Log("OnItemCollected");
        inventory[type] += value;
        ActivaAction(type, value);
        
    }

    private void ActivaAction(ItemId i,int delta)
    {
        if (OnItemCollected != null)
        {
            OnItemCollected(i, inventory[i], delta);
        }
        
    }



    public void Update()
    {
        PowerLeft += Time.deltaTime;
    }
}

