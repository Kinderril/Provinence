using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class PlayerData
{
    public const string INVENTORY = "INVENTORY_";
    public DictionaryOfItemAndInt playerInv = new DictionaryOfItemAndInt();

    public int levelHp = 1;
    public int levelPower = 1;
    public int levelDef = 1;
    public void Load()
    {
        foreach (ItemId v in Enum.GetValues(typeof(ItemId)))
        {
            var count = PlayerPrefs.GetInt(INVENTORY + v,0);
            playerInv.Add(v,count);
        }
    }

    public void Save()
    {
        foreach (var v in playerInv)
        {
            PlayerPrefs.SetInt(INVENTORY + v.Key.ToString(),v.Value);
        }
    }

    public void AddInventory(DictionaryOfItemAndInt inventory)
    {
        foreach (var kp in inventory)
        {
            playerInv[kp.Key] += kp.Value;
        }
    }
}

