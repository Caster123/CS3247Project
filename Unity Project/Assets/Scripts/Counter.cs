using UnityEngine;
using System.Collections;

public class Counter : MonoBehaviour {

	public int numTarget;
	private int numRemoved;
	// Use this for initialization
	void Start () {
		numRemoved = 0;
		updateDisplay ();
	}
	
	// Update is called once per frame
	void updateDisplay () {
		guiText.text = "Remaining: "+(numTarget - numRemoved);
	}

	public void addRemove()
	{
		numRemoved++;
		updateDisplay();
		//
		if (numRemoved == numTarget) {
			print ("Game Success!");
			StartCoroutine (Success());
		}
	}

	public IEnumerator Success() {
		yield return new WaitForSeconds (0.5f);
		Application.LoadLevel("Success");
	}

}
