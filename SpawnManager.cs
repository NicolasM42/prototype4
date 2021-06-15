using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyList;
    public GameObject enemyPrefab;
    public GameObject ezPrefab;
    public GameObject bossPrefab;
    public GameObject powerupPrefab;
    public GameObject[] powerupList;
    public int enemyCount;
    private int waveNum = 1;
    private float spawnRange = 9;
    private bool firstWave = true;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(waveNum);
        firstWave = false;
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("enemy").Length + GameObject.FindGameObjectsWithTag("Alpha").Length + GameObject.FindGameObjectsWithTag("Boss").Length;
        if (enemyCount == 0)
        {
            int index = Random.Range(0, powerupList.Length);
            powerupPrefab = powerupList[index];
            waveNum++;

            if (waveNum % 5 == 0)
            {
                SpawnBossWave();
            }
            else
            {
                Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
                SpawnEnemyWave(waveNum);
            }
        }
    }

    private void SpawnEnemyWave(int n)
    {
        if (firstWave)
        {
            Instantiate(ezPrefab, GenerateSpawnPosition(), ezPrefab.transform.rotation);
        }
        else
        {
            for (int i = 0; i < n; i++)
            {
                int index = Random.Range(0, 2);
                enemyPrefab = enemyList[index];
                Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
            }
        }
    }

    private void SpawnBossWave()
    {
        Instantiate(bossPrefab, new Vector3(0, 5, 0), bossPrefab.transform.rotation);
    }

    private Vector3 GenerateSpawnPosition()
    {
        float xPos = Random.Range(-spawnRange, spawnRange);
        float zPos = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(xPos, 0, zPos);

        return randomPos;
    }
}
