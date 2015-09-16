using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityStandardAssets.Effects;


public class Chest : MonoBehaviour
{
    public Dictionary<ItemId, int> items = new Dictionary<ItemId, int>();
    private bool isOpen = false;
    public ParticleSystemMultiplier SystemMultiplier;

    void Start()
    {
        items.Add(ItemId.money,25);//STUB
    }
    void OnTriggerEnter(Collider other)
    {
        if (!isOpen)
        {
            var unit = other.GetComponent<Hero>();
            if (unit != null)
            {
                unit.GetItems(items);
                PlayOpen();
                SystemMultiplier.StartPlay();
            }
        }

    }

    private void PlayOpen()
    {
        isOpen = true;
    }
}

