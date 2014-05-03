using UnityEngine;
using System.Collections;

public class SparkPointBoss : MonoBehaviour {

	bool particlesOn;
	bool captured;
	float captureTimer;
	public ParticleSystem particleSystem;
	int playerId;
	bool destroyed;
	bool inputOne;
	bool inputTwo;

	public SparkPoint SparkPoint1;
	public SparkPoint SparkPoint2;
	public SparkPoint SparkPoint3;
	public SparkPoint SparkPoint4;
	public SparkPoint SparkPoint5;
	public SparkPoint SparkPoint6;
	public SparkPoint SparkPoint7;
	public SparkPoint SparkPoint8;

    NetworkManager NetMgr;

	// Use this for initialization
	void Start () {
		particlesOn = false;
		captured = false;
		captureTimer = 0.0f;
		playerId = -1;
		destroyed = false;
		inputOne = false;
		inputTwo = false;
        NetMgr = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkManager>();
	}
	
	// Update is called once per frame
	void Update () {

		inputOne = false;
		inputTwo = false;

		if (SparkPoint1.gameObject != null) {
			if (SparkPoint1.GetComponent<SparkPoint>().GetParticles()) {
				if (SparkPoint1.GetComponent<SparkPoint>().GetPlayerCaptured() == 1) {
					inputOne = true;
				}
				else if (SparkPoint1.GetComponent<SparkPoint>().GetPlayerCaptured() == 2) {
					inputTwo = true;
				}
			}
		}

		if (SparkPoint2.gameObject != null) {
			if (SparkPoint2.GetComponent<SparkPoint>().GetParticles()) {
				if (SparkPoint2.GetComponent<SparkPoint>().GetPlayerCaptured() == 1) {
					inputOne = true;
				}
				else if (SparkPoint2.GetComponent<SparkPoint>().GetPlayerCaptured() == 2) {
					inputTwo = true;
				}
			}
		}

		if (SparkPoint3.gameObject != null) {
			if (SparkPoint3.GetComponent<SparkPoint>().GetParticles()) {
				if (SparkPoint3.GetComponent<SparkPoint>().GetPlayerCaptured() == 1) {
					inputOne = true;
				}
				else if (SparkPoint3.GetComponent<SparkPoint>().GetPlayerCaptured() == 2) {
					inputTwo = true;
				}
			}
		}

		if (SparkPoint4.gameObject != null) {
			if (SparkPoint4.GetComponent<SparkPoint>().GetParticles()) {
				if (SparkPoint4.GetComponent<SparkPoint>().GetPlayerCaptured() == 1) {
					inputOne = true;
				}
				else if (SparkPoint4.GetComponent<SparkPoint>().GetPlayerCaptured() == 2) {
					inputTwo = true;
				}
			}
		}

		if (SparkPoint5.gameObject != null) {
			if (SparkPoint5.GetComponent<SparkPoint>().GetParticles()) {
				if (SparkPoint5.GetComponent<SparkPoint>().GetPlayerCaptured() == 1) {
					inputOne = true;
				}
				else if (SparkPoint5.GetComponent<SparkPoint>().GetPlayerCaptured() == 2) {
					inputTwo = true;
				}
			}
		}

		if (SparkPoint6.gameObject != null) {
			if (SparkPoint6.GetComponent<SparkPoint>().GetParticles()) {
				if (SparkPoint6.GetComponent<SparkPoint>().GetPlayerCaptured() == 1) {
					inputOne = true;
				}
				else if (SparkPoint6.GetComponent<SparkPoint>().GetPlayerCaptured() == 2) {
					inputTwo = true;
				}
			}
		}

		if (SparkPoint7.gameObject != null) {
			if (SparkPoint7.GetComponent<SparkPoint>().GetParticles()) {
				if (SparkPoint7.GetComponent<SparkPoint>().GetPlayerCaptured() == 1) {
					inputOne = true;
				}
				else if (SparkPoint7.GetComponent<SparkPoint>().GetPlayerCaptured() == 2) {
					inputTwo = true;
				}
			}
		}

		if (SparkPoint8.gameObject != null) {
			if (SparkPoint8.GetComponent<SparkPoint>().GetParticles()) {
				if (SparkPoint8.GetComponent<SparkPoint>().GetPlayerCaptured() == 1) {
					inputOne = true;
				}
				else if (SparkPoint8.GetComponent<SparkPoint>().GetPlayerCaptured() == 2) {
					inputTwo = true;
				}
			}
		}

		if (!particlesOn && inputOne && inputTwo) {
			particleSystem.startColor = Color.magenta;
			particleSystem.Play();
			particlesOn = true;
		}

		if (particlesOn && !captured) {
			captureTimer += Time.deltaTime;
			if (captureTimer >= 3.0f) {
				captured = true;
                NetMgr.SpawnBoss();
				Explode();
			}
			else if (captureTimer >= 2.5f) {
				particleSystem.startSize = 5.0f;
			}
			else {
				particleSystem.startSize += Time.deltaTime/2;
			}
		}
	}

	void Explode() {
		particleSystem.Stop();
		this.gameObject.renderer.material.color = Color.grey;
		Destroy (this.gameObject);
	}

	public bool GetParticles() {
		return particlesOn;
	}
}
