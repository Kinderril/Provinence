using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Map : Singleton<Map>
{
    public List<BornPosition> appearPos;
    private Transform bornPositions;
    public Transform enemiesContainer;
    private TimerManager.ITimer timer;
    public List<Unit> enemies = new List<Unit>();
    private int maxEnemies = 15;
    public Transform effectsContainer;

    public void Init()
    {
        bornPositions = transform.Find("BornPos");
        enemiesContainer = transform.Find("Enemies");
        foreach (Transform bornPosition in bornPositions)
        {
            var bp = bornPosition.GetComponent<BornPosition>();
            appearPos.Add(bp);
            bp.Init(this, OnEnemyDead);
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

