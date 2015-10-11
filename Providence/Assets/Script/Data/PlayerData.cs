using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class PlayerData
{
    public const string INVENTORY = "INVENTORY_";
    public const string ITEMS = "ITEMS";
    public const char ITEMS_DELEMETER = ':';
    public DictionaryOfItemAndInt playerInv = new DictionaryOfItemAndInt();
    private List<PlayerItem> playerItems = new List<PlayerItem>();
    public event Action<PlayerItem> OnNewItem;

    public int levelHp = 0;
    public int levelPower = 0;
    public int levelDef = 0;
    public void Load()
    {
        foreach (ItemId v in Enum.GetValues(typeof(ItemId)))
        {
            var count = PlayerPrefs.GetInt(INVENTORY + v,0);
            playerInv.Add(v,count);
        }
        var allItems = PlayerPrefs.GetString(ITEMS, "").Split(ITEMS_DELEMETER);
        foreach (var item in allItems)
        {
            if (item.Length > 4)
            {
                PlayerItem playerItem = new PlayerItem(item);
                playerItems.Add(playerItem);
            }
        }

    }

    public IEnumerable<PlayerItem> GetAllWearedItems()
    {
        return playerItems.Where(x => x.isEquped);
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

    public void AddItem(PlayerItem item)
    {
        if (OnNewItem != null)
        {
            OnNewItem(item);
        }
        playerItems.Add(item);
    }

    public List<PlayerItem> GetAllItems()
    {
        return playerItems;
    }
}

