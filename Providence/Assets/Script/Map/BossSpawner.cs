using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        ToSpawnBossOnStart =(int)( EnemiesOnStart*0.35f );
    }

    public void EnemieDead()
    {
        if (!isBossSpawned)
        {
            EnemiesOnStart--;
            if (ToSpawnBossOnStart > EnemiesOnStart)
            {
                isBossSpawned = true;
                OnSpawnBoss();
            }
        }
    }
}

