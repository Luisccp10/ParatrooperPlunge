using UnityEngine;
using System.Collections.Generic;

public class InitialCircleFormation : MonoBehaviour
{
    public GameObject[] playerPrefabs;
    public float circleRadius = 5f;
    public float spawnHeight = 10f;
    public Transform centerPoint;
    public FollowGroup overheadFollowCamera; // Referencia a la cámara de seguimiento

    private List<Transform> spawnedPlayers = new List<Transform>();

    void Start()
    {
        if (playerPrefabs.Length == 0)
        {
            Debug.LogError("No se han asignado prefabs de jugador en InitialCircleFormation.");
            return;
        }

        if (overheadFollowCamera == null)
        {
            Debug.LogError("No se ha asignado la cámara de seguimiento en InitialCircleFormation.");
            return;
        }

        Vector3 center = centerPoint != null ? centerPoint.position : transform.position;
        int numberOfPlayers = playerPrefabs.Length;

        for (int i = 0; i < numberOfPlayers; i++)
        {
            float angle = i * Mathf.PI * 2f / numberOfPlayers;
            float x = center.x + circleRadius * Mathf.Cos(angle);
            float z = center.z + circleRadius * Mathf.Sin(angle);
            Vector3 spawnPosition = new Vector3(x, spawnHeight, z);

            GameObject playerInstance = Instantiate(playerPrefabs[i], spawnPosition, Quaternion.Euler(180f, 0f, 0f));
            spawnedPlayers.Add(playerInstance.transform);
        }

        // Pasar la lista de Transforms de los jugadores a la cámara de seguimiento
        overheadFollowCamera.SetTargets(spawnedPlayers.ToArray());

        // Opcional: Desactivar la cámara de seguimiento al inicio si quieres otra vista primero
        // overheadFollowCamera.gameObject.SetActive(false);

        // Opcional: Puedes destruir este spawner después de instanciar a los jugadores
        // Destroy(gameObject);
    }
}