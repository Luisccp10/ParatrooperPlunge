using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public GameObject blooperPrefab;
    public float spawnRateCoin = 1f; // Monedas por segundo
    public float spawnRateBlooper = 0.5f; // Bloopers por segundo
    public float spawnAreaRadius = 10f;
    public float spawnHeight = 15f; // Altura desde donde aparecen los objetos

    private float nextSpawnTimeCoin = 0f;
    private float nextSpawnTimeBlooper = 0f;

    void Update()
    {
        if (Time.time >= nextSpawnTimeCoin)
        {
            SpawnObject(coinPrefab);
            nextSpawnTimeCoin = Time.time + 1f / spawnRateCoin;
        }

        if (Time.time >= nextSpawnTimeBlooper)
        {
            SpawnObject(blooperPrefab);
            nextSpawnTimeBlooper = Time.time + 1f / spawnRateBlooper;
        }
    }

    void SpawnObject(GameObject prefab)
    {
        Vector3 spawnPosition = Random.insideUnitSphere * spawnAreaRadius;
        spawnPosition = new Vector3(spawnPosition.x, spawnHeight, 0f);
        Instantiate(prefab, spawnPosition, Quaternion.identity);
    }

    // Opcional: Puedes dibujar Gizmos en el editor para visualizar el área de spawn
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + Vector3.up * spawnHeight, spawnAreaRadius);
    }
}
