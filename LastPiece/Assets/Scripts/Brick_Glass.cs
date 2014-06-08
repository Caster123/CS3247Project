// Used for special blocks
// To set the density and load failure scene once fall on the ground

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
		// To check whether on the ground
		if (onGround ())
		{
			// Load failure scene
			print ("Game End");
			if (isSingle())
			{
				StartCoroutine (Fail());
			}
			else
			{
				GameObject target = GameObject.Find ("TimerMulti");
				TimerMulti tm = target.GetComponent<TimerMulti>();
				print (tm.getPlayer());
				StartCoroutine(tm.Win(tm.getPlayer()));
			}
		}
	}
	
	void OnMouseDown()
	{

	}

	bool isSingle()
	{
		return (GameObject.Find ("TimerMulti") == null);
	}

	// Use raycast to check whether on the ground
	public bool onGround(){
		RaycastHit hit;
		Physics.Raycast (transform.position,
		                 Vector3.up * -1,
		                 out hit);
		return (hit.transform.gameObject.tag == "Plane" && hit.distance < transform.renderer.bounds.size.y/2 + 0.001);
	}

	public IEnumerator Fail() {
		yield return new WaitForSeconds (0.3f);
		Application.LoadLevel("Fail");
	}
}
