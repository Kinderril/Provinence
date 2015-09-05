using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 0.00001f;
    private float time = 0;
    public Vector3 trg;
    public Vector3 start;
    private Weapon weapon;

    public void Init(Vector3 trg,Weapon weapon)
    {
        this.weapon = weapon;
        this.trg = trg.normalized * 5;
        start = transform.position;
        time = 0;
    }
    void OnTriggerEnter(Collider other)
    {
        var unit = other.GetComponent<Unit>();
        Debug.Log(" Trigger " + unit + "   " + weapon.owner);
        if (unit != null && unit != weapon.owner)
        {
            unit.GetHit(this);
            Destroy(gameObject);
        }
        
    }
    void FixedUpdate()
    {
        time += speed;
        transform.position = Vector3.Lerp(start, trg, time);
        if (time > 1)
        {
            Destroy(gameObject);
        }
    }
}

