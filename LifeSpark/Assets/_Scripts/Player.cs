using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : LivingObject {

	/* Player's variables. -jk */
	private LinkedList<GameObject> respawnPoints;
	private Vector3 translate;
	private Vector3 teleportPosition;
	private float correctingFactorMove;
	private float correctingFactorSparkPoint;
	private bool moveSparkPoint;
	private float speed;
	private bool move;
	private int playerId;
	static int id = 1;
	public GameObject projectile;
	private GameObject networkManager;
	private GameObject respawnPoint;
	private float respawnTimer;
	private bool needRespawn;
	//public GameObject minionController;
    public MinionController minionController;
	/* Networking variables. -jk */
	private float lastSynchronizationTime = 0f;
	private float syncDelay = 0f;
	private float syncTime = 0f;
	private Vector3 syncStartPosition = Vector3.zero;
	private Vector3 syncEndPosition = Vector3.zero;
    public GameObject projectilePrefab;
    private Color projectileColor;

    public bool itemBuff = false;
    public bool ismine = false;

    void Awake() {
        if (isInited)
            return;
        //GUIManager.guiManager.addHealthBar(this);
        isInited = true;
    }

	void Start () {
		respawnPoints = new LinkedList<GameObject>();
		networkManager = GameObject.FindGameObjectWithTag("NetworkManager");
		translate = Vector3.zero;
		teleportPosition = Vector3.zero;
		correctingFactorMove = 0.1f;
		correctingFactorSparkPoint = 0.7f;
		moveSparkPoint = false;
		speed = 5.0f;
		move = false;
		playerId = id;
		id+=1;
        //Debug.Log(playerId);

		respawnTimer = -1f;
		respawnPoint = null;
		needRespawn = false;

        minionController = GetComponent<MinionController>();
		//minionController = (GameObject) Instantiate (minionController);
        //minionController.GetComponent<MinionController>().id = playerId;

        health = 20;
	}
	
	void Update () {
		//Debug.Log (respawnPoints.Count);
		//later may want to move all input to server side -- server takes all input, then moves things -- more secure
		if (ismine) {
			PlayerInput ();
            //Debug.Log();
		}
		//else {
		//	SyncedMovement ();
		//}
        MovePlayer();

        if (onHealthChanged != null) {
            onHealthChanged(health);
        }

	}

	void PlayerInput () {

		if (respawnTimer > 0.0f) {
			respawnTimer -= Time.deltaTime;
		}

		if (respawnTimer <= 0.0f && needRespawn) {
			if (respawnPoint != null) {
                health = 20;
				Vector3 targetPos = new Vector3(respawnPoint.transform.position.x, 10.0f, respawnPoint.transform.position.z);
				networkView.RPC("NetworkRespawn",RPCMode.AllBuffered,targetPos);
				needRespawn = false;
			}
		}

		if (Input.GetMouseButtonDown(0) && !needRespawn) {
			
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			
			if (Physics.Raycast(ray, out hit, 1000.0f))
			{
				Debug.Log (hit.collider.name);
				if (hit.collider.tag == "Ground" || hit.collider.tag == "Lane")
				{
					//translate = hit.point;
                    networkView.RPC("NetworkMove", RPCMode.Server, hit.point);
					moveSparkPoint = false;
				}
                else if (hit.collider.tag == "Player" && hit.collider.gameObject != this.gameObject || hit.collider.tag == "Boss")
				{
					networkView.RPC("NetworkMove",RPCMode.Server,Vector3.zero);
					Vector3 projPosition = hit.collider.gameObject.transform.position - rigidbody.position;
					projPosition.Normalize();
                    projPosition = projPosition * 2 + rigidbody.position;
                    Material mat = gameObject.renderer.material;
					//networkManager.GetComponent<NetworkManager>().SpawnProjectile(projPosition, projPosition - rigidbody.position, playerId, mat.color);
					//SpawnBullet (projPosition,projPosition - rigidbody.position, playerId);
                    //SpawnProjectile(projPosition, projPosition - rigidbody.position, playerId, mat.color.r, mat.color.g, mat.color.b);
                    networkView.RPC("SpawnProjectile", RPCMode.Server, projPosition, projPosition - rigidbody.position, playerId, mat.color.r, mat.color.g, mat.color.b);
				}
				else if (hit.collider.tag == "SparkPoint")
				{
					if (hit.collider.gameObject.GetComponent<SparkPoint>().GetPlayerCaptured()
					    == playerId)
					{
						networkView.RPC("NetworkMove",RPCMode.Server,Vector3.zero);
						teleportPosition = Vector3.Normalize(this.rigidbody.position - hit.collider.rigidbody.position);
						teleportPosition += hit.collider.rigidbody.position;
						teleportPosition.y = this.rigidbody.position.y;
						networkView.RPC("NetworkTeleport",RPCMode.Server, teleportPosition);
					}
					//minionController.GetComponent<MinionController> ().targetNode = hit.collider.gameObject;
					//minionController.targetNode = hit.collider.gameObject;
					else 
					{
						networkView.RPC("SetTargetNode", RPCMode.AllBuffered, hit.collider.gameObject.transform.position);
					}
				}
			}
		}
        /*
		if (translate != Vector3.zero)
		{
			rigidbody.MovePosition(rigidbody.position + Vector3.Normalize(translate - rigidbody.position) * speed * Time.deltaTime);
			if (moveSparkPoint)
			{
				if (rigidbody.position.x >= translate.x - correctingFactorSparkPoint
				    && rigidbody.position.x <= translate.x + correctingFactorSparkPoint
				    && rigidbody.position.z >= translate.z - correctingFactorSparkPoint
				    && rigidbody.position.z <= translate.z + correctingFactorSparkPoint)
				{
					translate = Vector3.zero;
				}
			}
			else
			{
				if (rigidbody.position.x >= translate.x - correctingFactorMove
				    && rigidbody.position.x <= translate.x + correctingFactorMove
				    && rigidbody.position.z >= translate.z - correctingFactorMove
				    && rigidbody.position.z <= translate.z + correctingFactorMove)
				{
					translate = Vector3.zero;
				}
			}
		}
        */
	}

    void MovePlayer() {
        if (translate != Vector3.zero) {
            rigidbody.MovePosition(rigidbody.position + Vector3.Normalize(translate - rigidbody.position) * speed * Time.deltaTime);
            if (moveSparkPoint) {
                if (rigidbody.position.x >= translate.x - correctingFactorSparkPoint
                    && rigidbody.position.x <= translate.x + correctingFactorSparkPoint
                    && rigidbody.position.z >= translate.z - correctingFactorSparkPoint
                    && rigidbody.position.z <= translate.z + correctingFactorSparkPoint) {
                    translate = Vector3.zero;
                }
            }
            else {
                if (rigidbody.position.x >= translate.x - correctingFactorMove
                    && rigidbody.position.x <= translate.x + correctingFactorMove
                    && rigidbody.position.z >= translate.z - correctingFactorMove
                    && rigidbody.position.z <= translate.z + correctingFactorMove) {
                    translate = Vector3.zero;
                }
            }
        }
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

	public int getPlayerId() {
		return playerId;
	}

	public void TakeDamage() {
		//networkManager.GetComponent<NetworkManager>().DestroyNetworkObject(this.gameObject);;
		rigidbody.position = new Vector3 (0f,-5f,0f);
		respawnTimer = 3.0f;
		needRespawn = true;
	}

	[RPC] void ApplyDamage(int dmg) {
		Debug.Log("called");

        health -= dmg;

        if (health <= 0) {
            health = 0;
            rigidbody.position = new Vector3(0f, -5f, 0f);
            respawnTimer = 3.0f;
            needRespawn = true;
            networkManager.GetComponent<NetworkManager>().SpawnItem(this.gameObject.transform.position);
        }
	}

    [RPC]
    public void SpawnProjectile(Vector3 position, Vector3 direction, int id, float r, float g, float b) {
        projectilePrefab.GetComponent<Projectile>().SetProjectilePosition(position);
        projectilePrefab.GetComponent<Projectile>().SetProjectileDirection(direction);
        projectilePrefab.GetComponent<Projectile>().SetProjectileId(id);
        GameObject bullet = Network.Instantiate(projectilePrefab, position, Quaternion.identity, 0) as GameObject;
        //bullet.GetComponent<Projectile>().SetProjectileColor(new Color(r, g, b));
        //bullet.GetComponent<Projectile>().SetProjectileId(id);
        bullet.networkView.RPC("SetProjectileId", RPCMode.AllBuffered, id);
        bullet.networkView.RPC("SetProjectileColor", RPCMode.AllBuffered, projectileColor.r, projectileColor.g, projectileColor.b);
        /*
        bullet.GetComponent<Projectile>().SetProjectileDirection(direction);
        bullet.GetComponent<Projectile>().SetProjectileId(id);
        bullet.GetComponent<Projectile>().SetProjectileColor(color);
        */
    }

	public GameObject GetRespawnPoint() {
		return respawnPoint;
	}

	public void removeSpawnPoint(GameObject point){
		Debug.Log (respawnPoints.Remove(point));
		Destroy(point);
		respawnPoint = respawnPoints.First.Value;
	}

	public void AddRespawnPoint (GameObject gameObject) {
		if(respawnPoint == null){
			respawnPoint = gameObject;
			//minionController.GetComponent<MinionController> ().spawnNode = gameObject;
        //minionController.spawnNode = gameObject;
        	networkView.RPC("SetSpawnNode", RPCMode.AllBuffered, gameObject.transform.position);
		}
		respawnPoints.AddLast(gameObject);
	}

    public void ItemBoost() {
        if (this.gameObject.renderer.material.color == Color.red)
            projectileColor = new Color(1f, 0.40f, 0f);
        else if (this.gameObject.renderer.material.color == Color.blue)
            projectileColor = new Color(0f, 1f, 0f);
    }

    [RPC]
    void SetNetworkPlayer(NetworkPlayer np, int id) {
        if (np == Network.player) {
            ismine = true;// Or disable this script if the NetworkPlayer doesn't match.
        }
        playerId = id;

        switch (playerId) {
            case 1:
                this.gameObject.renderer.material.color = Color.red;
                projectileColor = Color.red;
                break;
            case 2:
                this.gameObject.renderer.material.color = Color.blue;
                projectileColor = Color.blue;
                break;
            case 3:
                this.gameObject.renderer.material.color = Color.yellow;
                projectileColor = Color.yellow;
                break;
            case 4:
                this.gameObject.renderer.material.color = Color.green;
                projectileColor = Color.green;
                break;
        }
    }

    [RPC]
    public void NetworkMove(Vector3 trans) {
        translate = trans;
    }

	[RPC]
	public void NetworkTeleport(Vector3 pos){
		rigidbody.position = pos;
	}

	[RPC]
	public void NetworkRespawn(Vector3 pos){
		rigidbody.position = pos;
		respawnTimer = -1;
		needRespawn = false;
		health = 20;
	}

	void OnGUI(){
		//Debug.Log (playerId + " " + health);
		GUI.Label(new Rect(10, playerId*20, 100, 50), "Player" + playerId+  ": " + health);
	}
}
