using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	/* Projectile variables. -jk */
	public Vector3 translate;
	private Vector3 position;
	private float speed;
	private bool move;
	public int projectileId;
	public Color projectileColor;
	private float distanceTraveled;
	private GameObject networkManager;

	/* Networking variables. -jk */
	private float lastSynchronizationTime = 0f;
	private float syncDelay = 0f;
	private float syncTime = 0f;
	private Vector3 syncStartPosition = Vector3.zero;
	private Vector3 syncEndPosition = Vector3.zero;

	void Start () {
		networkManager = GameObject.FindGameObjectWithTag("NetworkManager");
		distanceTraveled = 0.0f;
		speed = 8.0f;
		move = false;
        Physics.IgnoreLayerCollision(8, 9);
	}
	
	void Update () {
		this.gameObject.renderer.material.color = Color.yellow;
		if (networkView.isMine) {
			ProjectileMovement();
		}
		else {
			//SyncedMovement ();
		}
	}

	void ProjectileMovement() {
		Vector3 projMove = translate + rigidbody.position;
		Vector3.Normalize(projMove);
		distanceTraveled += Vector3.Magnitude(translate * speed * Time.deltaTime);
		if (distanceTraveled >= 10.0f) {
			//networkManager.GetComponent<NetworkManager>().DestroyNetworkObject(this.gameObject);
            Network.Destroy(GetComponent<NetworkView>().viewID);
		}
		rigidbody.MovePosition(rigidbody.position + translate * speed * Time.deltaTime);
	}

    /*
	void OnSerializeNetworkView (BitStream stream, NetworkMessageInfo info) {
		//use navmesh for even smoother inperolation prediction
		Vector3 syncPosition = Vector3.zero;
		Vector3 syncVelocity = Vector3.zero;
		
		if (stream.isWriting) {
			syncPosition = rigidbody.position;
			stream.Serialize (ref syncPosition);
			
			syncVelocity = rigidbody.velocity;
			stream.Serialize (ref syncVelocity);
		}
		else {
			stream.Serialize (ref syncPosition);
			stream.Serialize (ref syncVelocity);
			
			syncTime = 0f;
			syncDelay = Time.time - lastSynchronizationTime;
			lastSynchronizationTime = Time.time;
			
			syncEndPosition = syncPosition + syncVelocity * syncDelay;
			syncStartPosition = rigidbody.position;
			//rigidbody.position = syncPosition;
		}
	}
    */
	
	private void SyncedMovement () {
		syncTime += Time.deltaTime;
		rigidbody.position = Vector3.Lerp (syncStartPosition, syncEndPosition, syncTime / syncDelay);
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.collider.tag == "SparkPoint" || collision.collider.tag == "Wall") {
            Network.Destroy(GetComponent<NetworkView>().viewID);
		}
		if (collision.collider.tag == "Player") {
			Debug.Log ("why");
			collision.gameObject.networkView.RPC("ApplyDamage",RPCMode.AllBuffered);
            Network.Destroy(GetComponent<NetworkView>().viewID);
        }
        if (collision.collider.tag == "Boss") {
            collision.gameObject.networkView.RPC("ApplyDamage", RPCMode.AllBuffered, 5);
            Network.Destroy(GetComponent<NetworkView>().viewID);
        }
	}
    [RPC]
	public void SetProjectileId (int id) {
		projectileId = id;
		Debug.Log (id);
	}

	[RPC]
    public void SetProjectileColor (float r, float g, float b) {
		projectileColor = new Color(r, g, b);
        this.gameObject.renderer.material.color = projectileColor;
	}

	public void SetProjectilePosition (Vector3 pposition) {
		position = pposition;
		rigidbody.position = pposition;
	}

	public void SetProjectileDirection (Vector3 direction) {
		Debug.Log (direction);
		translate = direction;
	}
}
