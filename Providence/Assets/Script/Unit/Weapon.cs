using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class Weapon : MonoBehaviour
{
    private float nexAttackTime;
    public float attackCooldown;
    public float range;
    public int power;
    public Bullet bullet;
    public Unit owner;

    public void Init(Unit owner)
    {
        this.owner = owner;
    }

    public bool CanShoot()
    {
        return Time.time > nexAttackTime;
    }

    public void DoShoot(Vector3 v)
    {
        v = new Vector3(v.x,owner.transform.position.y,v.z);
        var b = CanShoot();
        if (b)
        {
            nexAttackTime = Time.time + attackCooldown;
            Bullet bullet1 = Instantiate(bullet.gameObject).GetComponent<Bullet>();
            bullet1.transform.position = owner.transform.position;
            Debug.Log("DoShoot " + v + "  can:" + b + "   " + bullet1 + "from:" + bullet1.transform.position + " to:" + v);
            bullet1.Init(v,this);
        }
    }
}

