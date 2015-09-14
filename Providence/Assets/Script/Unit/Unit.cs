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
    public float maxHp = 15f;
    public float curHp;
    public Weapon curWeapon;
    public List<Weapon> InventoryWeapons;
    public Character Control;
    protected BaseAction action;
    public UnitType unitType;
    private Transform weaponsContainer;
    public event Action<Unit> OnDead;
    public event Action<float, float> OnGetHit;
    public float speed = 6f;

    void Start()
    {
        Control = GetComponent<Character>();
    }
    
    public virtual void Init()
    {
        weaponsContainer = transform.Find("Weapons");
        curHp = maxHp;
        List<Weapon> weapons = new List<Weapon>();
        foreach (var inventoryWeapon in InventoryWeapons)
        {
            var w = GameObject.Instantiate(inventoryWeapon.gameObject).GetComponent<Weapon>();
            weapons.Add(w);
            w.Init(this);
            w.transform.SetParent(weaponsContainer);
        }
        Control.SetSpped(speed);
        InventoryWeapons = weapons;
        curWeapon = InventoryWeapons[0];
    }
    
    public void TryAttack(Vector3 target)
    {
        curWeapon.DoShoot(target);
    }

    void FixedUpdate()
    {
        if (action != null)
            action.Update();
    }

    public void MoveTo(Vector3 vector3)
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
        curHp -= bullet.weapon.power;
        if (OnGetHit != null)
        {
            OnGetHit(curHp, maxHp);
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
        Destroy(gameObject);
    }

}
