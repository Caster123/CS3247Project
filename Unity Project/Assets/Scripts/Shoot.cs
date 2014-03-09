using UnityEngine;
using System.Collections;

public class Shoot : TouchLogic {

	public Transform camera;
	public Transform center;
	public float hoverForce = 100.0f;
	private Vector3 direction;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	public override void OnTouchBegan()
	{
		//need to cache the touch index that started on the pad so others wont interfere
		direction = center.transform.position - camera.transform.position;
		RaycastHit hit;
		if (Physics.Raycast (camera.transform.position, direction, out hit)) {
			float distanceToGround = hit.distance;
			//print(hit.collider.gameObject.tag);
			hit.collider.gameObject.rigidbody.AddForce(direction * hoverForce, ForceMode.Acceleration);
		}
				
	}
}

