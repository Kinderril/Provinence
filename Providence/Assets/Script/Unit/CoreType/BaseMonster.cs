using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public enum AIStatus
{
    attack,
    returnHome,
    walk,
    disable,
}

public class BaseMonster : Unit
{
    public AttackType AttackType;
    private const float isHomeDist = 2;
    public float attackDist = 40;
    private float runAwayDist = 0;
    private float aiDist = 180;
    public float targetDist = 0;
    public Vector3 bornPosition;
    public AIStatus aiStatus;
    private Unit mainHero;
    public int moneyCollect;
    public float energyadd = 4f;
    private BaseAction attackBehaviour;
    public bool haveAction;

    protected override void Dead()
    {
        base.Dead();
        MainController.Instance.level.PowerLeft -= energyadd;
    }

    public override void Init()
    {
        runAwayDist = attackDist * 1.4f;
        speed = GreatRandom.RandomizeValue(speed);
        base.Init();
        mainHero = MainController.Instance.MainHero;
        bornPosition = transform.position;
        curWeapon.power = GreatRandom.RandomizeValue(curWeapon.power);
        moneyCollect = GreatRandom.RandomizeValue(moneyCollect);
        energyadd = GreatRandom.RandomizeValue(energyadd);
        aiStatus = AIStatus.disable;
    }

    void FixedUpdate()
    {
        CheckDistance();
        if ( action != null)
            action.Update();
    }


    public void CheckDistance()
    {
        targetDist = (mainHero.transform.position - bornPosition).sqrMagnitude;
        if (targetDist < aiDist)
        {
            bool isTargetClose = (targetDist < attackDist);
            switch (aiStatus)
            {
                case AIStatus.disable:
                    StartWalk();
                    break;
                case AIStatus.attack:
                    if ((targetDist > runAwayDist))
                    {
                        EndAttack();
                    }
                    break;
                case AIStatus.returnHome:
                    if (isTargetClose)
                    {
                        StartAttack();
                    }
                    else
                    {
                        var isHome = (transform.position - bornPosition).sqrMagnitude < isHomeDist;
                        if (isHome)
                        {
                            StartWalk();
                        }
                    }
                    break;
                case AIStatus.walk:
                    if (isTargetClose)
                    {
                        StartAttack();
                    }
                    break;
            }
        }
        else
        {
            aiStatus = AIStatus.disable;
            action = null;
        }
    }

    private void StartWalk()
    {
        aiStatus = AIStatus.walk;
        var randPos = new Vector3(bornPosition.x + UnityEngine.Random.Range(-isHomeDist, isHomeDist), bornPosition.y,
            bornPosition.z + UnityEngine.Random.Range(-isHomeDist, isHomeDist));
        action = new MoveAction(this, randPos, StartWalk);
    }

    private void StartAttack()
    {
        Debug.Log("Start attack " + this);
        aiStatus = AIStatus.attack;
        switch (AttackType)
        {
            case AttackType.hitAndRun:
                action = new AttackHitAndRun(this, MainController.Instance.MainHero, StartAttack);
                break;
            case AttackType.distanceFight:
                action = new AttackDistance(this, MainController.Instance.MainHero, StartAttack);
                break;
            case AttackType.closeCombat:
                action = new AttackCloseCombat(this, MainController.Instance.MainHero, StartAttack);
                break;
        }
    }

    private void EndAttack()
    {
        aiStatus = AIStatus.returnHome;
        action = new MoveAction(this, bornPosition, StartWalk);
    }
}

