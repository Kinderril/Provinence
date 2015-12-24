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
    protected List<Unit> AffecttedUnits = new List<Unit>();
//    private UnitType ownerType;

    void Awake()
    {
        if (HitParticleSystem != null)
        {
            HitParticleSystem.Stop();
        }
    }
    public virtual void Init(Vector3 target,Weapon weapon)
    {
        speed = weapon.Parameters.bulletSpeed;
//        ownerType = weapon.owner.unitType;
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
        OnBulletHit(other);
//        var unit = other.GetComponent<Unit>();
//        if (unit != null && unit != weapon.owner)
//        {
//            Hit(unit);
//        }
    }

    protected virtual void OnBulletHit(Collider other)
    {
        Debug.LogError("DON'T USE THIS CLASSS");
    }

    protected virtual void Hit(Unit unit)
    {
        bool haveManyTargets = false;
        if (weapon != null && weapon.PlayerItem != null)
        {
            switch (weapon.PlayerItem.specialAbilities)
            {
                case SpecialAbility.penetrating:
                    haveManyTargets = true;
                    if (AffecttedUnits.Count > 3)
                    {
                        Death();
                    }
                    else
                    {
                        AffecttedUnits.Add(unit);
                        unit.GetHit(this);
                        return;
                    }
                    break;
                case SpecialAbility.chain:
                    //TODO find another target
                    haveManyTargets = true;
                    if (AffecttedUnits.Count > 3)
                    {
                        Death();
                    }
                    else
                    {
                        AffecttedUnits.Add(unit);
                        unit.GetHit(this);
                        return;
                    }
                    break;
            }
        }

        if (!haveManyTargets)
        {
            if (AffecttedUnits.Count < 1)
            {
                AffecttedUnits.Add(unit);
                unit.GetHit(this);
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

    protected void updateVector()
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

