using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class MapItem : MonoBehaviour
{
    private Dictionary<ItemId, int> count; 
    public ParticleSystem OpenEffect;

    public void Init(Dictionary<ItemId, int> count)
    {
        this.count = count;
    }
    void OnTriggerEnter(Collider other)
    {
        var unit = other.GetComponent<Hero>();
        if (unit != null)
        {
            unit.GetItems(count);
            if (OpenEffect != null)
            {
                OpenEffect.Play();
                Map.Instance.LeaveEffect(OpenEffect);
                Destroy(gameObject);
            }
        }

    }
}

