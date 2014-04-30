using UnityEngine;
using System.Collections;

public class IndividualMinionController : MonoBehaviour {

	float moveSpeed;

	/* Networking variables. -jk */
	private float lastSynchronizationTime = 0f;
	private float syncDelay = 0f;
	private float syncTime = 0f;
	private Vector3 syncStartPosition = Vector3.zero;
	private Vector3 syncEndPosition = Vector3.zero;
	private GameObject networkManager;
	
	public int minionId;
	public GameObject targetNode;
	Color minionColor;

	void Start () {
		networkManager = GameObject.FindGameObjectWithTag("NetworkManager");

		//Set the speed
		moveSpeed = 0.1f;
		Physics.IgnoreLayerCollision(11,10);

	}
	
	void Update () {
		if (Network.isServer && networkView.isMine || Network.isClient && !networkView.isMine) {
			this.gameObject.renderer.material.color = Color.red;
		}
		else if (Network.isClient && networkView.isMine || Network.isServer && !networkView.isMine) {
			this.gameObject.renderer.material.color = Color.blue;
		}
		//Move towards the target node
		if (targetNode != null) {
			Vector3 targetPosition = new Vector3 (targetNode.transform.position.x, transform.position.y, targetNode.transform.position.z);
			gameObject.transform.position = Vector3.MoveTowards (transform.position, targetPosition, moveSpeed);
		
			//Check if it reached the target
			if (transform.position == targetPosition) {
				//Destroy the object
				GameObject.FindGameObjectWithTag("MinionController").GetComponent<MinionController>().minionsSpawned--;

				Network.Destroy (GetComponent<NetworkView> ().viewID);
			}
		}
	}

	public void SetMinionId (int id) {
		minionId = id;
		Debug.Log (id);
	}
	
	[RPC]
	public void SetMinionPosition (Vector3 pposition) {
		rigidbody.position = pposition;
	}
	public void SetMinionTarget (GameObject targetObj) {
		targetNode = targetObj;
	}
}
