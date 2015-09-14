using System;
using UnityEngine;
using System.Collections;

public class BornPosition : MonoBehaviour
{
    public float chanceToWork = 0.9f;
    public BaseMonster monsterPrebaf;
    public float radius = 2;
    private Map map;
    public int unitsCout = 1;

    public void Init(Map map, Action<Unit> OnEnemyDead)
    {
        this.map = map;
        var mesh = GetComponent<MeshRenderer>();
        if (mesh != null)
        {
            mesh.enabled = false;
        }
        bool work = UnityEngine.Random.Range(0f, 1f) < chanceToWork;
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
        var m = (GameObject.Instantiate(monster.gameObject, pos, Quaternion.identity) as GameObject).GetComponent<Unit>();
        map.enemies.Add(m);
        m.Init();
        m.transform.SetParent(map.enemiesContainer);
        m.OnDead += OnEnemyDead;
    }
}
