using UnityEngine;
using System.Collections;

public class SparkPoint : MonoBehaviour {

	private LineRenderer Beam;
	
	bool particlesOn;
	int playerCaptured;
	bool captured;
	float captureTimer;
	public ParticleSystem particleSystem;
	int playerId;
	bool stealing;
	bool destroyed;
    Color playerColor;
	GameObject player;

	// Use this for initialization
	void Start () {
		particlesOn = false;
		playerCaptured = -1;
		captured = false;
		captureTimer = 0.0f;
		playerId = -1;
		stealing = false;
		destroyed = false;
		

		Beam = this.gameObject.AddComponent<LineRenderer>();
		Beam.material = new Material (Shader.Find("Particles/Additive")); 
		Beam.castShadows = false;
		Beam.receiveShadows = false;
		Beam.SetVertexCount(2);
		Beam.SetColors(Color.white, Color.white);
		Beam.SetWidth(0.2f, 0.2f);
		Beam.useWorldSpace = true;
		Beam.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (particlesOn && !captured) {
			captureTimer += Time.deltaTime;
			if (captureTimer >= 3.0f) {
				if (player.GetComponent<Player>().GetRespawnPoint() == null) {
					player.GetComponent<Player>().SetRespawnPoint(this.gameObject);
				}
				captured = true;
				ChangeColors();
			}
			else if (captureTimer >= 2.5f) {
				particleSystem.startSize = 5.0f;
			}
			else {
				particleSystem.startSize += Time.deltaTime/2;
			}
		}
		else if (captured && stealing) {
			captureTimer += Time.deltaTime;
			if (captureTimer >= 3.0f) {
				destroyed = true;
				Explode();
			}
			else if (captureTimer >= 2.5f) {
				particleSystem.startSize = 5.0f;
			}
			else {
				particleSystem.startSize += Time.deltaTime/2;
			}
		}
		
		if (captured)
		{
			Vector3 srcPos = gameObject.transform.position;
			Vector3 dstPos;
			float minDistance = 0;
			Beam.SetPosition(0, srcPos);
			//Beam.SetPosition(1, new Vector3(1,1,1));
			
			IndividualMinionController[] minions = GameObject.FindObjectsOfType(typeof(IndividualMinionController))
				as IndividualMinionController[];
			Beam.enabled = false;
			if(minions.Length>0)
			{
				for(int i =0;i<minions.Length;i++)
				{
					if (minions[0].minionId == playerId)
						continue;
					float distance = Vector3.Distance(minions[i].gameObject.transform.position,srcPos);
					if (distance < minDistance || minDistance == 0)
					{
						minDistance = distance;
						dstPos = minions[i].gameObject.transform.position;
						Beam.SetPosition(1, dstPos);
						Beam.enabled = true;
					}
				}
			}
		}
	}

	void OnCollisionEnter (Collision collision) {
		if (collision.collider.tag.Equals ("Player") && !destroyed) {
			if (!captured) {
				if (!particlesOn) {
					particlesOn = true;
					particleSystem.Play();
					particleSystem.startSize = 1.0f;
					captureTimer = 0.0f;
					playerId = collision.gameObject.GetComponent<Player>().getPlayerId();
                    playerColor = collision.gameObject.renderer.material.color;
					player = collision.collider.gameObject;
				}
			}
			else if (collision.gameObject.GetComponent<Player>().getPlayerId() != playerCaptured && !stealing) {
				stealing = true;
				if (collision.gameObject.GetComponent<Player>().getPlayerId() == 1) {
                    particleSystem.startColor = playerColor;
				}
				else {
                    particleSystem.startColor = playerColor;
				}
				particleSystem.startSize = 1.0f;
				captureTimer = 0.0f;
				playerId = collision.gameObject.GetComponent<Player>().getPlayerId();
			}
		}
	}

	void OnCollisionExit (Collision collision) {
		if (collision.collider.tag.Equals("Player")) {
			if (!captured) {
				particlesOn = false;
				particleSystem.Stop();
			}
			else if (!destroyed) {
				RevertColors();
			}
		}
	}

	void ChangeColors() {
		playerCaptured = playerId;
		if (playerCaptured == 1) {
			particleSystem.startSize = 0.6f;
            this.gameObject.renderer.material.color = playerColor;
            particleSystem.startColor = playerColor;
		}
		else {
			particleSystem.startSize = 0.6f;
            this.gameObject.renderer.material.color = playerColor;
            particleSystem.startColor = playerColor;
		}
	}

	void RevertColors() {
		if (playerCaptured == 1) {
			particleSystem.startSize = 0.6f;
            this.gameObject.renderer.material.color = playerColor;
            particleSystem.startColor = playerColor;
		}
		else {
			particleSystem.startSize = 0.6f;
            this.gameObject.renderer.material.color = playerColor;
            particleSystem.startColor = playerColor;
		}
	}

	void Explode() {
		particleSystem.Stop();
		this.gameObject.renderer.material.color = Color.grey;
		playerCaptured = -1;
		Destroy (this.gameObject);
	}

	public bool GetParticles() {
		return particlesOn;
	}

	public int GetPlayerCaptured() {
		return playerCaptured;
	}
}
