using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class Hero : Unit
{
    void FixedUpdate()
    {
        if (action != null)
            action.Update();
    }

    public void GetChest(Chest chest)
    {
        Debug.Log("Get chest " + chest.items.Count );
        foreach (var item in chest.items)
        {
            MainController.Instance.level.AddItem(item.Key, item.Value);
        }

    }
}
