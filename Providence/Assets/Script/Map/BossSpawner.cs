using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class BossSpawner
{
    private int EnemiesOnStart;
    private int ToSpawnBossOnStart;
    private Action OnSpawnBoss;
    private bool isBossSpawned = false;

    public BossSpawner(int count, Action SpawnBoss)
    {
        OnSpawnBoss = SpawnBoss;
        this.EnemiesOnStart = count;
        ToSpawnBossOnStart =(int)( EnemiesOnStart*0.99f );
    }

    public void EnemieDead()
    {
        if (!isBossSpawned)
        {
            EnemiesOnStart--;
            Debug.Log("ToSpawnBossOnStart " + ToSpawnBossOnStart + " > " + EnemiesOnStart);
            if (ToSpawnBossOnStart > EnemiesOnStart)
            {
                isBossSpawned = true;
                OnSpawnBoss();
            }
        }
    }
}

