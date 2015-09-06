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
    private Weapon weapon;

    public void Init(Vector3 target,Weapon weapon)
    {
        this.weapon = weapon;
        //trg = target.normalized * 5;

        start = transform.position;
        var dir = target - start;
        trg = dir.normalized * 5 + start;
        Debug.Log(" Target final =  " + trg + " dir:" + dir);
        time = 0;
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

