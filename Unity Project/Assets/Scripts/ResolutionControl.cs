using UnityEngine;
using System.Collections;

public class ResolutionControl : MonoBehaviour {
	Rect originalSettings;
	int originalFont;
	// Use this for initialization
	void Start () {
		if (guiTexture!=null)
			originalSettings = guiTexture.pixelInset;
		if (guiText!=null)
			originalFont = guiText.fontSize;
	}
	
	// Update is called once per frame
	void Update () {
		//print (Screen.width + " " +  Screen.height);
		//print (originalSettings);
		if (guiTexture!=null)
			guiTexture.pixelInset =  new Rect(0, 0, originalSettings.width*Screen.width/1203.0f, originalSettings.height*Screen.height/752.0f);
		if (guiText!=null)
			guiText.fontSize = (int)(originalFont * Screen.width / 1203.0f);
		//print (guiTexture.pixelInset);
	}
}
