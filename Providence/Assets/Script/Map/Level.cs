using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class Level
{
    private float powerLeft;
    private float maxpower = 120;
    public Action<float, float> OnLeft;

    public float PowerLeft
    {
        get { return powerLeft; }
        set
        {
            powerLeft = Mathf.Clamp(PowerLeft, -1, maxpower);
            powerLeft = value;
            if (OnLeft != null)
            {
                OnLeft(powerLeft, maxpower);
            }
        }
    }


    public void Update()
    {
        PowerLeft += Time.deltaTime;
    }
}

