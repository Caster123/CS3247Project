// Used to load next scene for Tutorial and introduction section

using UnityEngine;
using System.Collections;

public class LoadNextScene : MonoBehaviour {
	public string sceneName;
	
	// Use this for initialization
	void Start()
	{
	}
	
	// Update is called once per frame
	void Update()
	{
		guiTexture.pixelInset =  new Rect(0, 0, Screen.width, Screen.height);
	}
	
	void OnMouseDown()
	{
		StartCoroutine(Wait(0.1F));
	}
	
	IEnumerator Wait(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		Application.LoadLevel(sceneName);
	}
}
