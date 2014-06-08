// Used to record some global variables
// currentLevel records the current level entered
// isSingle records whether it is single player mode

using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
//using PlayerPrefsX;

public class SharedBehaviour : MonoBehaviour {
	
	public static SharedBehaviour current;

	public int totalNumOfLevels = 18;
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
		//PlayerPrefs.SetInt ("HighestLevel", 0);
		print (PlayerPrefs.GetInt ("Played"));
		if (PlayerPrefs.GetInt("Played") == 0)
		{
			Application.LoadLevel("ManIntroduction1");
			string levelRemainingTime;
			for (int i = 1; i <= totalNumOfLevels; i++)
			{
				levelRemainingTime = "level" + i + "remaining";
				PlayerPrefs.SetFloat(levelRemainingTime, 1000001.0f);
			}
		}
		PlayerPrefs.SetInt ("Played", 1);
		//print (PlayerPrefs.GetInt ("HighestLevel"));
		//PlayerPrefs.SetInt("HighestLevel", 19);
	}
}