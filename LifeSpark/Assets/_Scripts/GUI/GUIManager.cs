using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GUIManager  {
	private static GUIManager m_guiManager;
	public Camera m_uiCamera;
	UIPanel m_uipanel;
	List<GameObject> independceObjectsList;
	public static GUIManager guiManager {
		get {
			if(m_guiManager == null)
			{
				m_guiManager = new GUIManager();
			}
			return m_guiManager;
		}

	}
	private GUIManager ()
	{
		Debug.Log ("init gui Mgr");
		GameObject g =  GameObject. Instantiate(Resources.Load ("UI", typeof(GameObject))) as GameObject;
		m_uipanel = g.GetComponentInChildren<UIPanel> ();
		independceObjectsList = new List<GameObject> ();
		m_uiCamera = g.GetComponentInChildren<Camera> ();
	}



    public void addHealthBar(LivingObject boss)
	{
		GameObject g =   GameObject.Instantiate(Resources.Load ("healthBar", typeof(GameObject))) as GameObject;
		g.transform.parent = m_uipanel.transform;
		g.transform.localScale = Vector3.one;
		HealthBar h = g.GetComponent<HealthBar> ();
		h.boss = boss;
		h.thisType = HealthBar.objType.boss;
		
	}

}
