using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class ChestBornPosition : BaseBornPosition
{
    private bool withCrystal;
    public override void Init(Map map)
    {
        base.Init(map);
        BornChest();
    }


    public void BornChest()
    {
        var p = transform.position;
        var b = new Vector3(p.x + UnityEngine.Random.Range(-radius, radius), p.y, p.z + UnityEngine.Random.Range(-radius, radius));
        var chest = DataBaseController.Instance.GetItem<Chest>(DataBaseController.Instance.chestPrefab, b);
        chest.Init(withCrystal);
        chest.transform.SetParent(map.miscContainer,true);
    }

    public void SetCrystal()
    {
        withCrystal = true;
    }
}

