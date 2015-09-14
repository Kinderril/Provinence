using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 0.000001f;
    private float time = 0;
    public Vector3 trg;
    public Vector3 start;
    public Unit targetUnit;
    public Weapon weapon;
    private Action updateAction;

    public void Init(Vector3 target,Weapon weapon)
    {
        this.weapon = weapon;
        start = transform.position;
        var dir = target - start;
        trg = dir.normalized * weapon.range + start;
        Debug.Log(" Target final =  " + trg + " dir:" + dir);
        time = 0;
        updateAction = updateVector;
    }

    public void Init(Unit target, Weapon weapon)
    {
        targetUnit = target;
        this.weapon = weapon;
        time = 0;
        updateAction = updateTargetUnit;
    }

    void OnTriggerEnter(Collider other)
    {
        var unit = other.GetComponent<Unit>();
        if (unit != null && unit != weapon.owner)
        {
            unit.GetHit(this);
            Destroy(gameObject);
        }
        
    }

    private void updateVector()
    {
        time += speed;
        transform.position = Vector3.Lerp(start, trg, time);
        if (time > 1)
        {
            Destroy(gameObject);
        }
    }

    private void updateTargetUnit()
    {
        time += speed;
        transform.position = Vector3.Lerp(start, targetUnit.transform.position, time);
        if (time > 1)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        if (updateAction != null)
        {
            updateAction();
        }
    }
}

