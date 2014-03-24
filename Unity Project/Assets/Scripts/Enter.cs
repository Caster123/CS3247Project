using UnityEngine;
using System.Collections;

public class Enter : MonoBehaviour {
	
	// Use this for initialization
	public AudioClip hit = Resources.Load("building crush") as AudioClip;
	public string levelName = "Level5";
	// Update is called once per frame
	void Update()
	{
	}
	
	void OnMouseDown()
	{
		//Destroy(this.gameObject);
		//Application.LoadLevel("basic");
		//Application.LoadLevel("1st");
		StartCoroutine(Wait(0.5F));
		//Application.LoadLevel("basic");
	}
	
	IEnumerator Wait(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		Application.LoadLevel(levelName);
	}    

}
