// Used to enter next level
// Attached on the nextLevel button
// Used in Success scene

using UnityEngine;
using System.Collections;

public class NextLevel : MonoBehaviour {
	
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
		int nextLevel = SharedBehaviour.current.currentLevel + 1;
		if (SharedBehaviour.current.isSingle)
			levelName = "level" + nextLevel;
		else
			levelName = "level" + nextLevel + "Multi";
		if (nextLevel > 18)
			levelName = "Final1";
		print (levelName);

		this.gameObject.rigidbody.isKinematic = false;
		this.gameObject.rigidbody.AddRelativeForce(-Vector3.forward * 1000);
		transform.Rotate(3f, 30f, 0);
		StartCoroutine(Wait(1.0F));
	}
	
	IEnumerator Wait(float waitTime){
		yield return new WaitForSeconds(waitTime);
		Application.LoadLevel(levelName);
	}    

}
