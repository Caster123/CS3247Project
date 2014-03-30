using UnityEngine;
using System.Collections;

public class Enter : MonoBehaviour {
	
	// Use this for initialization
	public AudioClip hit = Resources.Load("building crush") as AudioClip;
	public int level = 5;
	public Texture t;
	//string title;
	//public Color originalColor = new Color(1.0f,1.0f,1.0f);
	//public Color highlightColor = new Color(0.5f,0.0f,0.3f);
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
			//newMaterial.color = Color.black;
			gameObjectRenderer.material = newMaterial ;
			GameObject go = GameObject.Find (level.ToString());
			go.renderer.enabled = true;
		}
	}
	
	void OnMouseDown()
	{
		int highestLevel = PlayerPrefs.GetInt ("HighestLevel");
		print (highestLevel);
		if (highestLevel >= level) {
			//Destroy(this.gameObject);
			//Application.LoadLevel("basic");
			//Application.LoadLevel("1st");
			StartCoroutine(Wait(0.5F));
			//Application.LoadLevel("basic");
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
