using UnityEngine;
using System.Collections;

public class Restart : MonoBehaviour {
	
	// Use this for initialization
	public AudioClip hit = Resources.Load("building crush") as AudioClip;
	string levelName;
	// Update is called once per frame
	void Update()
	{
	}
	
	void OnMouseDown()
	{
		audio.PlayOneShot(hit);

		if (SharedBehaviour.current.currentLevel == 0)
			levelName = "Tutorial";
		else
			levelName = "level" + SharedBehaviour.current.currentLevel;
		print (levelName);

		this.gameObject.rigidbody.isKinematic = false;
		this.gameObject.rigidbody.AddRelativeForce(-Vector3.forward * 1000);
		transform.Rotate(3f, 30f, 0);
		//Destroy(this.gameObject);
		//Application.LoadLevel("basic");
		//Application.LoadLevel("1st");
		StartCoroutine(Wait(1.5F));
		//Application.LoadLevel("basic");
	}
	
	IEnumerator Wait(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		Application.LoadLevel(levelName);
	}    

}
