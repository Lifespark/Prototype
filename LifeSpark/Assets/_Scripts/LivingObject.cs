using UnityEngine;
using System.Collections;

public class LivingObject : MonoBehaviour {
    [HideInInspector]
    public int health;
    public delegate void OnhealthChanged(int hp);
    public OnhealthChanged onHealthChanged;
    protected bool isInited = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
