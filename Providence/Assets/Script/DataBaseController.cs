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

public class DataBaseController : Singleton<DataBaseController>
{
    public List<Weapon> Weapons;
    public List<Unit> Monsters;
    public GameObject debugCube;
    public MapItem MapItemPrefab;
    public Chest chestPrefab;
    public Hero prefabHero;
    public FlyingNumbers FlyingNumber;
    public ColorUI[] ColorsOfUI;

    public T GetItem<T>(T item,Vector3 pos) where T : MonoBehaviour
    {
        return (GameObject.Instantiate(item.gameObject, pos, Quaternion.identity) as GameObject).GetComponent<T>();
    }

    public Color GetColor(ItemId f)
    {
        return ColorsOfUI.FirstOrDefault(x => x.type == f).color;
    }
}

