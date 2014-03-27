using UnityEngine;
using System.Collections;
using System;

public class Brick_Glass : MonoBehaviour {

	// Use this for initialization
	void Start()
	{
		rigidbody.SetDensity((float)Convert.ToInt32(this.tag));
	}
	
		
	// Update is called once per frame
	void Update()
	{
	}
	
	void OnMouseDown()
	{

	}
}
