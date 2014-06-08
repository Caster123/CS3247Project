// Used to update counter display
// Attached on Counter

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Counter : MonoBehaviour {
	public float timeToWait = 1.5f;
	public int numTarget,layerNum;
	public int level = 0;
	private int numRemoved;
	private List<string> pieceOnGround = new List<string>();
	private GameObject[] pieces;
	bool fail = false;
	// Use this for initialization
	void Start () {
		numRemoved = 0;
		StartCoroutine(updateDisplay ());
		SharedBehaviour.current.currentLevel = level;
	}

	void checkForFailure() {
		GameObject[] goArray = FindObjectsOfType(typeof(GameObject)) as GameObject[];
		for (int i = 0; i < goArray.Length; i++) {
			if (goArray[i].layer == layerNum) {
				if (onGround(goArray[i])){
					print ("Game End");
					fail = true;
					StartCoroutine (Fail());
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


	public void addRemove()
	{
		numRemoved++;
		StartCoroutine(updateDisplay());
	}

	public IEnumerator updateDisplay () 
	{
		guiText.text = (numTarget - numRemoved).ToString();
		yield return new WaitForSeconds(0.0f);
		if (!fail)
			if (numRemoved == numTarget)
			{
				print ("Game Success!");
				int highestLevel = PlayerPrefs.GetInt("HighestLevel");
				if (level == highestLevel)
					PlayerPrefs.SetInt("HighestLevel", level+1);
				// update time
				GameObject timer = GameObject.Find ("Timer");
				float timeRemaining = timer.GetComponent<Timer>().getTimeRemaining();
				if (timeRemaining < PlayerPrefs.GetFloat("level" + level + "remaining"))
				{
					PlayerPrefs.SetFloat("level" + level + "remaining", timeRemaining);
					print ("UPDATED");
				}
				StartCoroutine (Success());
			}
	}

	public IEnumerator Success() {
		yield return new WaitForSeconds (1.5f);
		Application.LoadLevel("Success");
	}

	public IEnumerator Fail() {
		yield return new WaitForSeconds (0.5f);
		Application.LoadLevel("Fail");
	}
}
