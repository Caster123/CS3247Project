/*/
* NOTE: This script has been changed since the video recording
	* all additions are tagged with //[NEW]// read comments for details
		* Script by Devin Curry
		* www.Devination.com
		* www.youtube.com/user/curryboy001
		* Please like and subscribe if you found my tutorials helpful :D
			* Google+ Community: https://plus.google.com/communities/108584850180626452949
				* Twitter: https://twitter.com/Devination3D
				* Facebook Page: https://www.facebook.com/unity3Dtutorialsbydevin
				* Attach this script to a GUITexture for a trackpad area OR attach to anything and change all the OnTouch functions to OnTouchAnywhere functions
					/*/
using UnityEngine;
using System.Collections;

public class SwipeMovement : TouchLogic 
{
	public float upperBound = 8;
	public float lowerBound = 0;
	public float moveSpeed = 100.0f;
	public int invertPitch = 1;
	public Transform center;
	private float pitch = 0.0f;
	//[NEW]//cache initial rotation of player so pitch and yaw don't reset to 0 before rotating
	
	//[NEW]//
	void Start()
	{
		//cache original rotation of player so pitch and yaw don't reset to 0 before rotating
	}
	//[NEW]//
	void OnTouchBegan()
	{
		//need to cache the touch index that started on the pad so others wont interfere
		touch2Watch = TouchLogic.currTouch;
	}
	
	public void OnTouchMoved()
	{
		var yDelta = Input.GetTouch(touch2Watch).deltaPosition.y * moveSpeed * invertPitch * Time.deltaTime;

		//do the rotations of our camera 
		if (center.transform.position.y + yDelta <= lowerBound) {
			yDelta = lowerBound-center.transform.position.y;
		}
		if (center.transform.position.y + yDelta >= upperBound) {
			yDelta = upperBound-center.transform.position.y;
		}
		center.transform.position += new Vector3(0,yDelta,0);
	}
	//[NEW]//
	void OnTouchEndedAnywhere()
	{
		//the || condition is a failsafe
		if(TouchLogic.currTouch == touch2Watch || Input.touches.Length <= 0)
			touch2Watch = 64;
	}
}