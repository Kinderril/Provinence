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
    public float power = 1;
    public Bullet bullet;
    public Unit owner;
    public bool isHoming = false;
    private ParticleSystem pSystemOnShot;

    public void Init(Unit owner)
    {
        pSystemOnShot = GetComponentInChildren<ParticleSystem>();
        if (pSystemOnShot != null)
        {
            pSystemOnShot.Stop();
        }
        this.owner = owner;
    }

    public bool CanShoot()
    {
        return Time.time > nexAttackTime;
    }

    public void DoShoot(Vector3 v)
    {
        v = new Vector3(v.x,transform.position.y, v.z);
        var b = CanShoot();
        if (b)
        {
            if (isHoming)
            {
                Unit potentialTarget = Map.Instance.FindClosesEnemy(v);
                if (potentialTarget != null && (owner.transform.position - potentialTarget.transform.position).sqrMagnitude < 4)
                {
                    nexAttackTime = Time.time + attackCooldown;
                    Bullet bullet1 = Instantiate(bullet.gameObject).GetComponent<Bullet>();
                    bullet1.transform.position = owner.transform.position;
                    bullet1.Init(potentialTarget, this);
                }
            }
            else
            {
                nexAttackTime = Time.time + attackCooldown;
                Bullet bullet1 = Instantiate(bullet.gameObject).GetComponent<Bullet>();
                bullet1.transform.position = transform.position;
                bullet1.Init(v, this);
            }
        }
        if (pSystemOnShot != null)
        {
            pSystemOnShot.Play();
        }
    }
}

