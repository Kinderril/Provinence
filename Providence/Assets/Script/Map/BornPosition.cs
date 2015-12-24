using System;
using UnityEngine;
using System.Collections;
using System.Linq;

public class BornPosition : BaseBornPosition
{
    public BaseMonster monsterPrebaf;
    public int difficulty = 1;
    private Level level;
    private int totalUnits;
    private TimerManager.ITimer timer;
    private Action<Unit> OnEnemyDead;
    private Hero hero;
    private bool isReborned;

    public void Init(Map map, Action<Unit> OnEnemyDead,Level level, Hero hero)
    {
        this.hero = hero;
        isReborned = false;
        this.level = level;
        this.OnEnemyDead = OnEnemyDead;
        difficulty = difficulty + level.difficult - 1;
        base.Init(map);
        if (work)
        {
            BornMosters();
        }
    }

    private void BornMosters()
    {
        var p = transform.position;
        for (int i = 0; i < unitsCout; i++)
        {
            var b = new Vector3(p.x + UnityEngine.Random.Range(-radius, radius), p.y, p.z + UnityEngine.Random.Range(-radius, radius));
            BornEnemy(b, OnEnemyDead, hero);
        }
    }

    public override BornPositionType GetBornPositionType()
    {
        return BornPositionType.monster;
    }

    public void BornEnemy(Vector3 pos, Action<Unit> OnEnemyDead, Hero hero)
    {
        var monster = DataBaseController.Instance.mosntersLevel[difficulty].RandomElement();
        if (monster != null)
        {
            var unit = DataBaseController.Instance.GetItem(monster, pos);
            map.enemies.Add(unit);
            unit.Init(hero);
            unit.transform.SetParent(map.enemiesContainer);
            unit.OnDead += OnEnemyDead;
            unit.OnDead += OnDead;
            totalUnits++;
        }
        else
        {
            Debug.Log("can't dinf monster level " + difficulty);
        }
    }

    private void OnDead(Unit unit)
    {
        unit.OnDead -= OnDead;
        totalUnits--;
        if (totalUnits <= 0)
        {
            StartReborn();
        }
    }

    private void StartReborn()
    {
        if (!isReborned)
        {
            int sec = UnityEngine.Random.Range(15, 30);
            if (UnityEngine.Random.Range(0, 100) < 85)
            {
                timer = MainController.Instance.TimerManager.MakeTimer(TimeSpan.FromSeconds(sec));
                timer.OnTimer += OnReborn;
            }
            isReborned = true;
        }
    }

    private void OnReborn()
    {
        timer = null;
        if (work)
        {
            BornMosters();
        }
    }

    public void EndLevel()
    {
        if (timer != null)
        {
            timer.Stop();
        }
    }
}
