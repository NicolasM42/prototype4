using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    public float speed;
    private Rigidbody bossRb;
    private GameObject player;
    private float minionCooldown = 5.0f;
    private float spawnRange = 9;

    public GameObject enemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        bossRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        minionCooldown -= Time.deltaTime;
        if (minionCooldown <= 0)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
            minionCooldown = 10.0f;
        }

        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        bossRb.AddForce(lookDirection * speed);

        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        float xPos = Random.Range(-spawnRange, spawnRange);
        float zPos = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(xPos, 0, zPos);

        return randomPos;
    }
}
