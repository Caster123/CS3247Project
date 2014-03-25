using UnityEngine;
using System.Collections;

public class EnergyTimer : MonoBehaviour {
	public float timeToWait = 3.0f;
	private float timeRemaining;
	private bool ready;
	// Use this for initialization
	void Start () 
	{
		timeRemaining = 0.0f;
		ready = true;
		guiText.text = "READY";
	}

	public void Reset()
	{
		timeRemaining = timeToWait;
		guiText.text = timeRemaining.ToString ("0.0");
		ready = false;
	}

	public bool isReady()
	{
		return ready;
	}

	// Update is called once per frame
	void Update () 
	{
		if (!isPause()) 
		{
			if (!ready)
			{
				timeRemaining -= Time.deltaTime;
				if (timeRemaining > 0.0f)
					guiText.text = timeRemaining.ToString ("0.0");
				else
				{
					ready = true;
					guiText.text = "READY";
				}
			}
		}
	}
	
	bool isPause()
	{
		GameObject target = GameObject.Find("Pause");
		Pause p = target.GetComponent<Pause>();
		return p.check();
	}
}
