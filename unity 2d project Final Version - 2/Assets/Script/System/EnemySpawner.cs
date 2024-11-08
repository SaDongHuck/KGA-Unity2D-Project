using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyPrefab;

    public Transform location;

    public int spawnCount = 10; // 생성할 방해뿌요 개수
    public float spawnInterval = 0.5f;
    public bool isSpawning = false;

    private void Start()
    {
        SpawnEnemies();
    }

    public void SpawnEnemies()
    {
        
        StartCoroutine(EnemySpawn());
    }

    public void ResetSpawner()
    {
        StopAllCoroutines(); // 기존 스폰 중지
        isSpawning = false;   // 스폰 상태 초기화
    }


    private IEnumerator EnemySpawn()
    {

        isSpawning = true;

        for (int i = 0; i < spawnCount; i++)
        {
            GameObject enemy = Instantiate(EnemyPrefab, location.position, Quaternion.identity);

            enemy.name = $"Enemy{i}";

            Enemy enemyComponent = enemy.GetComponent<Enemy>();
            enemyComponent.SetHealth(GameManager.Instance.currentStage);

            GameManager.Instance.enemies.Add(enemy.GetComponent<Enemy>());
            
            yield return new WaitForSeconds(spawnInterval);
        }

        isSpawning = false;
    }


}
