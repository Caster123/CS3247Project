using UnityEngine;
using System.Collections;

public class SharedBehaviour : MonoBehaviour {
	
	public static SharedBehaviour current;
	
	public int currentLevel = 0;
	
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
}