using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float cooldown = 2f;
    [SerializeField] private float cooldownDecreaseRate = 0.2f;
    [SerializeField] private float cooldownCap = 0.7f;
    private float timer;
    private Transform player;

    private void Awake()
    {
        player = FindFirstObjectByType<Player>().transform;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = cooldown;
            SpawnEnemy();
            
            if (cooldown > cooldownCap)
            {
                cooldown -= cooldownDecreaseRate;
            }
            // also can do this -> cooldown = Mathf.Max(cooldownCap, cooldown - cooldownDecreaseRate);
        }
    }

    private void SpawnEnemy()
    {
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        Vector3 spawnPosition = spawnPoints[spawnPointIndex].position;

        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        bool spawnOnRight = newEnemy.transform.position.x > player.transform.position.x;
        
        if (spawnOnRight)
        { 
            newEnemy.GetComponent<Enemy>().Flip();
        }
    }
}
