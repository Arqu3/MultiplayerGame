using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class EnemySpawner : NetworkBehaviour
{
    public GameObject m_EnemyPrefab;
    public int m_SpawnAmount = 5;

    public override void OnStartServer()
    {
        for (int i = 0; i < m_SpawnAmount; i++)
        {
            var spawnpos = new Vector3(Random.Range(-8.0f, 8.0f), 0.0f, Random.Range(-8.0f, 8.0f));
            var spawnrot = Quaternion.Euler(new Vector3(0.0f, Random.Range(0.0f, 180.0f), 0.0f));

            var enemy = (GameObject)Instantiate(m_EnemyPrefab, spawnpos, spawnrot);
            NetworkServer.Spawn(enemy);
        }
    }
}
