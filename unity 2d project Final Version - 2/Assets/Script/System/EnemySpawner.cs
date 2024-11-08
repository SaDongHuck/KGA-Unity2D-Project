using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyPrefab;

    public Transform location;

    public int spawnCount = 10; // ������ ���ػѿ� ����
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
        StopAllCoroutines(); // ���� ���� ����
        isSpawning = false;   // ���� ���� �ʱ�ȭ
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
