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
			pause = false;
			this.guiTexture.texture = up;
		}
		else{
			pause = true;
			this.guiTexture.texture = down;
		}
	}

	public bool check(){
		return pause;
	}
}
