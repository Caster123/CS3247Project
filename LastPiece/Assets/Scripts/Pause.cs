// Used to pause and un-pause the game
// Attached on pause button

using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {

	public Texture2D up;
	public Texture2D down;
	bool pause;
	// Use this for initialization
	void Start () {
		pause = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown() {
		if (pause){
			setPadTouched();
			pause = false;
			this.guiTexture.texture = up;
		}
		else{
			setPadTouched();
			pause = true;
			this.guiTexture.texture = down;
		}
	}

	void OnMouseUp(){
		setPadUntouched ();
	}

	void setPadTouched()
	{
		GameObject target = GameObject.Find("PAD");
		Pad p = target.GetComponent<Pad>();
		p.setPadTouched();
	}
	
	void setPadUntouched()
	{
		GameObject target = GameObject.Find("PAD");
		Pad p = target.GetComponent<Pad>();
		p.setPadUntouched();
	}

	public bool check(){
		return pause;
	}
}
