using UnityEngine;
using System.Collections;

public class PauseSign : MonoBehaviour {

	// Use this for initialization
	void Start () {
		guiTexture.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isPause ()){
			guiTexture.enabled = true;
		}
		else{
			guiTexture.enabled = false;
		}
	}

	bool isPause()
	{
		GameObject target = GameObject.Find("Pause");
		Pause p = target.GetComponent<Pause>();
		return p.check();
	}
}
