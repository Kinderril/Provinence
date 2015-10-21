using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public enum MainParam
{
    HP = 0,
    DEF = 1,
    ATTACK = 2,
}

public class PlayerData
{
    public const string INVENTORY = "INVENTORY_";
    public const string ITEMS = "ITEMS";
    public const string BASE_PARAMS = "BASE_PARAMS";
    public const char ITEMS_DELEMETER = ':';
    public DictionaryOfItemAndInt playerInv = new DictionaryOfItemAndInt();
    private List<PlayerItem> playerItems = new List<PlayerItem>();
    public event Action<PlayerItem> OnNewItem;
    public event Action<PlayerItem,bool> OnItemEquiped;
    public event Action<PlayerItem> OnItemSold;
    public event Action<Dictionary<MainParam, int>> OnParametersChange;
    public event Action<ItemId, int> OnCurrensyChanges;
    public Dictionary<MainParam,int> MainParameters;
    private int level;

    public int Level
    {
        get { return level; }
    }

    public void UpgdareParameter(MainParam parameter)
    {
        Debug.Log("Upgdare Main Parameter " + parameter);
        var cost = DataBaseController.Instance.DataStructs.costParameterByLvl[MainParameters[parameter]];
        AddCurrensy(ItemId.money,-cost);
        MainParameters[parameter] += 1;
        if (OnParametersChange != null)
        {
            OnParametersChange(MainParameters);
        }
        Save();
    }

    public bool CanUpgradeParameter(int nextLvl)
    {
        if (DataBaseController.Instance.DataStructs.costParameterByLvl.Length < nextLvl)
        {
            var cost = DataBaseController.Instance.DataStructs.costParameterByLvl[nextLvl];
            return CanPay(ItemId.money, cost);
        }
        return false;
    }

    public bool CanPay(ItemId t, int cost)
    {
        return cost < playerInv[t];
    }


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
        MainParameters = new Dictionary<MainParam, int>();
        var bp = PlayerPrefs.GetString(BASE_PARAMS, "");
        if (bp.Length > 3)
        {
            var allParams = bp.Split(ITEMS_DELEMETER);
            int i = 0;
            foreach (var p in allParams)
            {
                if (p.Length > 0)
                {
                    int par = Convert.ToInt32(p);
                    MainParameters.Add((MainParam) i, par);
                    i++;
                }
            }
            if (MainParameters.Count != 3)
            {
                Debug.LogError("BAD PARAMETERS LOAD " + MainParameters.Count + "   bp:" + bp);
                MainParameters.Clear();
                MainParameters.Add(global::MainParam.ATTACK, 1);
                MainParameters.Add(global::MainParam.HP, 1);
                MainParameters.Add(global::MainParam.DEF, 1);
            }
        }
        else
        {
            MainParameters.Add(global::MainParam.ATTACK, 1);
            MainParameters.Add(global::MainParam.HP, 1);
            MainParameters.Add(global::MainParam.DEF, 1);
        }

        level = 0;
        foreach (var baseParameter in MainParameters)
        {
            level += baseParameter.Value;
        }
        level -= 2;
    }

    public IEnumerable<PlayerItem> GetAllWearedItems()
    {
        return playerItems.Where(x => x.IsEquped);
    }

    public void Save()
    {
        foreach (var v in playerInv)
        {
            PlayerPrefs.SetInt(INVENTORY + v.Key.ToString(),v.Value);
        }
        string bsStr = "";
        foreach (var baseParameter in MainParameters)
        {
            bsStr += baseParameter.Value.ToString() + ITEMS_DELEMETER;
        }
        Debug.Log("Save ALl DATA :: " + bsStr);
        PlayerPrefs.SetString(BASE_PARAMS, bsStr);
    }

    public void AddInventory(DictionaryOfItemAndInt inventory)
    {
        foreach (var kp in inventory)
        {
            AddCurrensy(kp.Key,kp.Value);
        }
    }

    public void AddCurrensy(ItemId id, int count)
    {
        playerInv[id] += count;
        if (OnCurrensyChanges != null)
        {
            OnCurrensyChanges(id, playerInv[id]);
        }
        Save();

    }

    public void AddItem(PlayerItem item)
    {
        if (OnNewItem != null)
        {
            OnNewItem(item);
        }
        playerItems.Add(item);
        Save();
    }

    public List<PlayerItem> GetAllItems()
    {
        return playerItems;
    }

    public void EquipItem(PlayerItem playerItem)
    {
        var oldEquipedItem = playerItems.FirstOrDefault(x => x.IsEquped && x.Slot == playerItem.Slot);
        if (oldEquipedItem != null)
        {
            oldEquipedItem.IsEquped = false;
        }
        playerItem.IsEquped = true;

        if (OnItemEquiped != null)
        {
            if (oldEquipedItem != null)
            {
                OnItemEquiped(oldEquipedItem, false);
            }
            OnItemEquiped(playerItem, true);
        }
        Save();
    }

    public void Sell(PlayerItem playerItem)
    {
        AddCurrensy(ItemId.money, -playerItem.cost/3);
        playerItem.IsEquped = false;
        if (OnItemSold != null)
        {
            OnItemSold(playerItem);
        }
        Save();
    }

    public float CalcParameter(ParamType type)
    {
        float v = 0;
        foreach (var playerItem in playerItems.Where(x=>x.IsEquped && x.parameters.ContainsKey(type)))
        {
            foreach (var parameter in playerItem.parameters)
            {
                if (parameter.Key == type)
                {
                    v += parameter.Value;
                }
            }
        }
        switch (type)
        {
            case ParamType.Speed:
                v += 3;
                break;
            case ParamType.MPower:
                v += MainParameters[MainParam.ATTACK];
                break;
            case ParamType.PPower:
                v += MainParameters[MainParam.ATTACK];
                break;
            case ParamType.PDef:
                v += MainParameters[MainParam.DEF];
                break;
            case ParamType.MDef:
                v += MainParameters[MainParam.DEF];
                break;
            case ParamType.Hp:
                v += MainParameters[MainParam.HP] * 40;
                break;
        }
        return v;
    }
}

