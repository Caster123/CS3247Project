using UnityEngine;
using System.Collections;

public class CenterMovement : TouchLogic {

	// Use this for initialization
	public Transform target;
	public Transform camera;
	public static float speed = 1.0f;
	public float upperBound = 8;
	public float lowerBound = 0;
	public float distance = 20.0f;
	void Start () {
	}
	
	// Update is called once per frame
	public override void OnTouchBegan()
	{
		//need to cache the touch index that started on the pad so others wont interfere
		float tempSpeed = speed;
		if (transform.tag == "Up") {
			print ("HELLO FROM UP BUTTON!");
						if (target.transform.position.y + speed >= upperBound)
								tempSpeed = upperBound - target.transform.position.y;
						target.transform.position += new Vector3 (0, tempSpeed, 0);
		}
		if (transform.tag == "Down") {
			print ("HELLO FROM DOWN BUTTON!");
			if (target.transform.position.y - speed <= lowerBound)
				tempSpeed = target.transform.position.y - lowerBound;
			target.transform.position -= new Vector3(0,tempSpeed,0);
		}

		Vector3 angles = camera.transform.eulerAngles;
		float x = angles.y;
		float y = angles.x;
		
		// Make the rigid body not change rotation
		if (rigidbody)
			rigidbody.freezeRotation = true;
		
		Quaternion rotation = Quaternion.Euler(y, x, 0);
		Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
		Vector3 position = rotation * negDistance + target.position;
		
		camera.transform.rotation = rotation;
		camera.transform.position = position;
	}
}
