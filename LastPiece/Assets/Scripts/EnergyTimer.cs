// Used to update energy bar
// Attached on energy timer

using UnityEngine;
using System.Collections;

public class EnergyTimer : MonoBehaviour {
	public float timeToWait = 3.0f;
	public Texture2D Ready;
	public Texture2D notReady;
	private float timeRemaining;
	private bool ready;
	Rect originalSettings;
	// Use this for initialization
	void Start () 
	{
		if (guiTexture!=null)
			originalSettings = guiTexture.pixelInset;
		//print (originalSettings);
		timeRemaining = 0.0f;
		ready = true;
		guiTexture.texture = Ready;
	}

	public void Reset()
	{
		timeRemaining = timeToWait;
		guiTexture.texture = notReady;
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
					guiTexture.pixelInset =  new Rect(0, 0, originalSettings.width*(timeToWait - timeRemaining)/timeToWait, originalSettings.height);
				else
				{
					ready = true;
					guiTexture.texture = Ready;
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
