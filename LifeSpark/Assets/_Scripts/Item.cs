using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

    NetworkManager NetMgr;

	void Start () {
        NetMgr = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkManager>();
        this.gameObject.renderer.material.color = Color.yellow;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision collision) {
        if (collision.collider.tag == "Player")
        {
            collision.collider.GetComponent<Player>().ItemBoost();
            Network.Destroy(GetComponent<NetworkView>().viewID);
        }
    }
}
