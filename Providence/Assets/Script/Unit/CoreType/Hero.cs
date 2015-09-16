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

    public void GetItems(Dictionary<ItemId,int> chest)
    {
        foreach (var item in chest)
        {
            MainController.Instance.level.AddItem(item.Key, item.Value);
        }

    }
}
