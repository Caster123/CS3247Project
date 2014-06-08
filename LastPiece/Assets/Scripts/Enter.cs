// Used to enter different levels
// Used in singe player mode and multiplayer mode

using UnityEngine;
using System.Collections;

public class Enter : MonoBehaviour {
	
	// Use this for initialization
	public AudioClip hit = Resources.Load("building crush") as AudioClip;
	public int level = 5;
	public Texture t;
	// Update is called once per frame
	void Start()
	{
		GameObject go = GameObject.Find (level.ToString());
		go.renderer.enabled = false;
	}

	void Update()
	{
		int highestLevel = PlayerPrefs.GetInt ("HighestLevel");
		if (highestLevel >= level) {
			MeshRenderer gameObjectRenderer = this.GetComponent<MeshRenderer>();
			Material newMaterial = new Material(Shader.Find("Diffuse"));
			newMaterial.mainTexture = t;
			gameObjectRenderer.material = newMaterial ;
			GameObject go = GameObject.Find (level.ToString());
			go.renderer.enabled = true;

			if (highestLevel > level) {
				string levelRemainingTime = "level" + level + "remaining";
				float largestRemainingTime = PlayerPrefs.GetFloat(levelRemainingTime);

				GameObject record = GameObject.Find (level.ToString()+"Time");

				TextMesh textMesh = (TextMesh) record.GetComponent(typeof(TextMesh));
				if (largestRemainingTime < 1000000.0f)
					textMesh.text = "Record:" + largestRemainingTime.ToString ("0.0");
				else
					textMesh.text = "Record:XX.X";
				textMesh.characterSize = 0.75f;
			}
		}
	}
	
	void OnMouseDown()
	{
		int highestLevel = PlayerPrefs.GetInt ("HighestLevel");
		print (highestLevel);
		// Need to check whether it is unlocked or not
        if (highestLevel >= level)
        {
            this.gameObject.rigidbody.AddRelativeForce(Vector3.forward * 1000);
			StartCoroutine(Wait(0.5F));
		}
	}
	
	IEnumerator Wait(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		if (SharedBehaviour.current.isSingle)
			Application.LoadLevel("Level" + level);
		else
			Application.LoadLevel("Level" + level + "Multi");
	}    

}
