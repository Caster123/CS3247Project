using UnityEngine;
using System;

/** 
 * Class DrawActivity is a wrapper around a DrawActivity class from Java. It represents a single Activity, which contains an SCanvasView.
 * Every script that wants to use this class must implement DrawActivityOnResult interface. DrawActivity is a singleton - only one instance of this 
 * class can exist simultaneously. Acquire the instance using one of the static getInstance methods.
 * */
public class DrawActivity : MonoBehaviour
{
	// Button visibility settings
	private bool mShowPenSettings = true;
	private bool mShowEraser = true;
	private bool mShowText = true;
	private bool mShowFilling = true;
	private bool mShowColorPicker = true;
	private bool mShowInsertBtn = true;
	private bool mShowUndo = true;
	private bool mShowRedo = true;
	private bool mShowSave = true;

	private AndroidJavaClass mUnityPlayerClass;
	private AndroidJavaObject mCallingActivity;
	private DrawActivityOnResult mCaller;
	private byte[] mImage;
	private bool mDrawActivityOpened = false;
	private bool mIsCanvasInitialized = false;
	private bool mOnlyIncludeForeground = false;

	// GameObject for receiving messages from Java
	private static GameObject mSPenManager;
	private static DrawActivity mInstance;

	/**
	 * Returns the instance of DrawActivity class with default values of pOnlyIncludeForeground and button visibility settings.
	 * 
	 * @param caller The calling object, to be used for callbacks.
	 */
	public static DrawActivity getInstance (DrawActivityOnResult caller)
	{
		return getInstance(caller, false);
	}

	/**
	 * Returns the instance of DrawActivity class with default values of button visibility settings.
	 * 
	 * @param caller The calling object, to be used for callbacks.
	 * @param pOnlyIncludeForeground If true, only foreground of the drawn image will be returned as a result of this activity. If false, also 
	 * background will be included.
	 */
	public static DrawActivity getInstance (DrawActivityOnResult caller, bool pOnlyIncludeForeground)
	{
		return getInstance(caller, pOnlyIncludeForeground, true, true, true, true, true, true, true, true, true);
	}

	/**
	 * Returns the instance of DrawActivity class with given button visibility settings.
	 * 
	 * @param caller Calling object, to be used for callbacks.
	 * @param pOnlyIncludeForeground If true, only foreground of the drawn image will be returned as a result of this activity. If false, also 
	 * background will be included.
	 * @param showPenBtn
	 *            Pen button visibility flag.
	 * @param showEraserBtn
	 *            Eraser button visibility flag.
	 * @param showTextBtn
	 *            Text button visibility flag.
	 * @param showFillingBtn
	 *            Filling button visibility flag.
	 * @param showColorPickerBtn
	 *            Color picker button visibility flag.
	 * @param showInsertBtn
	 *            Insert button visibility flag.
	 * @param showUndoBtn
	 *            Undo button visibility flag.
	 * @param showRedoBtn
	 *            Redo button visibility flag.
	 * @param showSaveBtn
	 *            Save button visibility flag.
	 */
	public static DrawActivity getInstance (DrawActivityOnResult caller, bool pOnlyIncludeForeground, bool showPenBtn, bool showEraserBtn, 
		bool showTextBtn, bool showFillingBtn, bool showColorPickerBtn, bool showInsertBtn, bool showUndoBtn, bool showRedoBtn, bool showSaveBtn)
	{
		ZLog.Log ("DrawActivity getInstance start");

        if (caller == null)
            return null;

		if ((mSPenManager = GameObject.Find("_SPenManager")) == null) {
			ZLog.Log ("DrawActivity getInstance - creating manager game object.");
			mSPenManager = new GameObject ("_SPenManager");
			AndroidJNI.AttachCurrentThread ();
			AndroidJNIHelper.debug = true;
		}

		if ((mInstance = mSPenManager.GetComponent<DrawActivity> ()) == null) {
			ZLog.Log ("DrawActivity getInstance - adding component to manager game object.");
			mInstance = mSPenManager.AddComponent<DrawActivity> ();
			mInstance.mUnityPlayerClass = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
		}

		mInstance.mCallingActivity = mInstance.mUnityPlayerClass.GetStatic<AndroidJavaObject> ("currentActivity");
		mInstance.mCaller = caller;
		mInstance.mOnlyIncludeForeground = pOnlyIncludeForeground;
		mInstance.mShowPenSettings = showPenBtn;
		mInstance.mShowEraser = showEraserBtn;
		mInstance.mShowText = showTextBtn;
		mInstance.mShowFilling = showFillingBtn;
		mInstance.mShowColorPicker = showColorPickerBtn;
		mInstance.mShowInsertBtn = showInsertBtn;
		mInstance.mShowUndo = showUndoBtn;
		mInstance.mShowRedo = showRedoBtn;
		mInstance.mShowSave = showSaveBtn;

		ZLog.Log ("DrawActivity getInstance - returning instance.");
		return mInstance;
	}

	/**
	 * Returns the instance of DrawActivity if one exists, null otherwise.
	 * 
	 * @return The instance of DrawActivity if one exists, null otherwise.
	 */
	public static DrawActivity getExistingDrawActivity ()
	{
		ZLog.Log ("DrawActivity getExistingDrawActivity start");
		DrawActivity dd;

		if ((mSPenManager = GameObject.Find("_SPenManager")) != null) {
			if ((dd = mSPenManager.GetComponent<DrawActivity> ()) != null) {
				return dd;
			} else {
				ZLog.Log ("DrawActivity getExistingDrawActivity no DrawActivity - returning null");
				return null;
			}
		} else {
			ZLog.Log ("DrawActivity getExistingDrawActivity no _SPenManager game object - returning null");
			return null;
		}
	}

	/**
	 * Starts the Java DrawActivity and displays it on the screen.
	 */
	public void startDrawActivity ()
	{
		ZLog.Log ("DrawActivity startDrawActivity start");

		if (mDrawActivityOpened) {
			ZLog.LogWarning ("DrawActivity startDrawActivity - already started, aborting!");
			return;
		}

		mCallingActivity.Call ("startDrawActivity", mOnlyIncludeForeground, mShowPenSettings, mShowEraser, mShowText, mShowFilling, mShowColorPicker, 
			mShowInsertBtn, mShowUndo, mShowRedo, mShowSave);
		mDrawActivityOpened = true;
	}

	/**
	 * Checks if DrawActivity is currently opened.
	 * 
	 * @return True, if DrawActivty is currently opened, false otherwise.
	 */
	public bool isDrawActivityOpened ()
	{
		return mDrawActivityOpened;
	}

	/**
	 * Checks if DrawActivity is already initialized.
	 * 
	 * @return True, if DrawActivty has already been initialized, false otherwise.
	 */
	public bool isCanvasInitialized() {
		return mIsCanvasInitialized;
	}
	
	/**
	 * Returns the last image created by the user in this DrawActivity.
	 * 
	 * @return Last image created by the user as byte array.
	 */
	public byte[] getLastImage ()
	{
		ZLog.Log ("DrawActivity getLastImage start, mDrawActivityOpened: " + mDrawActivityOpened);
		if (mDrawActivityOpened) { // for performance reasons
			return mCallingActivity.Call<byte[]> ("getLastImage");
		} else {
			ZLog.Log ("DrawActivity getLastImage - returning last stored image");
			return mImage;
		}
	}

	/**
	 * Clears the stored image.
	 */
	public void clearStoredImage() {
		mImage = null;
	}

	/** 
	 * Callback from Java DrawActivity class. Calls onDrawActivityResult on the caller object. 
	 */
	public void onDrawActivityResultListener ()
	{
		ZLog.Log ("DrawActivity onDrawActivityResultListener start");

		mImage = getLastImage();
		mDrawActivityOpened = false; //This line must be AFTER getLastImage(), otherwise it will wrongly return the cached image.
		mCaller.onDrawActivityResult (mImage);

		ZLog.Log ("DrawActivity onDrawActivityResultListener end");
	}

	/** 
	 * Callback from Java DrawActivity class. Called when DrawActivity was cancelled (back arrow was pressed).
	 * Calls onDrawActivityResult on the caller object. 
	 */
	public void onDrawActivityCanceledListener ()
	{
		ZLog.Log ("DrawActivity onDrawActivityCanceledListener start");
		mDrawActivityOpened = false;
		mCaller.onDrawActivityCanceled ();
		ZLog.Log ("DrawActivity onDrawActivityCanceledListener end");
	}

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
