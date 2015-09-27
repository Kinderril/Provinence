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
    public Animator animator;
    private bool canBeTaken = false;

    public void Init(ItemId type, int count)
    {
        Utils.SetRandomRotation(transform);
        this.type = type;
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
                }
                Destroy(gameObject);
            }
        }
    }
    void OnTriggerStay(Collider other)
    {
        OnTriggerEnter(other);
    }
    public void EndAnimation()
    {
        Debug.Log("EndAnimation");
        canBeTaken = true;//STUB
    }

    public void StartFly(Transform parentTransform)
    {
        transform.rotation = parentTransform.rotation;
        if (animator != null)
        {
            Debug.Log("StartAnimation");
            animator.SetBool("isOpen", true);
        }
        else
        {
            EndAnimation();
        }
    }
}

