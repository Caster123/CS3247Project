﻿using UnityEngine;
using System.Collections;

public class BackToMenu : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
			Application.LoadLevel("Main Menu");
	}

	void OnMouseDown () {
		Application.LoadLevel("Main Menu");
	}
}