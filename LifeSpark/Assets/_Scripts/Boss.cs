using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour {
    public int health = 100;
    public delegate void OnhealthChanged(int hp);
    public OnhealthChanged onHealthChanged;
    bool isInited = false;
    // Use this for initialization
    void Start() {

    }

    void Awake() {
        if (isInited)
            return;
        GUIManager.guiManager.addHealthBar(this);
        isInited = true;
    }

    // Update is called once per frame
    void Update() {
        if (onHealthChanged != null) {
            onHealthChanged(health);
        }
    }

    [RPC]
    void ApplyDamage(int pDamage) {
        health -= pDamage;
        if (health <= 0)
            Network.Destroy(networkView.viewID);
        Debug.Log(health);

    }

    void OnGUI() {
        GUI.Label(new Rect(10, 10, 100, 50), "BOSS health: " + health);
    }
}
