using UnityEngine;
using System.Collections;

public class HitIntroMan : MonoBehaviour {
    Texture2D c1;
    Texture2D c2;
    Texture2D c3;
    Texture2D c4;
    Texture2D c5;
	// Use this for initialization
	void Start () {
        c1 = (Texture2D)Resources.Load("First Scene_1");
        c2 = (Texture2D)Resources.Load("First Scene_2");
        c3 = (Texture2D)Resources.Load("First Scene_3");
        c4 = (Texture2D)Resources.Load("First Scene_4");
        c5 = (Texture2D)Resources.Load("First Scene_5");
	}
	
	// Update is called once per frame
	void Update () {
	}

    void MouseDown()
    {
        GameObject.Find("1_Man").guiTexture.texture = c2;
    }
}
