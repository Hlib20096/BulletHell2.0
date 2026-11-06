using System.Collections;
using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
    public GameObject shieldBonusPrefab;
    public float minSpawnTime = 8f;
    public float maxSpawnTime = 14f;

    public float xMin = -6f;
    public float xMax = 6f;
    public float yMin = -3f;
    public float yMax = 3f;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            float delay = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(delay);

            Vector3 spawnPos = new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), 0f);
            if (shieldBonusPrefab != null)
            {
                Instantiate(shieldBonusPrefab, spawnPos, Quaternion.identity);
            }
        }
    }
}