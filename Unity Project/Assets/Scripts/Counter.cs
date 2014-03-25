using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Counter : MonoBehaviour {
	public float timeToWait = 3.0f;
	public int numTarget,layerNum;
	public int level = 0;
	private int numRemoved;
	private List<string> pieceOnGround = new List<string>();
	private GameObject[] pieces;
	bool fail = false;
	// Use this for initialization
	void Start () {
		numRemoved = 0;
		//print (layerNum);
		//print ("EXE");
		StartCoroutine(updateDisplay ());
		SharedBehaviour.current.currentLevel = level;
	}

	void checkForFailure() {
		GameObject[] goArray = FindObjectsOfType(typeof(GameObject)) as GameObject[];
		//print (goArray.Length);
		for (int i = 0; i < goArray.Length; i++) {
			//print(goArray[i].name + goArray[i].layer);
			//print(layerNum);
			if (goArray[i].layer == layerNum) {
				//print (layerNum);
				if (onGround(goArray[i])){
					print ("Game End");
					fail = true;
					StartCoroutine (Fail());
				}
			}

		}
	}

	public bool onGround(GameObject go){
		//print ("fuck");
		/*return Physics.Raycast (go.transform.position,
		                        go.transform.up * -1,
		                        go.transform.renderer.bounds.size.y, 1 << LayerMask.NameToLayer ("Ground"));*/
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
		//
	}

	public IEnumerator updateDisplay () {
		guiText.text = (numTarget - numRemoved).ToString();
		yield return new WaitForSeconds(timeToWait);
		print ("Start checking");
		checkForFailure();
		if (!fail)
			if (numRemoved == numTarget) {
				print ("Game Success!");
				//if (SharedBehaviour != null)
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
