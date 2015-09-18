using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class MapItem : MonoBehaviour
{
    private ItemId type;
    private int count;
    public ParticleSystem OpenEffect;
    private bool canBeTaken = false;

    public void Init(ItemId type, int count)
    {
        this.count = count;
    }
    void OnTriggerEnter(Collider other)
    {
        if (canBeTaken)
        {
            var unit = other.GetComponent<Hero>();
            if (unit != null)
            {
                unit.GetItems(type,count);
                if (OpenEffect != null)
                {
                    OpenEffect.Play();
                    Map.Instance.LeaveEffect(OpenEffect);
                    Destroy(gameObject);
                }
            }
        }

    }

    public void StartFly()
    {

    }
}

