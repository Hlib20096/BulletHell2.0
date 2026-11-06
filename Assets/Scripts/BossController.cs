using System.Collections;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("Bullet Storm Settings")]
    public GameObject enemyBulletPrefab;
    public float spawnInterval = 0.2f;
    public float bulletSpeed = 5f;

    [Header("Spawn Area")]
    public float xMin = -8f;
    public float xMax = 8f;
    public float spawnY = 6f;

    private void Start()
    {
        StartCoroutine(SpawnBulletsPattern());
    }

    private IEnumerator SpawnBulletsPattern()
    {
        while (true)
        {
            float randomX = Random.Range(xMin, xMax);
            Vector3 spawnPosition = new Vector3(randomX, spawnY, 0f);

            GameObject bullet = Instantiate(enemyBulletPrefab, spawnPosition, Quaternion.identity);

            Vector2 randomDirection = new Vector2(Random.Range(-0.3f, 0.3f), -1f).normalized;

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = randomDirection * bulletSpeed;
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}