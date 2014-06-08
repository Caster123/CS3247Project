// Used to update timer display in turn
// Used in multi-player mode

using UnityEngine;
using System.Collections;

public class TimerMulti : MonoBehaviour {

	public float timeLimit = 120.0f;
	public int layerNum = 10;
	//public float timeToWait = 1.5f;
	public int level = 1;
	private float[] timeRemaining;
	private int player;
	private GUIText[] TIMER;
	// Use this for initialization
	void Start () {
		timeRemaining = new float[2];
		timeRemaining[0] = timeLimit;
		timeRemaining[1] = timeLimit;
		TIMER = new GUIText[2];
		TIMER[0] = GameObject.Find("Timer0").guiText;
		TIMER[1] = GameObject.Find("Timer1").guiText;
		player = 0;
		TIMER[0].text = timeRemaining[0].ToString ("0.0");
		TIMER[1].text = timeRemaining[1].ToString ("0.0");
		SharedBehaviour.current.currentLevel = level;
	}

	// Update is called once per frame
	void Update () {
		if (!isPause()) 
{
			timeRemaining[player] -= Time.deltaTime;
			if (timeRemaining[player] < 10.0){
				TIMER[player].color = Color.red;
			}
			if (timeRemaining[player] > 0.0)
				TIMER[player].text = timeRemaining[player].ToString ("0.0");
			else
				StartCoroutine (Win(1-player));
		}
	}
	
	bool isPause(){
		GameObject target = GameObject.Find("Pause");
		Pause p = target.GetComponent<Pause>();
		return p.check();
	}

	public int getPlayer(){
		return player;
	}

	public void changePlayer(){
		player = 1 - player;
	}

	public void checkForFailure(int realPlayer) {
		GameObject[] goArray = FindObjectsOfType(typeof(GameObject)) as GameObject[];
		print (goArray.Length);
		for (int i = 0; i < goArray.Length; i++) {
			if (goArray[i].layer == layerNum) {
				print (i);
				if (onGround(goArray[i])){
					print ("Game End");
					StartCoroutine (Win(1-realPlayer));
				}
			}
			
		}
	}
	
	public bool onGround(GameObject go){
		RaycastHit hit;
		Physics.Raycast (go.transform.position,
		                 Vector3.up * -1,
		                 out hit);
		return (hit.transform.gameObject.tag == "Plane" && hit.distance < go.transform.renderer.bounds.size.y/2 + 0.001);
	}

	public void addRemove(int realPlayer)
	{
	}
	
	public IEnumerator updateDisplay (int realPlayer) {
		yield return new WaitForSeconds(0.0f);
		print ("Start checking");
		checkForFailure(realPlayer);
	}

	public IEnumerator Win(int player) {
		yield return new WaitForSeconds (0.0f);
		if (player == 0)
			Application.LoadLevel("Player1Win");
		else
			Application.LoadLevel("Player2Win");
	}
}
