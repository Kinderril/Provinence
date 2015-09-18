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
            Hit(unit);
        }
        
    }

    private void Hit(Unit unit)
    {
        unit.GetHit(this);
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

