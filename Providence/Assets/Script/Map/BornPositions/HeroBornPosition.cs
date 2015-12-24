using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class HeroBornPosition : BaseBornPosition
{
    private bool isPositionOpend;

    void OnTriggerEnter(Collider other)
    {
        if (!isPositionOpend)
        {
            var unit = other.GetComponent<Hero>();
            if (unit != null)
            {
                MainController.Instance.PlayerData.OpenBornPosition(ID);
                isPositionOpend = true;
            }
        }
    }
    public override BornPositionType GetBornPositionType()
    {
        return BornPositionType.chest;
    }
}

