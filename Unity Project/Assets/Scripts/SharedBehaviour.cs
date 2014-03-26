using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SharedBehaviour : MonoBehaviour {
	
	public static SharedBehaviour current;
	
	public int currentLevel = 0;
	public bool isSingle = true;
	
	void Awake()
	{
		if(current != null && current != this)
		{
			Destroy(gameObject);
		}
		else
		{
			DontDestroyOnLoad(gameObject);
			current = this;
		}
	}

	void Start(){
		print (PlayerPrefs.GetInt ("HighestLevel"));
		//PlayerPrefs.SetInt("HighestLevel", 0);
	}
}