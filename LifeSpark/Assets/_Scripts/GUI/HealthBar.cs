using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {
    Boss m_boss;
    public UISprite fg;
    public GameObject root;
    public enum objType {
        player,
        boss,
        none,
    }
    public Boss boss {
        get {
            if (this.m_boss == null) {

            }
            return this.m_boss;
        }
        set {
            m_boss = value;
            m_boss.onHealthChanged = changeHealth;
            traget = m_boss.gameObject.transform;
        }
    }
    public Camera uicamera;
    public Transform traget;
    public objType thisType = objType.none;
    public Camera MainCamera;
    void Update() {
        if (traget == null) {
            GameObject.DestroyImmediate(root);
            return;
        }
        Vector3 pos = Camera.main.WorldToViewportPoint(traget.position);

        if (true) {
            fg.transform.position = GUIManager.guiManager.m_uiCamera.ViewportToWorldPoint(pos);
            pos = fg.transform.localPosition;
            pos.x = Mathf.RoundToInt(pos.x);
            pos.y = Mathf.RoundToInt(pos.y);
            pos.z = 0f;
            fg.transform.localPosition = pos;
        }

    }

    void changeHealth(int h) {
        switch (thisType) {
            case objType.boss:
                if (boss == null) {
                    Debug.LogWarning("UI_Missing boss");
                    return;
                }
                if (h <= 0) {
                    if (root != null) {
                        DestroyImmediate(root);
                    }
                }
                //TODO: change color
                float length = 100f * h / 100;
                Vector3 scale = fg.transform.localScale;
                scale = new Vector3(length, scale.y, scale.z);
                fg.transform.localScale = scale;


                break;
            case objType.player:

                break;
        }
    }

    // Use this for initialization
    void Start() {


    }


}
