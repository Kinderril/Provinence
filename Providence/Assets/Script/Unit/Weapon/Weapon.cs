using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class Weapon : MonoBehaviour
{
    private float nexAttackTime;
    public Bullet bullet;
    public Unit owner;
    public ParticleSystem pSystemOnShot;
    public WeaponParameters Parameters;
    public List<SpecialAbility> Abilities;

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

    public void SetNextTimeShoot()
    {
        nexAttackTime = Time.time + Parameters.attackCooldown;
    }

    public void DoShoot(Vector3 v)
    {
        v = new Vector3(v.x,transform.position.y, v.z);
            
        if (Parameters.isHoming)
        {
            Unit potentialTarget = null;
            if (owner is Hero)
            {
                potentialTarget = Map.Instance.FindClosesEnemy(v);
            }
            else
            {
                potentialTarget = MainController.Instance.level.MainHero;
            }
            var dist = (owner.transform.position - potentialTarget.transform.position).sqrMagnitude;
            if (potentialTarget != null && dist < 30)
            {
                Bullet bullet1 = Instantiate(bullet.gameObject).GetComponent<Bullet>();
                bullet1.transform.position = owner.transform.position;
                bullet1.Init(potentialTarget, this);
            }
            else
            {
                Debug.Log("Homing dist is LONG " + dist + " > 30");
            }
        }
        else
        {
            Bullet bullet1 = Instantiate(bullet.gameObject).GetComponent<Bullet>();
            bullet1.transform.position = transform.position;
            bullet1.Init(v, this);
        }
        if (pSystemOnShot != null)
        {
            pSystemOnShot.Play();
        }
    }
}

