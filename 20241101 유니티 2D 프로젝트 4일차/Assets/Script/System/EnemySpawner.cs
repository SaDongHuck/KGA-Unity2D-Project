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
        //StartCoroutine(EnemySpawn());
        SpawnEnemies();
    }

    public void SpawnEnemies()
    {
        // 기존 적 삭제 (필요한 경우)
        foreach (Enemy enemy in GameManager.Instance.enemies) { Destroy(enemy.gameObject); }
        GameManager.Instance.enemies.Clear(); // 기존 방해뿌요를 지우고 새로 생성

        StartCoroutine(EnemySpawn());
    }


    private IEnumerator EnemySpawn()
    {
        //int count = GameManager.Instance.currentStage * spawnCount;

        isSpawning = true;

        for (int i = 0; i < spawnCount; i++)
        {
            GameObject enemy = Instantiate(EnemyPrefab, location.position, Quaternion.identity);

            enemy.name = $"Enemy{i}";

            GameManager.Instance.enemies.Add(enemy.GetComponent<Enemy>());
            
            yield return new WaitForSeconds(spawnInterval);
        }

        isSpawning = false;
    }


}
