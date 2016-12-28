using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ControllerPlayer : NetworkBehaviour
{
    //Public vars
    public float m_MovementSpeed = 3.0f;
    public float m_TurnSpeed = 150.0f;
    public GameObject m_BulletPrefab;

    //Shoot vars
    private Transform m_ShootTransform;

    public override void OnStartLocalPlayer()
    {
        if (GetComponent<MeshRenderer>())
            GetComponent<MeshRenderer>().material.color = Color.blue;

        m_ShootTransform = transform.FindChild("BulletSpawn");
    }

    void Update()
    {
        //Make sure only localplayer can access its own script
        if (!isLocalPlayer)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
            CmdShoot();

        transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal") * m_TurnSpeed * Time.deltaTime, 0));
        transform.Translate(new Vector3(0, 0, Input.GetAxis("Vertical") * m_MovementSpeed * Time.deltaTime));
	}

    //This function is called by clients, but it runs on the server
    [Command]
    void CmdShoot()
    {
        var bullet = (GameObject)Instantiate(m_BulletPrefab, m_ShootTransform.position, m_ShootTransform.rotation);
        if (bullet.GetComponent<Rigidbody>())
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6.0f;

        NetworkServer.Spawn(bullet);

        Destroy(bullet, 2.0f);
    }
}
