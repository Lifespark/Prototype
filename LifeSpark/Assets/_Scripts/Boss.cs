using UnityEngine;
using System.Collections;

public class Boss : LivingObject {
    //public int health = 100;
	LayerMask mask;
	Vector3 goal;
	public float speed = 2;
    // Use this for initialization
    void Start() {
        health = 100;
    }

    void Awake() {
        if (isInited)
            return;
        GUIManager.guiManager.addHealthBar(this);
        isInited = true;
		mask = ~1<<12;
		randomizeGoal();
    }

    // Update is called once per frame
    void Update() {
        if (onHealthChanged != null) {
            onHealthChanged(health);
        }
		if(Network.isServer){
			if(Random.Range(0,100)<1)
				randomizeGoal();
			while(Physics.Raycast(new Vector3(transform.position.x,.5f,transform.position.z),goal,100)){
				Debug.Log("haps");
				randomizeGoal();
			}
		}
		transform.Translate(goal*speed*Time.deltaTime);
		
    }

	void randomizeGoal(){
		float xDir = Random.Range(-100,100)/100.0f;
		float yDir = Random.Range(-100,100)/100.0f;
		networkView.RPC("UpdateGoal",RPCMode.All,new Vector3(xDir,0,yDir).normalized);
	}

	void OnCollisionEnter(Collision col){
		if(Network.isServer)
		if(col.gameObject.name!="Ground"&&!col.gameObject.name.StartsWith("Lane")){
			randomizeGoal();
		}
	}

	void OnCollisionStay(Collision col){
		if(Network.isServer)
		if(col.gameObject.name!="Ground"&&!col.gameObject.name.StartsWith("Lane")){
			randomizeGoal();
		}
	}

    [RPC]
    void ApplyDamage(int pDamage) {
        health -= pDamage;
        if (health <= 0)
            Network.Destroy(networkView.viewID);
        Debug.Log(health);

    }

	[RPC]
	void UpdateGoal(Vector3 g){
		goal = g;
	}

    void OnGUI() {
        GUI.Label(new Rect(10, 10, 100, 50), "BOSS health: " + health);
    }

}
