using System;
using UnityEngine;
using System.Collections;

public class BornPosition : BaseBornPosition
{
    public BaseMonster monsterPrebaf;
    private Level level;

    public void Init(Map map, Action<Unit> OnEnemyDead,Level level)
    {
        this.level = level;
        base.Init(map);
        if (work)
        {
            var p = transform.position;
            for (int i = 0; i < unitsCout; i++)
            {
                var b = new Vector3(p.x + UnityEngine.Random.Range(-radius, radius), p.y, p.z + UnityEngine.Random.Range(-radius, radius));
                BornEnemy(b, OnEnemyDead);
            }
        }
    }


    public void BornEnemy(Vector3 pos, Action<Unit> OnEnemyDead)
    {
        var monster = DataBaseController.Instance.Monsters.RandomElement();
        var unit = (GameObject.Instantiate(monster.gameObject, pos, Quaternion.identity) as GameObject).GetComponent<Unit>();
        map.enemies.Add(unit);
        unit.Init();
        unit.transform.SetParent(map.enemiesContainer);
        unit.OnDead += OnEnemyDead;
    }
}
