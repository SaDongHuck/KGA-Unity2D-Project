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

    internal List<Skill> playerSkill = new List<Skill>();

    internal Skill currentSkill;

    private void Awake()
    {
        if (instance == null) { instance = this; }
        else { DestroyImmediate(this); return; }
        DontDestroyOnLoad(gameObject);
    }

    public void OnEnemyDefeated(Enemy enemy)
    {
        if(enemySpawner == null)
        {
            enemySpawner = FindObjectOfType<EnemySpawner>();
        }
        enemies.Remove(enemy);

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

    public void ResetStage()
    {
        currentStage = 1;
        enemies.Clear();
        playerSkill.Clear();
        currentSkill = null;
    }


}
