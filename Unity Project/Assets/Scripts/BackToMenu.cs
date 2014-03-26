using UnityEngine;
using System.Collections;

public class BackToMenu : MonoBehaviour {
	public string sceneName = "Main Menu";
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
			Application.LoadLevel(sceneName);
	}

	void OnMouseDown () {
		Application.LoadLevel(sceneName);
	}
}
