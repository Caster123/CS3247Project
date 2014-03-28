using UnityEngine;
using System.Collections;

public class Restart : MonoBehaviour {
	
	// Use this for initialization
	public AudioClip hit = Resources.Load("building crush") as AudioClip;
	//public AudioClip voice;
	string levelName;

	void Start(){
	}
	// Update is called once per frame
	void Update(){
	}
	
	void OnMouseDown(){
		audio.PlayOneShot(hit);

		if (SharedBehaviour.current.currentLevel == 0)
			levelName = "Tutorial";
		else{
			if (SharedBehaviour.current.isSingle)
				levelName = "level" + SharedBehaviour.current.currentLevel;
			else
				levelName = "level" + SharedBehaviour.current.currentLevel + "Multi";
		}
		print (levelName);

		this.gameObject.rigidbody.isKinematic = false;
		this.gameObject.rigidbody.AddRelativeForce(-Vector3.forward * 1000);
		transform.Rotate(3f, 30f, 0);
		//Destroy(this.gameObject);
		//Application.LoadLevel("basic");
		//Application.LoadLevel("1st");
		StartCoroutine(Wait(1.0F));
		//Application.LoadLevel("basic");
	}
	
	IEnumerator Wait(float waitTime){
		yield return new WaitForSeconds(waitTime);
		Application.LoadLevel(levelName);
	}    

}
