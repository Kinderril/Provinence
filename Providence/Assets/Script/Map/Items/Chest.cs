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
    private Animator animator;

    void OnTriggerEnter(Collider other)
    {
        if (!isOpen)
        {
            var unit = other.GetComponent<Hero>();
            if (unit != null)
            {
                PlayOpen();
                if (SystemMultiplier != null)
                    SystemMultiplier.StartPlay();
            }
        }

    }

    public void Release()//Name from event
    {
        ReleaseItems();
    }
    private void ReleaseItems()
    {
        foreach (var item in items)
        {
            var mo = DataBaseController.Instance.GetItem<MapItem>(DataBaseController.Instance.MapItemPrefab,
                transform.position);
            mo.Init(item.Key, item.Value);
            mo.transform.SetParent(Map.Instance.miscContainer,true);
            mo.StartFly();
        }
    }

    private void PlayOpen()
    {
        isOpen = true;
        if (animator != null)
        {
            animator.SetBool("isOpen",true);
        }
    }

    public void Init()
    {
		float m_GroundCheckDistance = 9999f;
        animator = GetComponent<Animator>();
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, Vector3.down, out hitInfo, m_GroundCheckDistance))
        {
            var t = transform.position;
            float groundOffset = hitInfo.distance;
            transform.position = new Vector3(t.x,t.y - groundOffset,t.z);
        }
        transform.rotation = Quaternion.Euler(0, UnityEngine.Random.Range(-180,180), 0);
        items.Add(ItemId.money, 22);
    }
}

