using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour {
    public int health = 100;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    [RPC] void ApplyDamage(int pDamage) {
        health -= pDamage;
        if (health <= 0)
            Network.Destroy(networkView.viewID);
        Debug.Log(health);
    }

    void OnGUI() {
        GUI.Label(new Rect(10, 10, 100, 50), "BOSS health: " + health);
    }
}
