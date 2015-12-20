using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class HeroBornPosition : BaseBornPosition
{

    void OnTriggerEnter(Collider other)
    {
        var unit = other.GetComponent<Hero>();
        if (unit != null)
        {

        }

    }
}

