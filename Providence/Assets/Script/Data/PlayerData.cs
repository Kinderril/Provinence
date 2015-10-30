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
    public const string LEVEL = "LEVEL_";
    public const string ALLOCATED = "ALLOCATED_";
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
    public event Action<int> OnLevelUp;
    public event Action<ItemId, int> OnCurrensyChanges;
    public int AllocatedPoints;
    private int CurrentLevel;
    public Dictionary<MainParam,int> MainParameters;
    private readonly Dictionary<Slot,int> slotsCount = new Dictionary<Slot, int>() { {Slot.Talisman, 2} }; 

    public int Level
    {
        get { return CurrentLevel; }
    }

    private int GetSlotCount(Slot s)
    {
        var c = 1;
        if (slotsCount.TryGetValue(s, out c))
        {
            return c;
        }
        return 1;
    }

    public void UpgdareParameter(MainParam parameter)
    {
        if (CanUpgradeParameter())
        {
            Debug.Log("Upgdare Main Parameter " + parameter);
            var cost = DataBaseController.Instance.DataStructs.costParameterByLvl[MainParameters[parameter]];
            AddCurrensy(ItemId.money, -cost);
            AllocatedPoints -= 1;
            MainParameters[parameter] += 1;
            if (OnParametersChange != null)
            {
                OnParametersChange(MainParameters);
            }
            Save();
        }
    }

    public bool CanUpgradeParameter()
    {
        return (AllocatedPoints > 0);
    }

    public bool CanUpgradeLevel()
    {
        if (DataBaseController.Instance.DataStructs.costParameterByLvl.Length < CurrentLevel)
        {
            var cost = DataBaseController.Instance.DataStructs.costParameterByLvl[CurrentLevel];
            return CanPay(ItemId.money, cost);
        }
        return false;
    }

    public void LevelUp()
    {
        var cost = DataBaseController.Instance.DataStructs.costParameterByLvl[CurrentLevel];
        AllocatedPoints += 2;
        CurrentLevel++;
        if (OnLevelUp != null)
        {
            OnLevelUp(CurrentLevel);
        }
        AddCurrensy(ItemId.money, -cost);
    }

    public bool CanPay(ItemId t, int cost)
    {
        return cost <= playerInv[t];
    }


    public void Load()
    {
        CurrentLevel = PlayerPrefs.GetInt(LEVEL, 1);
        AllocatedPoints = PlayerPrefs.GetInt(ALLOCATED, 0);
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
        CheckIfFirstLevel();
    }

    private void CheckIfFirstLevel()
    {
        var money = playerInv[ItemId.money] == 0;
        var lvl = Level == 0;
        var items = playerItems.Count == 0;
        if (money && lvl && items)
        {
            PlayerItem item1 = new PlayerItem(new Dictionary<ParamType, float>() { {ParamType.PPower, 15} },Slot.physical_weapon, false,1);
            PlayerItem item2 = new PlayerItem(new Dictionary<ParamType, float>() { { ParamType.MPower, 10 } }, Slot.magic_weapon, false, 1);
            playerItems.Add(item2);
            playerItems.Add(item1);
            EquipItem(item1);
            EquipItem(item2);
        }
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
        PlayerPrefs.SetInt(LEVEL,CurrentLevel);
        PlayerPrefs.SetInt(ALLOCATED, AllocatedPoints);
    }
    public IEnumerable<PlayerItem> GetAllWearedItems()
    {
        return playerItems.Where(x => x.IsEquped);
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

    public void EquipItem(PlayerItem playerItem,bool equip = true)
    {
        PlayerItem oldEquipedItem = null;
        if (equip)
        {
            var oldEquipedItems = playerItems.Where(x => x.IsEquped && x.Slot == playerItem.Slot);
            var slotCount = GetSlotCount(playerItem.Slot);
            if (oldEquipedItems.Count() >= slotCount)
            {
                var item2Unquip = oldEquipedItems.GetEnumerator().Current;
                item2Unquip.IsEquped = false;
                if (OnItemEquiped != null)
                {
                    OnItemEquiped(item2Unquip, false);
                }

            }
            playerItem.IsEquped = true;
            if (OnItemEquiped != null)
            {
                OnItemEquiped(playerItem, true);
            }

        }
        else
        {
            playerItem.IsEquped = false;
            if (OnItemEquiped != null)
            {
                OnItemEquiped(playerItem, false);
            }

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
                v += MainParameters[MainParam.ATTACK] * 12 + 36;
                break;
            case ParamType.PPower:
                v += MainParameters[MainParam.ATTACK] * 10 + 32;
                break;
            case ParamType.PDef:
                v += MainParameters[MainParam.DEF] * 10 + 10;
                break;
            case ParamType.MDef:
                v += MainParameters[MainParam.DEF] * 9;
                break;
            case ParamType.Hp:
                v += MainParameters[MainParam.HP] * 40 + 200;
                break;
        }
        return v;
    }



}

