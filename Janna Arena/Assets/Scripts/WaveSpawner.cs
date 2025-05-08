using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct EnemyGroup
    {
        public GameObject enemyPrefab; // Враг
        public int count; // Количество этого врага
    }

    [System.Serializable]
    public struct Wave
    {
        public EnemyGroup[] enemyGroups; // Массив врагов с их количеством
        public float timeBTWSpawn; // Время между спавнами
    }

    [SerializeField] Wave[] waves; 
    [SerializeField] GameObject playerCharacter;
    [SerializeField] float enemiesSpawnDistance;

    Transform playerTransform;
    private int currentWaveIndex = 0;
    private bool isSpawning = false;

    void Start()
    {
        playerTransform = playerCharacter.transform;
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (currentWaveIndex < waves.Length)
        {
            if (!isSpawning)
            {
                isSpawning = true;
                yield return StartCoroutine(SpawnEnemies(waves[currentWaveIndex]));
                currentWaveIndex++;
                isSpawning = false;
                yield return new WaitForSeconds(2f); 
            }
            yield return null;
        }
    }

    Vector3 RandomizeV3(Vector3 inp)
    {
        float randomAngle = Random.Range(0, Mathf.PI * 2);
        float offsetX = Mathf.Cos(randomAngle) * enemiesSpawnDistance;
        float offsetY = Mathf.Sin(randomAngle) * enemiesSpawnDistance;
        return inp + new Vector3(offsetX, offsetY, 0);
    }

    IEnumerator SpawnEnemies(Wave wave)
    {
        foreach (var group in wave.enemyGroups)
        {
            for (int i = 0; i < group.count; i++)
            {
                if (playerTransform != null)
                {
                    GameObject spawnEnemy = Instantiate(group.enemyPrefab, RandomizeV3(playerTransform.position), Quaternion.identity);
                    if (!spawnEnemy.activeSelf)
                    {
                        spawnEnemy.SetActive(true);
                    }
                }
                yield return new WaitForSeconds(wave.timeBTWSpawn);
            }
        }
    }
}
