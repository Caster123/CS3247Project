using UnityEngine;
using System.Collections;

public class SampleCodeUse : MonoBehaviour, DrawingAreaOnResult, DrawActivityOnResult
{
	private DrawingArea mDrawingArea;
	private Rect mDrawingAreaRect;
	private Rect mDrawActivityRect;
	public Texture2D imageTexture;

	void Start ()
	{
		ZLog.Log ("SampleCodeUse Start() method, Screen.height: " + Screen.height + " Screen.width: " + Screen.width);

		mDrawingAreaRect = new Rect ((Screen.width / 2) - 150, 0, 150, 150);
		mDrawActivityRect = new Rect ((Screen.width / 2) + 50, 0, 150, 150);

		byte[] bgImage;
		if (imageTexture != null) {
			bgImage = imageTexture.EncodeToPNG ();
		} else {
			bgImage = null;
		}

		//Let's ask Java for the Drawing Area
		mDrawingArea = DrawingArea.getInstance (this, false, bgImage);
		mDrawingArea.setSize (DrawingArea.WRAP_CONTENT, Screen.height / 2);
		mDrawingArea.setButtonVisibility (true, true, true, true, true, true, true, true, true);

		ZLog.Log ("SampleCodeUse Start() method - end");
	}

	void OnGUI ()
	{
		if (GUI.Button (mDrawingAreaRect, new GUIContent ("Hide/Show\n DrawingArea", "Hovering over Hide/Show"))) {
			mDrawingArea.toggleDrawingAreaVisibility ();
		}

		if (GUI.Button (mDrawActivityRect, new GUIContent ("StartActivity", "Hovering over StartActivity"))) {
			DrawActivity.getInstance (this, false, true, true, true, true, true, true, true, true, true).startDrawActivity ();           
		}

		if (Application.platform == RuntimePlatform.Android) {
			if (Input.GetKey (KeyCode.Escape)) {
				Application.Quit ();
				return;
			}
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	public void onDrawActivityResult (byte[] result)
	{
		// Assigning the result image to our boxman.
		ZLog.Log ("SampleCodeUse onDrawActivityResult");
		Texture2D texture = new Texture2D (100, 100);
		texture.LoadImage (result);
		GameObject.FindWithTag ("BoxMan").renderer.material.mainTexture = texture;
	}

	public void onDrawActivityCanceled ()
	{
		ZLog.Log ("SampleCodeUse onDrawActivityCanceled");
	}

	public void onDrawingAreaResult (byte[] result)
	{
		// Assigning the result image to our boxman.
		ZLog.Log ("SampleCodeUse onDrawingAreaResult");
		Texture2D texture = new Texture2D (100, 100);
		texture.LoadImage (result);
		GameObject.FindWithTag ("BoxMan").renderer.material.mainTexture = texture;
	}

	public void onDrawingAreaCanvasInitialized ()
	{
		ZLog.Log ("SampleCodeUse onDrawingAreaCanvasInitialized");
	}

}