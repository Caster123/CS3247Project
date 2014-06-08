// Used to load the previous scene
// Most likely Main Menu

using UnityEngine;
using System.Collections;

public class BackToMenu : MonoBehaviour {
	public string sceneName = "Main Menu";
	// Use this for initialization
	void Start () {
		if (SharedBehaviour.current.currentLevel == 0) {
						SharedBehaviour.current.isSingle = true;
						sceneName = "Main Menu";
				}
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
