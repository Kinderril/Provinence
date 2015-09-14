using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public enum ItemId
{
    money,
    crystal,
}

public class DataBaseController : Singleton<DataBaseController>
{
    public List<Weapon> Weapons;
    public List<Unit> Monsters;
    public GameObject debugCube;
    
}

