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
		if ((Input.GetMouseButtonDown (0) && !continuing) || (!Input.GetMouseButtonUp(0)&&continuing)) {
						if (continuing) {
								//timeAtButtonUp = timeCurrent;
								//timeButtonHeld = (timeAtButtonUp - timeAtButtonDown);
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
					//print (angle);
									Vector3 v = new Vector3(Mathf.Cos(angle/180.0f*Mathf.PI) * rad, yConstant, Mathf.Sin(angle/180.0f*Mathf.PI) * rad);
									player.transform.position = v;
									//player.transform.Translate(-rad*Mathf.Sin(turnSpeed) , 0, 0);
					/*float deltaZ = 0.1f;
					float maxZ = 30.0f;
					if (Mathf.Abs(player.transform.rotation.z) < maxZ)
									player.transform.Rotate (0,0,deltaZ);*/
								}
								if (this.name == "Right") {
									player.transform.Rotate(0, -turnSpeed, 0);
									angle+=turnSpeed;
									if (angle > 360) angle-=360;
									//print (Mathf.Cos(180));
									Vector3 v = new Vector3(Mathf.Cos(angle/180.0f*Mathf.PI) * rad, player.transform.position.y, Mathf.Sin(angle/180.0f*Mathf.PI) * rad);
									player.transform.position = v;
									//player.transform.Translate(rad*Mathf.Sin(turnSpeed) , 0, 0);
								}
						}
				} else {
			//print ("Up");
						if (continuing) {
								//timeAtButtonUp = timeCurrent;
								//timeButtonHeld = (timeAtButtonUp - timeAtButtonDown);
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
									//player.transform.Translate(-rad*Mathf.Sin(turnSpeed) , 0, 0);
								}
								if (this.name == "Right") {
									player.transform.Rotate(0, -turnSpeed, 0);
									angle+=turnSpeed;
									if (angle > 360) angle-=360;
					Vector3 v = new Vector3(Mathf.Cos(angle/180.0f*Mathf.PI) * rad, yConstant, Mathf.Sin(angle/180.0f*Mathf.PI) * rad);
									player.transform.position = v;
									//player.transform.Translate(rad*Mathf.Sin(turnSpeed) , 0, 0);
								}
								continuing = false;
						}
				}
	}

	void OnMouseDown()
	{
		//print (this.name);
		setPadTouched ();
		if (!isPause ()){
			this.guiTexture.texture = down;
			timeAtButtonDown = timeCurrent;
			continuing = true;
			angle = Mathf.Atan2 (player.transform.position.z, player.transform.position.x)*Mathf.Rad2Deg;
			//print (angle);
		}
	}

	void OnMouseUp()
	{
		this.guiTexture.texture = up;
		setPadUntouched ();
	}

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
