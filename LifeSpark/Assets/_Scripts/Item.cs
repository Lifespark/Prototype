using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

    NetworkManager NetMgr;

	void Start () {
        NetMgr = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkManager>();
        this.gameObject.renderer.material.color = Color.yellow;
		Debug.Log("Item created");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision collision) {
        if (collision.collider.tag == "Player")
        {
			Debug.Log("Item grabbed");
            collision.collider.GetComponent<Player>().ItemBoost();
			if (networkView.isMine) {
            	Network.Destroy(GetComponent<NetworkView>().viewID);
			}
        }
    }
}
