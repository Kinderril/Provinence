using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 0.002f;
    private float time = 0;
    public Vector3 trg;
    public Vector3 start;
    public Unit targetUnit;
    public Weapon weapon;
    private Action updateAction;
    public ParticleSystem TrailParticleSystem;
    public ParticleSystem HitParticleSystem;
    private List<Unit> AffecttedUnits = new List<Unit>();

    void Awake()
    {
        if (HitParticleSystem != null)
        {
            HitParticleSystem.Stop();
        }
    }
    public void Init(Vector3 target,Weapon weapon)
    {
        speed = weapon.Parameters.bulletSpeed;
        this.weapon = weapon;
        start = transform.position;
        var dir = target - start;
        trg = dir.normalized * weapon.Parameters.range + start;
        time = 0;
        updateAction = updateVector;
    }

    public void Init(Unit target, Weapon weapon)
    {
        speed = weapon.Parameters.bulletSpeed;
        targetUnit = target;
        start = transform.position;
        this.weapon = weapon;
        time = 0;
        updateAction = updateTargetUnit;
    }

    void OnTriggerEnter(Collider other)
    {
        var unit = other.GetComponent<Unit>();
        if (unit != null && unit != weapon.owner)
        {
            Hit(unit);
        }
        
    }

    private void Hit(Unit unit)
    {
        AffecttedUnits.Add(unit);
        unit.GetHit(this);
        //Debug.Log("Get Hit " + unit);
        if (weapon.PlayerItem != null)
        {
            switch (weapon.PlayerItem.specialAbilities)
            {
                case SpecialAbility.penetrating:
                    if (AffecttedUnits.Count > 3)
                    {
                        Death();
                    }
                    else
                    {
                        return;
                    }
                    break;
                case SpecialAbility.chain:
                    //TODO find another target
                    if (AffecttedUnits.Count > 3)
                    {
                        Death();
                    }
                    else
                    {
                        return;
                    }
                    break;
            }
        }
        Death();
    }

    private void Death()
    {
        Destroy(gameObject);
        if (HitParticleSystem != null)
        {
            HitParticleSystem.Play();
            Map.Instance.LeaveEffect(HitParticleSystem);
        }
        if (TrailParticleSystem != null)
        {
            TrailParticleSystem.Stop();
            Map.Instance.LeaveEffect(TrailParticleSystem);
        }
        
    }

    private void updateVector()
    {
        time += speed;
        transform.position = Vector3.Lerp(start, trg, time);
        if (time > 1)
        {
            Death();
        }
    }

    private void updateTargetUnit()
    {
        time += speed;
        Debug.Log("update vector " + time + "   " + transform.position + "   " + targetUnit  +  "   s:" + start);
        transform.position = Vector3.Lerp(start, targetUnit.transform.position, time);
        if (time > 1)
        {
            Death();
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

