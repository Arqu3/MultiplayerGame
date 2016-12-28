using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Health : NetworkBehaviour
{
    //Public vars
    public int m_MaxHealth = 100;
    public RectTransform m_Healthbar;
    public bool m_IsDestroyedOnDeath = false;

    //Private vars
    [SyncVar(hook = "OnChangeHealth")]
    private int m_CurrentHealth = 0;

    private NetworkStartPosition[] m_SpawnPoints;

    void Start()
    {
        m_CurrentHealth = m_MaxHealth;

        if (isLocalPlayer)
        {
            m_SpawnPoints = FindObjectsOfType<NetworkStartPosition>();
        }
    }

    public void TakeDamage(int damage)
    {
        if (!isServer)
            return;

        m_CurrentHealth -= damage;
        if (m_CurrentHealth <= 0)
        {
            if (m_IsDestroyedOnDeath)
            {
                Destroy(gameObject);
            }
            else
            {
                m_CurrentHealth = m_MaxHealth;
                RpcRespawn();
            }
        }

    }

    void OnChangeHealth(int health)
    {
        m_Healthbar.sizeDelta = new Vector2(health, m_Healthbar.sizeDelta.y);
        m_CurrentHealth = health;
    }

    //Called on the server, invoked on the clients
    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            Vector3 spawnpoint = Vector3.zero;

            if (m_SpawnPoints != null && m_SpawnPoints.Length > 0)
            {
                spawnpoint = m_SpawnPoints[Random.Range(0, m_SpawnPoints.Length)].transform.position;
            }

            transform.position = spawnpoint;
        }
    }
}
