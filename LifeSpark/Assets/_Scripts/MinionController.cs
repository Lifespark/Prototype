using UnityEngine;
using System.Collections;

public class MinionController : MonoBehaviour {

	public GameObject minionObject;
    public GameObject playerObject;
	int maxMinions;
	public int minionsSpawned;
	public Vector3 spawnNode;
	public Vector3 targetNode;
	float spawnRate;
	public int id = -1;

    void Start() {
        spawnNode = new Vector3(-123, -456, -789);
        targetNode = new Vector3(-123, -456, -789);
		minionsSpawned = 0;
		maxMinions = 3;
		spawnRate = 0.2f;
        if (Network.isServer) {
            StartCoroutine(SpawnMinionsCount());
        }
	}
	
	void Update () {
        if (id == -1) {
            //id = playerObject.GetComponent<Player>().getPlayerId();
            id = GetComponent<Player>().getPlayerId();
        }
	}

	IEnumerator SpawnMinionsCount () {
		//Wait
        while (true) {
            yield return new WaitForSeconds(spawnRate);
            //Check that there arent too many minions already
            if (minionsSpawned < maxMinions && targetNode != new Vector3(-123, -456, -789) && spawnNode != new Vector3(-123, -456, -789)) {
                SpawnMinions();
                //networkView.RPC("SpawnMinions", RPCMode.Server);
            }
            //Loop spawner
        }
		//StartCoroutine (SpawnMinionsCount());

	}

	[RPC]
	public void SpawnMinions() {
		minionsSpawned++;

		minionObject.GetComponent<IndividualMinionController>().SetMinionPosition(spawnNode);
		minionObject.GetComponent<IndividualMinionController>().SetMinionId(id);
		minionObject.GetComponent<IndividualMinionController>().SetMinionTarget(targetNode);

		GameObject minion = Network.Instantiate(minionObject, spawnNode, Quaternion.identity, 0) as GameObject;

		//minion.GetComponent<IndividualMinionController>().SetMinionId(id);
        minion.networkView.RPC("SetMinionId", RPCMode.AllBuffered, id);

		//minion.networkView.RPC("SetMinionColor", RPCMode.AllBuffered, r, g, b);
	}

    [RPC]
    public void SetTargetNode(Vector3 pTarget) {
        targetNode = pTarget;
    }

    [RPC]
    public void SetSpawnNode(Vector3 pSpawn) {
        spawnNode = pSpawn;
    }

    [RPC]
    public void MinusMinion() {
        minionsSpawned--;
        //Debug.Log("minionsSpawned--");
    }
}
