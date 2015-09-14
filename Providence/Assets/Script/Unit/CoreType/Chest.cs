using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityStandardAssets.Effects;


public class Chest : MonoBehaviour
{
    public DictionaryOfItemAndInt items = new DictionaryOfItemAndInt();
    private bool isOpen = false;
    public ParticleSystemMultiplier SystemMultiplier;

    void Start()
    {
        items.Add(ItemId.money,25);
    }
    void OnTriggerEnter(Collider other)
    {
        if (!isOpen)
        {
            var unit = other.GetComponent<Hero>();
            if (unit != null)
            {
                unit.GetChest(this);
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

