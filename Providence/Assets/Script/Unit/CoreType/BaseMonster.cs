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
    private const float aiDist = 110;
    public float mainHeroDist = 0;
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
        Action = null;

    }

    public override void GetHit(Bullet bullet)
    {
        var hp = CurHp;
        base.GetHit(bullet);
        hp -= CurHp;
        if (hp > 0)
        {
            var fn = DataBaseController.Instance.Pool.GetItemFromPool<FlyNumberWIthDependence>(PoolType.flyNumberInGame);
            fn.transform.SetParent(WindowManager.Instance.CurrentWindow.TopPanel.transform);
            //fn.transform.position = transform.position;
            fn.Init(transform,hp,Color.red, "-");
            //fn.transform.LookAt(MainController.Instance.MainCamera.transform);
        }

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

        mainHeroDist = (mainHero.transform.position - bornPosition).sqrMagnitude;
        
        if (mainHeroDist < aiDist)
        {
            Control.UpdateFromUnit();
            bool isTargetClose = (mainHeroDist < attackDist);
            switch (aiStatus)
            {
                case AIStatus.disable:
                    StartWalk();
                    break;
                case AIStatus.attack:
                    if ((mainHeroDist > runAwayDist))
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
            Action = null;
        }
    }

    private void StartWalk()
    {
        aiStatus = AIStatus.walk;
        int coef = 60;
        if (Action is MoveAction)
        {
            coef = 30;
        }
        var shalgo = UnityEngine.Random.Range(0, 100) < coef;
        if (shalgo)
        {
            var randPos = new Vector3(bornPosition.x + UnityEngine.Random.Range(-isHomeDist, isHomeDist),
                bornPosition.y,
                bornPosition.z + UnityEngine.Random.Range(-isHomeDist, isHomeDist));
            Action = new MoveAction(this, randPos, StartWalk);
        }
        else
        {
            Action = new StayAction(this, StartWalk);
        }
        
    }

    private void StartAttack()
    {
        aiStatus = AIStatus.attack;
        switch (AttackType)
        {
            case AttackType.hitAndRun:
                Action = new AttackHitAndRun(this, MainController.Instance.level.MainHero, StartAttack);
                break;
            case AttackType.distanceFight:
                Action = new AttackDistance(this, MainController.Instance.level.MainHero, StartAttack);
                break;
            case AttackType.closeCombat:
                Action = new AttackCloseCombat(this, MainController.Instance.level.MainHero, StartAttack);
                break;
        }
    }
    public bool IsInRadius(float rad)
    {
        return mainHeroDist < rad;
    }

    private void EndAttack()
    {
        aiStatus = AIStatus.returnHome;
        Action = new MoveAction(this, bornPosition, StartWalk);
    }
}

