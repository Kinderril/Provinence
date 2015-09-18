using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.ThirdPerson;

public enum UnitType
{
    hero,
    monster
}
public class Unit : MonoBehaviour
{
    public float curHp;
    public Weapon curWeapon;
    public List<Weapon> InventoryWeapons;
    public Character Control;
    protected BaseAction action;
    public UnitType unitType;
    private Transform weaponsContainer;
    public event Action<Unit> OnDead;
    public event Action<float, float> OnGetHit;
    protected bool isDead = false;
    public UnitParameters Parameters;

    
    public virtual void Init()
    {
        Parameters = Parameters.Copy();
        Control = GetComponent<Character>();
        weaponsContainer = transform.Find("Weapons");
        curHp = Parameters.MaxHp;
        List<Weapon> weapons = new List<Weapon>();
        foreach (var inventoryWeapon in InventoryWeapons)
        {
            var w = GameObject.Instantiate(inventoryWeapon.gameObject).GetComponent<Weapon>();
            weapons.Add(w);
            w.Init(this);
            if (weaponsContainer != null)
                w.transform.SetParent(weaponsContainer,false);
            else
                w.transform.SetParent(transform, false);
        }
        Control.SetSpped(Parameters.Speed);
        InventoryWeapons = weapons;
        if (InventoryWeapons.Count == 0)
        {
            Debug.LogWarning("NO WEAPON!!! " + gameObject.name);
            return;
        }
        curWeapon = InventoryWeapons[0];
    }
    
    public void TryAttack(Vector3 target)
    {
        curWeapon.DoShoot(target);
    }

    void FixedUpdate()
    {
        if (!isDead)
        {
            UpdateUnit();
        }
    }

    protected virtual void UpdateUnit()
    {
        if (action != null)
            action.Update();
    }

    public void MoveToPosition(Vector3 vector3)
    {
        Debug.Log("MOve to: " + vector3);
        if (action != null)
        {
            action.End();
        }
        var moveAction = new MoveAction(this,vector3, () =>
        {
            action = null;
        });
        action = moveAction;
    }

    public void GetHit(Bullet bullet)
    {
        curHp -= bullet.weapon.Parameters.power;
        if (OnGetHit != null)
        {
            OnGetHit(curHp, Parameters.MaxHp);
        }
        if (curHp <= 0)
        {
            Dead();
        }
    }

    protected virtual void Dead()
    {
        if (OnDead != null)
        {
            OnDead(this);
        }
        var collider = GetComponent<Collider>();
        if (collider != null)
            collider.enabled = false;
        var rigbody = GetComponent<Rigidbody>();
        if (rigbody != null)
            rigbody.isKinematic = true;
        isDead = true;
        Control.SetDeath();
        StartCoroutine(PLayDeath());
    }

    private IEnumerator PLayDeath()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

    public void MoveToDirection(Vector3 dir)
    {
        Control.MoveToDir(dir* Parameters.Speed);
    }
}
