using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public enum AIStatus
{
    attack,
    returnHome,
    walk
}

public class BaseMonster : Unit
{
    public AttackType AttackType;
    private const float isHomeDist = 2;
    private float attackDist = 40;
    private float aiDist = 140;
    public float targetDist = 0;
    public Vector3 bornPosition;
    public AIStatus aiStatus = AIStatus.returnHome;
    public float attackPeriod;
    private Unit mainHero;
    private BaseAction attackBehaviour;

    protected override void Dead()
    {
        base.Dead();
        MainController.Instance.level.PowerLeft -= 1f;

    }

    public override void Init()
    {
        base.Init();
        mainHero = MainController.Instance.MainHero;
        bornPosition = transform.position;
    }

    void FixedUpdate()
    {
        CheckDistance();
        if (action != null)
            action.Update();
    }


    public void CheckDistance()
    {
        targetDist = (mainHero.transform.position - bornPosition).sqrMagnitude;
        if (targetDist < aiDist)
        {
            Debug.Log("close => calc");
            bool isTargetClose = (targetDist < attackDist);
            switch (aiStatus)
            {
                case AIStatus.attack:
                    if (!isTargetClose)
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

