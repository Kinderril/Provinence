using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Map : Singleton<Map>
{
    public List<BornPosition> appearPos;
    private Transform bornPositions;
    public Transform enemiesContainer;
    public List<Unit> enemies = new List<Unit>();
    public Transform effectsContainer;
    public Transform miscContainer;
    public Transform heroBornPositions;
    private Level level;
    public CameraFollow CameraFollow;

    public Hero Init(Level lvl)
    {
        level = lvl;
        List<Vector3> vector3s = new List<Vector3>();
        foreach (Transform v in heroBornPositions)
        {
            v.GetComponent<MeshRenderer>().enabled = false;
            vector3s.Add(v.position);
        }
        var hero = DataBaseController.Instance.GetItem(DataBaseController.Instance.prefabHero, vector3s.RandomElement());
        hero.Init();
        bornPositions = transform.Find("BornPos");
        enemiesContainer = transform.Find("Enemies");
        List<ChestBornPosition> chestPositions = new List<ChestBornPosition>();
        foreach (Transform bornPosition in bornPositions)
        {
            var bp = bornPosition.GetComponent<BornPosition>();
            if (bp != null)
            {
                appearPos.Add(bp);
                bp.Init(this, OnEnemyDead, lvl,hero);
            }
            else
            {
                var cbp = bornPosition.GetComponent<ChestBornPosition>();
                if (cbp != null)
                {
                    chestPositions.Add(cbp);
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
        return hero;
    }

    public void EndLevel()
    {
        foreach (var enemy in enemies)
        {
            enemy.DeInit();
        }
        foreach (Transform v in enemiesContainer)
        {
            Destroy(v.gameObject);
        }
        foreach (Transform v in miscContainer)
        {
            Destroy(v.gameObject);
        }
    }

    private void OnEnemyDead(Unit obj)
    {
        obj.OnDead -= OnEnemyDead;
        enemies.Remove(obj);
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

