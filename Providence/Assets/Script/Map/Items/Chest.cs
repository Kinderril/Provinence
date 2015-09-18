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
    

    void OnTriggerEnter(Collider other)
    {
        if (!isOpen)
        {
            var unit = other.GetComponent<Hero>();
            if (unit != null)
            {
                foreach (var item in items)
                {
                    var mo = DataBaseController.Instance.GetItem<MapItem>(DataBaseController.Instance.MapItemPrefab,
                        transform.position);
                    mo.Init(item.Key, item.Value);
                    mo.StartFly();
                }
                PlayOpen();
                SystemMultiplier.StartPlay();
            }
        }

    }

    private void PlayOpen()
    {
        isOpen = true;
    }

    public void Init()
    {

    }
}

