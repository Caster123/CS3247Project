using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {	
	float rad = 50;
	float turnSpeed = 120;
	float upSpeed = 60;

	void Start(){

	}

	void Update(){
        transform.Translate(0, Input.GetAxis("Vertical") * upSpeed * Time.deltaTime, 0);

        var tempH = Input.GetAxis("Horizontal");
        transform.Rotate(0, -tempH * Mathf.Sin(turnSpeed * Time.deltaTime), 0);
		print (tempH);
        var tempH2 = Input.GetAxis("Horizontal");
        transform.Translate(tempH2 * Mathf.Sin(rad * Time.deltaTime), 0, 0);
        
	}

}
