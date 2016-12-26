using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ControllerPlayer : NetworkBehaviour
{
    //Public vars
    public float m_MovementSpeed = 3.0f;
    public float m_TurnSpeed = 150.0f;

	void Start()
    {
	}
	
	void Update()
    {
        //Make sure only localplayer can access its own script
        if (!isLocalPlayer)
            return;

        transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal") * m_TurnSpeed * Time.deltaTime, 0));
        transform.Translate(new Vector3(0, 0, Input.GetAxis("Vertical") * m_MovementSpeed * Time.deltaTime));
	}

    public override void OnStartLocalPlayer()
    {
        if (GetComponent<MeshRenderer>())
            GetComponent<MeshRenderer>().material.color = Color.blue;
    }
}
