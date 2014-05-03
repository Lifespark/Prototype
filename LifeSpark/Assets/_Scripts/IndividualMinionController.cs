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
        switch (minionId) {
            case 1:
                this.gameObject.renderer.material.color = Color.red;
                break;
            case 2:
                this.gameObject.renderer.material.color = Color.blue;
                break;
            case 3:
                this.gameObject.renderer.material.color = Color.yellow;
                break;
            case 4:
                this.gameObject.renderer.material.color = Color.green;
                break;
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

    [RPC]
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
