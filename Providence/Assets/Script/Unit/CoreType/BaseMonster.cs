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
    public Hero mainHero;
    public int moneyCollect;
    public int energyadd = 4;
    private BaseAction attackBehaviour;
    public bool haveAction;


    public void Init(Hero hero)
    {
        mainHero = hero;
        Init();
    }

    public override void Init()
    {
        runAwayDist = attackDist * 1.4f;
        base.Init();
        Parameters.Parameters[ParamType.Speed] = GreatRandom.RandomizeValue(Parameters.Parameters[ParamType.Speed]);
        bornPosition = transform.position;
        Utils.GroundTransform(transform, 999f);
        //curWeapon.power = GreatRandom.RandomizeValue(curWeapon.power);
        moneyCollect = GreatRandom.RandomizeValue(moneyCollect);
        energyadd = GreatRandom.RandomizeValue(energyadd);
        aiStatus = AIStatus.disable;
    }

    protected override void Dead()
    {
        Control.Dead();
        MainController.Instance.level.AddItem(ItemId.energy, -energyadd);
        var mapItem2 = DataBaseController.Instance.GetItem<MapItem>(DataBaseController.Instance.MapItemPrefab, transform.position);
        mapItem2.Init(ItemId.money, moneyCollect);
        mapItem2.transform.SetParent(Map.Instance.miscContainer, true);
        mapItem2.StartFly(transform);
        base.Dead();
        action = null;

    }

    protected override void UpdateUnit()
    {
        if (!isDead)
        {
            CheckDistance();
            base.UpdateUnit();
        }
    }


    public void CheckDistance()
    {
        if (mainHero == null)
            return;

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
        int coef = 60;
        if (action is MoveAction)
        {
            coef = 30;
        }
        var shalgo = UnityEngine.Random.Range(0, 100) < coef;
        if (shalgo)
        {
            var randPos = new Vector3(bornPosition.x + UnityEngine.Random.Range(-isHomeDist, isHomeDist),
                bornPosition.y,
                bornPosition.z + UnityEngine.Random.Range(-isHomeDist, isHomeDist));
            action = new MoveAction(this, randPos, StartWalk);
        }
        else
        {
            action = new StayAction(this, StartWalk);
        }
        
    }

    private void StartAttack()
    {
        Debug.Log("Start attack " + this);
        aiStatus = AIStatus.attack;
        switch (AttackType)
        {
            case AttackType.hitAndRun:
                action = new AttackHitAndRun(this, MainController.Instance.level.MainHero, StartAttack);
                break;
            case AttackType.distanceFight:
                action = new AttackDistance(this, MainController.Instance.level.MainHero, StartAttack);
                break;
            case AttackType.closeCombat:
                action = new AttackCloseCombat(this, MainController.Instance.level.MainHero, StartAttack);
                break;
        }
    }

    private void EndAttack()
    {
        aiStatus = AIStatus.returnHome;
        action = new MoveAction(this, bornPosition, StartWalk);
    }
}

