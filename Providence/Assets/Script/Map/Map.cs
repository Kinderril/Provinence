﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Map : Singleton<Map>
{
    public List<BornPosition> appearPos;
    public List<BossBornPosition> BossAppearPos;
    private Transform bornPositions;
    public Transform enemiesContainer;
    public List<Unit> enemies = new List<Unit>();
    private BossSpawner bossSpawner;
    public Transform effectsContainer;
    public Transform miscContainer;
    public Transform heroBornPositions;
    private Level level;
    public CameraFollow CameraFollow;
    private BossUnit boss;

    public Hero Init(Level lvl,int heroBornPositionIndex)
    {
        level = lvl;
        var hero = DataBaseController.Instance.GetItem(DataBaseController.Instance.prefabHero, GetHeroBoenPos(heroBornPositionIndex));
        hero.Init();
        bornPositions = transform.Find("BornPos");
        enemiesContainer = transform.Find("Enemies");
        appearPos = new List<BornPosition>();
        BossAppearPos = new List<BossBornPosition>();
        List<ChestBornPosition> chestPositions = new List<ChestBornPosition>();
        foreach (Transform bornPosition in bornPositions)
        {
            var bp = bornPosition.GetComponent<BaseBornPosition>();
            if (bp != null)
            {
                switch (bp.GetBornPositionType())
                {
                    case BornPositionType.chest:
                        var cBP = (bp as ChestBornPosition);
                        chestPositions.Add(cBP);
                        break;
                    case BornPositionType.monster:
                        var mBP = (bp as BornPosition);
                        appearPos.Add(mBP);
                        mBP.Init(this, OnEnemyDead, lvl, hero);
                        break;
                    case BornPositionType.boss:
                        var bBP = (bp as BossBornPosition);
                        BossAppearPos.Add(bBP);
                        bBP.Init(this);
                        break;
                }
            }
        }
        var rnd = chestPositions.RandomElement();
        rnd.SetCrystal();
        foreach (var chestBornPosition in chestPositions)
        {
            chestBornPosition.Init(this,lvl);
            
        }
        CameraFollow.Init(hero.transform);
        bossSpawner = new BossSpawner(enemies.Count,OnSpawnBoss);
        return hero;
    }

    private Vector3 GetHeroBoenPos(int index)
    {
        Vector3 vector3s = Vector3.zero;
        foreach (Transform v in heroBornPositions)
        {
            v.GetComponent<MeshRenderer>().enabled = false;
            var heroBP = v.GetComponent<HeroBornPosition>();
            if (heroBP.ID == index)
            {
                vector3s = v.position;
//                break;
            }
//            vector3s = v.position;
        }
        return vector3s;
    }

    private void OnSpawnBoss()
    {
        var pos = BossAppearPos.RandomElement().transform.position;
        var bossPrefab = DataBaseController.Instance.BossUnits.FirstOrDefault(x => x.Parameters.Level == level.difficult);
        if (bossPrefab != null)
        {
            boss = DataBaseController.Instance.GetItem<BossUnit>(bossPrefab, pos);
            boss.Init(MainController.Instance.level.MainHero);
            boss.transform.SetParent(enemiesContainer);
            if (level.OnBossAppear != null)
            {
                level.OnBossAppear(boss);
            }
        }
        else
        {
            Debug.LogError("Can't find bossPrefab");
        }
    }

    public void EndLevel()
    {
        foreach (var enemy in enemies)
        {
            enemy.DeInit();
        }
        Utils.ClearTransform(enemiesContainer);
        Utils.ClearTransform(miscContainer);
        foreach (var bornPosition in appearPos)
        {
            bornPosition.EndLevel();
        }
        if (boss != null)
        {
            Destroy(boss.gameObject);
        }
    }

    private void OnEnemyDead(Unit obj)
    {
        obj.OnDead -= OnEnemyDead;
        enemies.Remove(obj);
        bossSpawner.EnemieDead();
    }

    public List<BaseMonster> GetEnimiesInRadius(float rad)
    {
        List < BaseMonster > list = new List<BaseMonster>();
        foreach (var enemy in enemies.Where(x => x is BaseMonster && (x as BaseMonster).IsInRadius(rad)))
        {
            list.Add(enemy as BaseMonster);
        }
        return list;
    } 

    public Unit FindClosesEnemy(Vector3 v)
    {
        float curDist = 999999;
        Unit unit = null;
        foreach (var enemy in enemies)
        {
            var pDist = (enemy.transform.position - v).sqrMagnitude;
            if (pDist < curDist)
            {
                curDist = pDist;
                unit = enemy;
            }
        }
        return unit;
    }

    public void LeaveEffect(ParticleSystem ps)
    {
        ps.transform.SetParent(effectsContainer, true);
        StartCoroutine(DestroyPS(ps));
    }
    public IEnumerator DestroyPS(ParticleSystem ps)
    {
        yield return new WaitForSeconds(4);
        Destroy(ps.gameObject);
    }

}

