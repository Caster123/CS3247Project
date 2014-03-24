using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

	public float timeLimit = 120.0f;
	private float timeRemaining;
	// Use this for initialization
	void Start () 
	{
		timeRemaining = timeLimit;
	}

	// Update is called once per frame
	void Update () 
	{
		if (!isPause()) 
		{
			timeRemaining -= Time.deltaTime;
			if (timeRemaining > 0.0)
				guiText.text = "Time left: " + timeRemaining.ToString ("0.0");
			else
				StartCoroutine (Fail());
		}
	}
	
	bool isPause()
	{
		GameObject target = GameObject.Find("Pause");
		Pause p = target.GetComponent<Pause>();
		return p.check();
	}

	public IEnumerator Fail() {
		yield return new WaitForSeconds (0.5f);
		Application.LoadLevel("Fail");
	}
}
