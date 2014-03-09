using UnityEngine;
using System.Collections;

public class SwipeRotationPlayer : TouchLogic 
{
	public float rotateSpeed = 100.0f;
	public int invertPitch = 1;
	public Transform player;
	private float pitch = 0.0f,	yaw = 0.0f;
	public Transform target;
	public float distance = 20.0f;
	
	public float xSpeed = 125.0f;
	public float ySpeed = 60.0f;
	
	public float yMinLimit = 0f;
	public float yMaxLimit = 80f;
	
	private float x = 0.0f;
	private float y = 0.0f;
	
	//[NEW]//
	void Start () {
		Vector3 angles = player.transform.eulerAngles;
		x = angles.y;
		y = angles.x;
		
		// Make the rigid body not change rotation
		if (rigidbody)
			rigidbody.freezeRotation = true;

		Quaternion rotation = Quaternion.Euler(y, x, 0);
		Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
		Vector3 position = rotation * negDistance + target.position;
		
		player.transform.rotation = rotation;
		player.transform.position = position;
	}
	//[NEW]//
	public override void OnTouchBegan()
	{
		//need to cache the touch index that started on the pad so others wont interfere
		touch2Watch = TouchLogic.currTouch;
	}
	
	public override void OnTouchMoved()
	{
		x += Input.GetTouch(touch2Watch).deltaPosition.x * xSpeed * invertPitch * 0.02f * Time.deltaTime;
		y -= Input.GetTouch(touch2Watch).deltaPosition.y * ySpeed * invertPitch * 0.02f * Time.deltaTime;
		
		y = ClampAngle(y, yMinLimit, yMaxLimit);
		
		Quaternion rotation = Quaternion.Euler(y, x, 0);
		Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
		Vector3 position = rotation * negDistance + target.position;
		
		player.transform.rotation = rotation;
		player.transform.position = position;
	}
	//[NEW]//
	public override void OnTouchEndedAnywhere()
	{
		//the || condition is a failsafe
		if(TouchLogic.currTouch == touch2Watch || Input.touches.Length <= 0)
			touch2Watch = 64;
	}
	
	float ClampAngle (float angle, float min, float max) {
		if (angle < -360f)
			angle += 360f;
		if (angle > 360f)
			angle -= 360f;
		return Mathf.Clamp (angle, min, max);
	}
}
