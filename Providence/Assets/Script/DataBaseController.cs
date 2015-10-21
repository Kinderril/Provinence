using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ItemId
{
    money,
    crystal,
    energy,
    health
}


public class DataBaseController : Singleton<DataBaseController>
{
    private readonly Dictionary<ItemId, Sprite> ItemIdSprites = new Dictionary<ItemId, Sprite>();
    private readonly Dictionary<MainParam, Sprite> MainParamSprites = new Dictionary<MainParam, Sprite>();
    private readonly Dictionary<ParamType, Sprite> ParamTypeSprites = new Dictionary<ParamType, Sprite>();
    private readonly Dictionary<Slot, Sprite> SlotSprites = new Dictionary<Slot, Sprite>();
    public List<IShopExecute> allShopElements;
    public Chest chestPrefab;
    public DataStructs DataStructs;
    public GameObject debugCube;
    public FlyingNumbers FlyingNumber;
    public FlyingNumbers InGameFlyingNumber;
    public MapItem MapItemPrefab;
    public int maxLevel = 20;
    public List<BaseMonster> Monsters;
    public Dictionary<int, List<BaseMonster>> mosntersLevel = new Dictionary<int, List<BaseMonster>>();
    public Hero prefabHero;
    public List<Weapon> Weapons;

    private void Awake()
    {
        for (var i = 0; i < maxLevel; i++)
        {
            mosntersLevel.Add(i, new List<BaseMonster>());
        }
        foreach (var baseMonster in Monsters)
        {
            mosntersLevel[baseMonster.Parameters.Level].Add(baseMonster);
        }
        LoadSprites();
    }

    private void LoadSprites()
    {
        foreach (var mp in DataStructs.MainParametersImages)
        {
            MainParamSprites.Add(mp.type, Resources.Load<Sprite>("sprites/MainParameters/" + mp.path));
        }
        foreach (var mp in DataStructs.SlotImage)
        {
            SlotSprites.Add(mp.type, Resources.Load<Sprite>("sprites/Slots/" + mp.path));
        }
        foreach (var mp in DataStructs.ItemImage)
        {
            ItemIdSprites.Add(mp.type, Resources.Load<Sprite>("sprites/Items/" + mp.path));
        }
        foreach (var mp in DataStructs.ParametersImages)
        {
            ParamTypeSprites.Add(mp.type, Resources.Load<Sprite>("sprites/Parameters/" + mp.path));
        }
    }

    public Sprite MainParameterIcon(MainParam mp)
    {
        return MainParamSprites[mp];
    }

    public Sprite SlotIcon(Slot mp)
    {
        return SlotSprites[mp];
    }

    public Sprite ItemIcon(ItemId itemId)
    {
        return ItemIdSprites[itemId];
    }

    public Sprite ParameterIcon(ParamType mp)
    {
        return ParamTypeSprites[mp];
    }

    public T GetItem<T>(T item, Vector3 pos) where T : MonoBehaviour
    {
        return (Instantiate(item.gameObject, pos, Quaternion.identity) as GameObject).GetComponent<T>();
    }

    public T GetItem<T>(T item) where T : MonoBehaviour
    {
        return (Instantiate(item.gameObject, Vector3.zero, Quaternion.identity) as GameObject).GetComponent<T>();
    }

    public Color GetColor(ItemId f)
    {
        return DataStructs.ColorsOfUI.FirstOrDefault(x => x.type == f).color;
    }
}