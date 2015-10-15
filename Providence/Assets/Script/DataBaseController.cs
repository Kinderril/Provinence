using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public enum ItemId
{
    money,
    crystal,
    energy,
    health
}

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

public class DataBaseController : Singleton<DataBaseController>
{
    public List<Weapon> Weapons;
    public List<BaseMonster> Monsters;
    public List<IShopExecute> allShopElements; 
    public GameObject debugCube;
    public MapItem MapItemPrefab;
    public Chest chestPrefab;
    public Hero prefabHero;
    public FlyingNumbers FlyingNumber;
    public ColorUI[] ColorsOfUI;
    public Dictionary<int,List<BaseMonster>> mosntersLevel = new Dictionary<int, List<BaseMonster>>();
    public int maxLevel = 20;
    public int[] costParameterByLvl;
    public MainParameterImage[] MainParametersImages;
    public ParameterImage[] ParametersImages;
    public ItemImage[] ItemImage;
    public SlotImage[] SlotImage;

    void Awake()
    {
        for (int i = 0; i < maxLevel; i++)
        {
            mosntersLevel.Add(i,new List<BaseMonster>());
        }
        foreach (var baseMonster in Monsters)
        {
            mosntersLevel[baseMonster.Parameters.Level].Add(baseMonster);
        }
    }

    public string MainParameterIcon(MainParam mp)
    {
        return MainParametersImages.FirstOrDefault(x => x.type == mp).path;
    }
    public string SlotIcon(Slot mp)
    {
        return SlotImage.FirstOrDefault(x => x.type == mp).path;
    }
    public string ItemIcon(ItemId itemId)
    {
        return ItemImage.FirstOrDefault(x => x.type == itemId).path;
    }

    public string ParameterIcon(ParamType mp)
    {
        return ParametersImages.FirstOrDefault(x => x.type == mp).path;
    }
    public T GetItem<T>(T item,Vector3 pos) where T : MonoBehaviour
    {
        return (GameObject.Instantiate(item.gameObject, pos, Quaternion.identity) as GameObject).GetComponent<T>();
    }
    public T GetItem<T>(T item) where T : MonoBehaviour
    {
        return (GameObject.Instantiate(item.gameObject, Vector3.zero, Quaternion.identity) as GameObject).GetComponent<T>();
    }

    public Color GetColor(ItemId f)
    {
        return ColorsOfUI.FirstOrDefault(x => x.type == f).color;
    }

}

