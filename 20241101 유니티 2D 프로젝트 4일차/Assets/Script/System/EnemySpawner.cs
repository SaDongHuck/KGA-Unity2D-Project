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
        //StartCoroutine(EnemySpawn());
        SpawnEnemies();
    }

    public void SpawnEnemies()
    {
        // ���� �� ���� (�ʿ��� ���)
        foreach (Enemy enemy in GameManager.Instance.enemies) { Destroy(enemy.gameObject); }
        GameManager.Instance.enemies.Clear(); // ���� ���ػѿ並 ����� ���� ����

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
