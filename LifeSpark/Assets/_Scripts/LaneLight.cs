﻿using UnityEngine;
using System.Collections;

public class LaneLight : MonoBehaviour {

	public GameObject SparkPoint1;
	public GameObject SparkPoint2;
	//Color defaultColor;
	Material clearMaterial;
	Material litMaterial;

	// Use this for initialization
	void Start () {
		//defaultColor = new Color(133.0f/255,255.0f/255,164.0f/255);
		clearMaterial = (Material)Resources.Load ("Clear");
		litMaterial = gameObject.renderer.material;
	}
	
	// Update is called once per frame
	void Update () {
		if (SparkPoint1.gameObject != null && SparkPoint2.gameObject != null) {
			if (SparkPoint1.GetComponent<SparkPoint>().GetParticles() && SparkPoint2.GetComponent<SparkPoint>().GetParticles()) {
                if (SparkPoint1.renderer.material.color == SparkPoint2.renderer.material.color) {
                    /*
					if (SparkPoint1.GetComponent<SparkPoint>().GetPlayerCaptured() == 1) {
						this.gameObject.renderer.material.color = Color.red;
					}
					else if (SparkPoint1.GetComponent<SparkPoint>().GetPlayerCaptured() == 2) {
						this.gameObject.renderer.material.color = Color.blue;
					}*/
					gameObject.renderer.material = litMaterial;
                    gameObject.renderer.material.color = SparkPoint1.renderer.material.color;

					if (SparkPoint1.GetComponent<SparkPoint>().GetConnectedToOrigin() || SparkPoint2.GetComponent<SparkPoint>().GetConnectedToOrigin())
					{
						SparkPoint1.GetComponent<SparkPoint>().SetConnectedToOrigin();
						SparkPoint2.GetComponent<SparkPoint>().SetConnectedToOrigin();
					}
				}
				else {
					this.gameObject.renderer.material = clearMaterial;
				}
			}
			else {
				this.gameObject.renderer.material = clearMaterial;
			}
		}
		else {
			this.gameObject.renderer.material = clearMaterial;
		}
	}
}
