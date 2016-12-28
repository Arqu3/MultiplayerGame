using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public int m_Damage = 10;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.GetComponent<Health>())
        {
            col.gameObject.GetComponent<Health>().TakeDamage(m_Damage);
        }

        Destroy(gameObject);
    }
}
