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
/*
[Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField]
    private List<TKey> keys = new List<TKey>();

    [SerializeField]
    private List<TValue> values = new List<TValue>();

    // save the dictionary to lists
    public void OnBeforeSerialize()
    {
        keys.Clear();
        values.Clear();
        foreach (KeyValuePair<TKey, TValue> pair in this)
        {
            keys.Add(pair.Key);
            values.Add(pair.Value);
        }
    }

    // load dictionary from lists
    public void OnAfterDeserialize()
    {
        this.Clear();

        if (keys.Count != values.Count)
            throw new System.Exception(string.Format("there are {0} keys and {1} values after deserialization. Make sure that both key and value types are serializable."));

        for (int i = 0; i < keys.Count; i++)
            this.Add(keys[i], values[i]);
    }
}
*/
[Serializable]
public class DictionaryOfItemAndInt : SerializableDictionary<ItemId, int> { }

public class Level
{
    private float powerLeft;
    private float maxpower = 120;
    public Action<float, float> OnLeft;
    public Action<float> OnMoneyCollected;
    private DictionaryOfItemAndInt inventory; 

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

    public int AddMoneyCollected
    {
        set
        {
            inventory[ItemId.money] += value;
            if (OnMoneyCollected != null)
            {
                OnMoneyCollected(inventory[ItemId.money]);
            }
        }
    }



    public void Update()
    {
        PowerLeft += Time.deltaTime;
    }
}

