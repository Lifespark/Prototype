﻿using UnityEngine;
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
	public Vector3 targetNode;

    GameObject targetGameObject;
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
		if (targetNode != null && Network.isServer) {
			Vector3 targetPosition = new Vector3 (targetNode.x, transform.position.y, targetNode.z);
			gameObject.transform.position = Vector3.MoveTowards (transform.position, targetPosition, moveSpeed);
		
			//Check if it reached the target
			if (transform.position == targetPosition) {
				//Destroy the object
				//GameObject.FindGameObjectWithTag("MinionController").GetComponent<MinionController>().minionsSpawned--;

                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

                foreach (GameObject p in players) {
                    MinionController mc = p.GetComponent<MinionController>();
                    if (mc.id == minionId) {
                        mc.networkView.RPC("MinusMinion", RPCMode.AllBuffered);
                        break;
                    }
                }

                Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1.0f);
                int i = 0;
                while (i < hitColliders.Length) {
                    if (hitColliders[i].gameObject.tag == "SparkPoint") {
                        hitColliders[i].gameObject.networkView.RPC("MinionCapture", RPCMode.AllBuffered, minionId);
                        break;
                    }
                    i++;
                }


				Network.Destroy (GetComponent<NetworkView> ().viewID);
                //Destroy(gameObject);
			}
		}
	}

    void OnCollisionEnter(Collision collision) {
        if (Network.isServer) {
            if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<Player>().getPlayerId() != minionId) {
                collision.gameObject.networkView.RPC("ApplyDamage", RPCMode.AllBuffered, 2);
                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

                foreach (GameObject p in players) {
                    MinionController mc = p.GetComponent<MinionController>();
                    if (mc.id == minionId) {
                        mc.networkView.RPC("MinusMinion", RPCMode.AllBuffered);
                        break;
                    }
                }
                Network.Destroy(GetComponent<NetworkView>().viewID);
            }
        }
    }

    [RPC]
	public void SetMinionId (int id) {
		minionId = id;
		//Debug.Log (id);
	}
	
	[RPC]
	public void SetMinionPosition (Vector3 pposition) {
		rigidbody.position = pposition;
	}
	public void SetMinionTarget (Vector3 targetObj) {
		targetNode = targetObj;
	}
}
