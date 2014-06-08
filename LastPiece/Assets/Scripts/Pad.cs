// Used to for Pad area
// Attached on Pad

using UnityEngine;
using System.Collections;

public class Pad : MonoBehaviour {
	bool isTouched = false;
	// Use this for initialization
	void Start () {
		// Make it invisible
		this.guiTexture.texture = null;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		isTouched = true;
	}

	void OnMouseUp(){
		isTouched = false;
	}

	public void setPadTouched(){
		isTouched = true;
	}

	public void setPadUntouched(){
		isTouched = false;
	}

	public bool isPadTouched(){
		return isTouched;
	}
}
