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
    private const float moneyBonusCoef = 0.2f;
    private const float moneyBonusCoefTime = 0.8f;
    public float coefVisibility = 1f;
    private bool isCrouch = false;
    private bool isBush = false;
    public ParticleSystem OnGetItems;
    private float currenthBonus = 0f;
    private float currenthBonusTimeLeft = 0f;
    public Action<float> CurrentBonusUpdateX;
    public float moneyBonusFromItem = 0.0f;
    public float damageBonusFromItem = 0.0f;

    public float CurrenthBonus
    {
        get { return currenthBonus; }
        set
        {
            currenthBonus = value;
            if (CurrentBonusUpdateX != null)
            {
                CurrentBonusUpdateX( currenthBonus );
            }
        }
    }

    public override void Init()
    {
        base.Init();
        var playerData = MainController.Instance.PlayerData;
        foreach (ParamType v in Enum.GetValues(typeof(ParamType)))
        {
            Parameters.Parameters[v] = playerData.CalcParameter(v);
            Debug.Log("CAlc parameter: " + v + " : " + Parameters.Parameters[v]);
        }
        /*
        foreach (var wearedItem in MainController.Instance.PlayerData.GetAllWearedItems())
        {
            wearedItem.Activate(this);
        }*/
        Parameters.Parameters[ParamType.PPower] *= damageBonusFromItem + 1f;
        Parameters.Parameters[ParamType.MPower] *= damageBonusFromItem + 1f;
        OnGetItems.Stop(true);
        Utils.GroundTransform(transform);
    }



    void FixedUpdate()
    {
        if (currenthBonusTimeLeft > 0)
        {
            currenthBonusTimeLeft -= Time.deltaTime;
            if (currenthBonusTimeLeft < 0)
            {
                currenthBonus = 0;
            }
        }
        Control.UpdateFromUnit();
        if (Action != null)
            Action.Update();
    }

    public override void TryAttack(Vector3 target)
    {
        var dir = target - transform.position;
        (Control as HeroControl).SetDir(dir);
        base.TryAttack(target);
    }

    public void GetItems(ItemId type, int count)
    {
        count =(int)(count * (currenthBonus + 1));

        CurrenthBonus += moneyBonusCoef;
        currenthBonusTimeLeft += moneyBonusCoefTime;

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
