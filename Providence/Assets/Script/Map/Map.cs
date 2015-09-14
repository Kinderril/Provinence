using System;
using System.Collections.Generic;
using UnityEngine;


public class Map : Singleton<Map>
{
    public List<Vector3> appearPos;
    private Transform bornPositions;
    private Transform enemiesContainer;
    private TimerManager.ITimer timer;
    private List<Unit> enemies = new List<Unit>();
    private int maxEnemies = 15;
    public Transform effectsContainer;

    public void Init()
    {
        bornPositions = transform.Find("BornPos");
        enemiesContainer = transform.Find("Enemies");
        foreach (Transform bornPosition in bornPositions)
        {
            appearPos.Add(bornPosition.position);
        }
        timer = MainController.Instance.TimerManager.MakeTimer(TimeSpan.FromSeconds(1), true);
        timer.OnTimer += OnTimerSpawn;
    }

    private void OnTimerSpawn()
    {
        if (enemies.Count < maxEnemies)
        {
            BornEnemy();
        }
    }

    public void BornEnemy()
    {
        var monster = DataBaseController.Instance.Monsters.RandomElement();
        var pos = appearPos.RandomElement();
        var m = (GameObject.Instantiate(monster.gameObject, pos, Quaternion.identity) as GameObject).GetComponent<Unit>();
        enemies.Add(m);
        m.Init();
        m.transform.SetParent(enemiesContainer);
        m.OnDead += OnEnemyDead;
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
}

