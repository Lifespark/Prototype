using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	GameObject texts;
	Transform[] text;

	// Use this for initialization
	void Start () {
		texts = GameObject.FindGameObjectWithTag("Text");
		text = texts.GetComponentsInChildren<Transform>();
		for (int i = 0; i < text.Length; i++) {
			text[i].renderer.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {

	}
}
