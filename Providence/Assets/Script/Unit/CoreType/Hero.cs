﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class Hero : Unit
{
    //С кустами осторожнее не ставить 2 куста рядом! иначе баги будут - если плавно перейти из куста а в куст б то извиза в кусте б не будет

    private const float crouchBonus = 0.7f;
    private const float bushBonus = 0.7f;
    public float coefVisibility = 1f;
    private bool isCrouch = false;
    private bool isBush = false;
    public ParticleSystem OnGetItems;

    public override void Init()
    {
        base.Init();
        OnGetItems.Stop(true);
        Utils.GroundTransform(transform);
    }

    void FixedUpdate()
    {
        if (action != null)
            action.Update();
    }

    public override void TryAttack(Vector3 target)
    {
        var dir = target - transform.position;
        (Control as HeroCharacter).SetDir(dir);
        base.TryAttack(target);
    }

    public void GetItems(ItemId type, int count)
    {
        OnGetItems.Play();
        MainController.Instance.level.AddItem(type, count);
    }

    public void GetItems(Dictionary<ItemId,int> chest)
    {
        foreach (var item in chest)
        {
            GetItems(item.Key, item.Value);
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        var bush = other.GetComponent<Bush>();
        if (bush != null)
        {
            if (!isBush)
            {
                isBush = true;
                coefVisibility *= bushBonus;
            }
        }

    }
    void OnTriggerExit(Collider other)
    {
        var bush = other.GetComponent<Bush>();
        if (bush != null)
        {
            if (isBush)
            {
                isBush = false;
                coefVisibility *= 1/bushBonus;
            }
        }
    }

    public void DoCrouch()
    {
        if (!isCrouch)
        {
            isCrouch = true;
            coefVisibility *= crouchBonus;
        }
        else
        {
            isCrouch = false;
            coefVisibility *= 1f/crouchBonus;
        }
    }

    protected override void Dead()
    {
        base.Dead();
        MainController.Instance.EndLevel();
    }

    public void ChangeWeaponTo(Weapon weap)
    {

    }
}
