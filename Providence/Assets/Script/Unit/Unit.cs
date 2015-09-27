using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.Remoting.Channels;
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
    public Transform weaponsContainer;
    public event Action<Unit> OnDead;
    public event Action<float, float,float> OnGetHit;
    protected bool isDead = false;
    public UnitParameters Parameters;
    private AnimationController animationController;

    
    public virtual void Init()
    {
        Parameters = Parameters.Copy();
        if (Control == null)
            Control = GetComponent<Character>();
        animationController = GetComponentInChildren<AnimationController>();
        if(animationController == null)
            Debug.LogError("NO ANImator Controller");
        curHp = Parameters.MaxHp;
        List<Weapon> weapons = new List<Weapon>();
        foreach (var inventoryWeapon in InventoryWeapons)
        {
            var w = GameObject.Instantiate(inventoryWeapon.gameObject).GetComponent<Weapon>();
            weapons.Add(w);
            w.Init(this);
            if (weaponsContainer != null)
                w.transform.SetParent(weaponsContainer,true);
            else
                w.transform.SetParent(transform, true);
            w.transform.localPosition = Vector3.zero;
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
        Control.PlayAttack();
        animationController.StartPlayAttack(() =>
        {
            curWeapon.DoShoot(target);
        });
        ;
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
    public void MoveToDirection(Vector3 dir)
    {
        Control.MoveToDir(dir * Parameters.Speed);
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

    private float calcResist(float curResist)
    {
        return 1 - curResist/(100 + curResist);
    }

    public void GetHit(Bullet bullet)
    {
        float power = bullet.weapon.Parameters.power;
        switch (bullet.weapon.Parameters.type)
        {
            case WeaponType.magic:
                power *= calcResist(Parameters.magicResist);
                break;
            case WeaponType.physics:
                power *= calcResist(Parameters.physicResist);
                break;
        }
        Debug.Log("Get hit:" + bullet.weapon.Parameters.power + " => " + power);

        curHp -= power;
        if (OnGetHit != null)
        {
            OnGetHit(curHp, Parameters.MaxHp, power);
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

}
