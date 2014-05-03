using UnityEngine;
using System.Collections;

public class MinionController : MonoBehaviour {

	public GameObject minionObject;
    public GameObject playerObject;
	int maxMinions;
	public int minionsSpawned;
	public GameObject spawnNode;
	public GameObject targetNode;
	float spawnRate;
	public int id = -1;

	void Start () {
		minionsSpawned = 0;
		maxMinions = 3;
		spawnRate = 0.2f;
		StartCoroutine (SpawnMinionsCount());
	}
	
	void Update () {
        if (id == -1) {
            id = playerObject.GetComponent<Player>().getPlayerId();
        }
	}

	IEnumerator SpawnMinionsCount () {
		//Wait
		yield return new WaitForSeconds (spawnRate);
		//Check that there arent too many minions already
		if (minionsSpawned < maxMinions && targetNode!=null && spawnNode!=null) {
			SpawnMinions();
		}
		//Loop spawner
		StartCoroutine (SpawnMinionsCount());

	}

	[RPC]
	public void SpawnMinions() {
		minionsSpawned++;

		minionObject.GetComponent<IndividualMinionController>().SetMinionPosition(spawnNode.transform.position);
		minionObject.GetComponent<IndividualMinionController>().SetMinionId(id);
		minionObject.GetComponent<IndividualMinionController>().SetMinionTarget(targetNode);

		GameObject minion = Network.Instantiate(minionObject, spawnNode.transform.position, Quaternion.identity, 0) as GameObject;

		//minion.GetComponent<IndividualMinionController>().SetMinionId(id);
        minion.networkView.RPC("SetMinionId", RPCMode.AllBuffered, id);

		//minion.networkView.RPC("SetMinionColor", RPCMode.AllBuffered, r, g, b);

	}

}
