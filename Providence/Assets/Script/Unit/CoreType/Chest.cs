using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class Chest : MonoBehaviour
{
    public DictionaryOfItemAndInt items = new DictionaryOfItemAndInt();

    void OnTriggerEnter(Collider other)
    {
        var unit = other.GetComponent<Hero>();
        if (unit != null)
        {
            unit.GetChest(this);
            PlayOpen();
        }

    }

    private void PlayOpen()
    {

    }
}

