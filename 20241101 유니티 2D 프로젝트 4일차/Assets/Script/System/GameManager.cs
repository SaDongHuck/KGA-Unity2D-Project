using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    public int currentStage = 1;

    public EnemySpawner enemySpawner;

    internal Player player;

    internal List<Enemy> enemies = new List<Enemy>();

    private void Awake()
    {
        if (instance == null) { instance = this; }
        else { DestroyImmediate(this); return; }
        DontDestroyOnLoad(gameObject);
    }

    public void OnEnemyDefeated(Enemy enemy)
    {
        enemies.Remove(enemy);
        // 모든 적이 죽었는지 확인

        if (!enemySpawner.isSpawning && enemies.Count == 0)
        {
            IncreaseStage();
            enemySpawner.SpawnEnemies();
        }
    }

    public void IncreaseStage()
    {
        currentStage++;
        print("스테이지: " + currentStage);
    }
    
}
