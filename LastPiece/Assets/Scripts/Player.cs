﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {	
	float rad = 70;
	float turnSpeed = 70;
	float upSpeed = 60;
    float pushForce = 10;


    Transform pushLazer;

	void Start(){
	}

	void Update(){
        transform.Translate(0, Input.GetAxis("Vertical") * upSpeed * Time.deltaTime, 0);

        var tempH = Input.GetAxis("Horizontal");
        transform.Rotate(0, -tempH * Mathf.Sin(turnSpeed * Time.deltaTime), 0);
		//print (tempH);
        var tempH2 = Input.GetAxis("Horizontal");
        transform.Translate(tempH2 * Mathf.Sin(turnSpeed * Time.deltaTime), 0, 0);

	}
}
