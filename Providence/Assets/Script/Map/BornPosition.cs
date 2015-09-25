using System;
using UnityEngine;
using System.Collections;

public class BornPosition : BaseBornPosition
{
    public BaseMonster monsterPrebaf;
    public int difficulty = 1;
    private Level level;

    public void Init(Map map, Action<Unit> OnEnemyDead,Level level, Hero hero)
    {
        this.level = level;
        difficulty += level.difficult;
        base.Init(map);
        if (work)
        {
            var p = transform.position;
            for (int i = 0; i < unitsCout; i++)
            {
                var b = new Vector3(p.x + UnityEngine.Random.Range(-radius, radius), p.y, p.z + UnityEngine.Random.Range(-radius, radius));
                BornEnemy(b, OnEnemyDead, hero);
            }
        }
    }


    public void BornEnemy(Vector3 pos, Action<Unit> OnEnemyDead, Hero hero)
    {
        var monster = DataBaseController.Instance.Monsters.RandomElement();
        var unit = DataBaseController.Instance.GetItem(monster, pos);
        map.enemies.Add(unit);
        unit.Init(hero);
        unit.transform.SetParent(map.enemiesContainer);
        unit.OnDead += OnEnemyDead;
    }
}
