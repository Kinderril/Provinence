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
    private const int isHomeDist = 10;
    private int attackDist = 90;
    private int targetDist = 0;
    private Vector3 bornPosition;
    private AIStatus aiStatus = AIStatus.walk;

    protected override void Dead()
    {
        base.Dead();
        MainController.Instance.level.PowerLeft -= 1f;
    }


    public void CheckDistance()
    {
        bool isTargetClose = (targetDist >= attackDist);
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

    private void StartWalk()
    {
        aiStatus = AIStatus.walk;
        var randPos = new Vector3(bornPosition.x + UnityEngine.Random.Range(-isHomeDist, isHomeDist), bornPosition.y,
            bornPosition.z + UnityEngine.Random.Range(-isHomeDist, isHomeDist));
        action = new MoveAction(this, randPos, StartWalk);
    }

    private void StartAttack()
    {
        aiStatus = AIStatus.attack;
        action = new AttackAction(this, MainController.Instance.MainHero, StartAttack);
    }

    private void EndAttack()
    {
        aiStatus = AIStatus.returnHome;
        action = new MoveAction(this, bornPosition, StartWalk);

    }
}

