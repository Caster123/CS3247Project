// Used to control the view
// Attached onto the buttons

using UnityEngine;
using System.Collections;

public class Control : MonoBehaviour {
	
	public float turnSpeed = 120;
	public float upSpeed = 60;
	public float UpperBound = 100.0f;
	public float LowerBound = 0.0f;
	public Texture2D up;
	public Texture2D down;

	private Transform player;
	private Transform center;
	
	float timeCurrent;
	float timeAtButtonDown ; 
	float timeAtButtonUp ;
	float timeButtonHeld = 0 ;
	float rad;
	bool continuing = false;
	float angle = 0.0f;
	float yConstant;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player").transform;
		center = GameObject.Find ("Center").transform;
		// radius, angle and y are recorded
		rad = Mathf.Sqrt ((center.transform.position.x - player.transform.position.x)
						* (center.transform.position.x - player.transform.position.x)
						+ (center.transform.position.z - player.transform.position.z)
						* (center.transform.position.z - player.transform.position.z));
		angle = Mathf.Atan2 (player.transform.position.z, player.transform.position.x) * Mathf.Rad2Deg;
		yConstant = player.transform.position.y;

	}
	
	// Update is called once per frame
	void Update () {
		yConstant = player.transform.position.y;
		timeCurrent = Time.fixedTime;
		// Use math to compute the new positions
		if ((Input.GetMouseButtonDown (0) && !continuing) || (!Input.GetMouseButtonUp(0)&&continuing)) {
						if (continuing) {
								if (this.name == "Up") {
									float move = Mathf.Min(upSpeed, UpperBound - player.transform.position.y);
									player.transform.Translate (0, move, 0);
								}
								if (this.name == "Down") {
									float move = Mathf.Max(-upSpeed, LowerBound - player.transform.position.y);
									player.transform.Translate (0, move, 0);
								}
								if (this.name == "Left") {
									player.transform.Rotate(0, turnSpeed, 0);
									angle-=turnSpeed;
									if (angle < 0) angle+=360;
									Vector3 v = new Vector3(Mathf.Cos(angle/180.0f*Mathf.PI) * rad, yConstant, Mathf.Sin(angle/180.0f*Mathf.PI) * rad);
									player.transform.position = v;
								}
								if (this.name == "Right") {
									player.transform.Rotate(0, -turnSpeed, 0);
									angle+=turnSpeed;
									if (angle > 360) angle-=360;
									Vector3 v = new Vector3(Mathf.Cos(angle/180.0f*Mathf.PI) * rad, player.transform.position.y, Mathf.Sin(angle/180.0f*Mathf.PI) * rad);
									player.transform.position = v;
								}
						}
				} else {
						if (continuing) {

								if (this.name == "Up") {
									float move = Mathf.Min(upSpeed, UpperBound - player.transform.position.y);
									player.transform.Translate (0, move, 0);
								}
								if (this.name == "Down") {
									float move = Mathf.Max(-upSpeed, LowerBound - player.transform.position.y);
									player.transform.Translate (0, move, 0);

								}
								if (this.name == "Left") {
									player.transform.Rotate(0, turnSpeed, 0);
									angle-=turnSpeed;
									if (angle < 0) angle+=360;
									Vector3 v = new Vector3(Mathf.Cos(angle/180.0f*Mathf.PI) * rad, yConstant, Mathf.Sin(angle/180.0f*Mathf.PI) * rad);
									player.transform.position = v;
								}
								if (this.name == "Right") {
									player.transform.Rotate(0, -turnSpeed, 0);
									angle+=turnSpeed;
									if (angle > 360) angle-=360;
									Vector3 v = new Vector3(Mathf.Cos(angle/180.0f*Mathf.PI) * rad, yConstant, Mathf.Sin(angle/180.0f*Mathf.PI) * rad);
									player.transform.position = v;
								}
								continuing = false;
						}
				}
	}

	void OnMouseDown()
	{
		setPadTouched ();
		if (!isPause ()){
			this.guiTexture.texture = down;
			timeAtButtonDown = timeCurrent;
			continuing = true;
			angle = Mathf.Atan2 (player.transform.position.z, player.transform.position.x)*Mathf.Rad2Deg;
		}
	}

	void OnMouseUp()
	{
		this.guiTexture.texture = up;
		setPadUntouched ();
	}

	// Check whether click in the Pad area
	void setPadTouched()
	{
		GameObject target = GameObject.Find("PAD");
		Pad p = target.GetComponent<Pad>();
		p.setPadTouched();
	}

	void setPadUntouched()
	{
		GameObject target = GameObject.Find("PAD");
		Pad p = target.GetComponent<Pad>();
		p.setPadUntouched();
	}

	bool isPause(){
		GameObject target = GameObject.Find("Pause");
		Pause p = target.GetComponent<Pause>();
		return p.check();
	}
}
