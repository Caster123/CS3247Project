using UnityEngine;
using System;

/** 
 * Class DrawingArea is a wrapper around a DrawingArea class from Java. It represents a View, which contains an SCanvasView.
 * Every script that wants to use this class must implement DrawingAreaOnResult interface. DrawingArea is a singleton - only one instance of this 
 * class can exist simultaneously. Acquire the instance using one of the static getInstance methods. This class allows much more customization than
 * DrawActivity.
 * */
public class DrawingArea : MonoBehaviour
{
	// Constant fields describing alignment.
	public const int GRAVITY_BOTTOM = 0x50;
	public const int GRAVITY_TOP = 0x30;
	public const int GRAVITY_RIGHT = 0x5;
	public const int GRAVITY_LEFT = 0x3;
	public const int GRAVITY_CENTER = 0x11;
	public const int MATCH_PARENT = -1;
	public const int WRAP_CONTENT = -2;

	private int alignment = GRAVITY_BOTTOM;
	private int width = MATCH_PARENT;
	private int height = MATCH_PARENT;

	private static GameObject mSPenManager;
	private static string DrawingAreaJavaClassName = "com.samsung.spenunity.views.DrawingArea";
	private static DrawingArea mInstance;

	private DrawingAreaOnResult mCaller;
	private AndroidJavaObject mDrawingAreaJava;
	private bool mOnlyIncludeForeground = false;
	private byte[] mImage;
	private bool mDrawingAreaOpened = false;

	private const long buttonPressDelayMillis = 100;
	private long lastTimeDrawingAreaShowed;
	private bool mIsCanvasInitialized = false;

	void Start ()
	{
		ZLog.Log ("DrawingArea Start");
	}
	
	/**
	 * Returns the instance of DrawingArea class with default values of pOnlyIncludeForeground and button visibility settings.
	 * 
	 * @param caller The calling object, to be used for callbacks.
	 */
	public static DrawingArea getInstance (DrawingAreaOnResult caller)
	{
		return getInstance(caller, false);
	}

	/**
	 * Returns the instance of DrawingArea class with default values of button visibility settings.
	 * 
	 * @param caller The calling object, to be used for callbacks.
	 * @param pOnlyIncludeForeground If true, only foreground of the drawn image will be returned as a result of this activity. If false, also 
	 * background will be included.
	 */
	public static DrawingArea getInstance (DrawingAreaOnResult caller, bool pOnlyIncludeForeground)
	{
		return getInstance(caller, pOnlyIncludeForeground, null);
	}

	/**
	 * Returns the instance of DrawingArea class with default values of button visibility settings.
	 * 
	 * @param caller The calling object, to be used for callbacks.
	 * @param pOnlyIncludeForeground If true, only foreground of the drawn image will be returned as a result of this activity. If false, also 
	 * background will be included.
	 * @param bgImage Background image to be set in the canvas view.
	 */
	public static DrawingArea getInstance (DrawingAreaOnResult caller, bool pOnlyIncludeForeground, byte[] bgImage)
	{
		ZLog.Log ("DrawingArea getInstance start");
        if (caller == null)
            return null;

		if ((mSPenManager = GameObject.Find("_SPenManager")) == null) {
			ZLog.Log ("DrawingArea getInstance - creating manager game object.");
			mSPenManager = new GameObject ("_SPenManager");
			AndroidJNI.AttachCurrentThread ();
			AndroidJNIHelper.debug = true;
		}
       
		if ((mInstance = mSPenManager.GetComponent<DrawingArea> ()) == null) {
			ZLog.Log ("DrawingArea getInstance - adding component to manager game object.");
			mInstance = mSPenManager.AddComponent<DrawingArea> ();
			mInstance.init (bgImage);
		} else {
			if (bgImage != null) {
				mInstance.setBackgroundImage (bgImage);
			}
		}

		mInstance.mCaller = caller;
		mInstance.mOnlyIncludeForeground = pOnlyIncludeForeground;

		ZLog.Log ("DrawingArea getInstance end");
		return mInstance;
	}

	private DrawingArea ()
	{
		ZLog.Log ("DrawingArea private(!) constructor noarg");
	}
	
	private void init(byte[] backgroundImage)
	{
		ZLog.Log ("DrawingArea init, width: " + width + "  height: " + height);
		AndroidJavaClass DrawingAreaJavaClass = new AndroidJavaClass (DrawingAreaJavaClassName);

		if (backgroundImage == null)
			backgroundImage = new byte[0];

		object[] drawAreaSettings = new object[4];
		drawAreaSettings [0] = alignment;
		drawAreaSettings [1] = width;
		drawAreaSettings [2] = height;
		drawAreaSettings [3] = backgroundImage;
		mDrawingAreaJava = DrawingAreaJavaClass.CallStatic<AndroidJavaObject>("getInstance", drawAreaSettings);
		ZLog.Log ("DrawingArea init - drawing area acquired.");

		ZLog.Log ("DrawingArea init end.");
	}
	
	/**
	 * Checks if DrawingArea is already initialized.
	 * 
	 * @return True, if DrawingArea has already been initialized, false otherwise.
	 */
	public bool isCanvasInitialized() {
		return mIsCanvasInitialized;
	}
	
	private void onDrawingAreaCanvasInitialized() {
		ZLog.Log ("DrawingArea onDrawingAreaCanvasInitialized start");
		mIsCanvasInitialized = true;
		mCaller.onDrawingAreaCanvasInitialized();
	}
	
	/**
	 * Sets button visibility in the Drawing Area.
	 * 
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
	public void setButtonVisibility(bool showPenBtn, bool showEraserBtn, 
		bool showTextBtn, bool showFillingBtn, bool showColorPickerBtn, bool showInsertBtn, bool showUndoBtn, bool showRedoBtn, bool showSaveBtn)
	{
		object[] btnSettings = new object[9];
		btnSettings [0] = showPenBtn;
		btnSettings [1] = showEraserBtn;
		btnSettings [2] = showTextBtn;
		btnSettings [3] = showFillingBtn;
		btnSettings [4] = showColorPickerBtn;
		btnSettings [5] = showInsertBtn;
		btnSettings [6] = showUndoBtn;
		btnSettings [7] = showRedoBtn;
		btnSettings [8] = showSaveBtn;

		ZLog.Log ("DrawingArea setButtonVisibility - before Java call to setButtonVisibility");
		mDrawingAreaJava.Call ("setButtonVisibility", btnSettings);
	}
	
	/**
	 * Returns a boolean array with button visibility settings.
	 * 
	 * @return A boolean array with current buttons visibility settings in order as set in setButtonVisibility.
	 */
	public bool[] getButtonVisibility()
	{
		bool[] buttons = new bool[9];

		ZLog.Log ("DrawingArea getButtonVisibility - before Java call to getButtonVisibility");
		buttons = mDrawingAreaJava.Call<bool[]> ("getButtonVisibility");

		return buttons;
	}
	
	/**
	 * Returns the canvas width.
	 * 
	 * @return Current canvas width (-1 is MATCH_PARENT, -2 is WRAP_CONTENT)
	 */
	public int getWidth()
	{
		ZLog.Log ("DrawingArea getWidth");
		return mDrawingAreaJava.Call<int> ("getWidth");
	}
	
	/**
	 * Returns the canvas height.
	 * 
	 * @return Current canvas height (-1 is MATCH_PARENT, -2 is WRAP_CONTENT)
	 */
	public int getHeight()
	{
		ZLog.Log ("DrawingArea getHeight");
		return mDrawingAreaJava.Call<int> ("getHeight");
	}

	/**
	 * Shows or hides the drawing area.
	 */
	public void toggleDrawingAreaVisibility ()
	{
		long timeNow = System.Environment.TickCount;
		long diff = timeNow - lastTimeDrawingAreaShowed;
		ZLog.Log ("DrawingArea toggleDrawingAreaVisibility start, time now: " + timeNow + "  lastTimeDrawingAreaShowed: " + lastTimeDrawingAreaShowed 
			+ "  difference: " + diff);
		
		if (timeNow - lastTimeDrawingAreaShowed < buttonPressDelayMillis) {
			ZLog.Log ("DrawingArea toggleDrawingAreaVisibility - delay too short, not toggling");
		} else {
			mDrawingAreaJava.Call ("toggleDrawingAreaVisibility");
			mDrawingAreaOpened = !mDrawingAreaOpened;
		}
		lastTimeDrawingAreaShowed = timeNow;
	}
	
	/**
	 * Hides the drawing area.
	 */
	public void hideDrawingArea ()
	{
		mDrawingAreaJava.Call ("hideDrawingArea");
		mDrawingAreaOpened = false;
	}
	
	/**
	 * Shows the drawing area.
	 */
	public void showDrawingArea ()
	{
		mDrawingAreaJava.Call ("showDrawingArea");
		mDrawingAreaOpened = true;
	}
	
	/**
	 * Checks if drawing area is currently visible.
	 * 
	 * @return True, if drawing area is visible, false otherwise.
	 */
	public bool isDrawingAreaOpened ()
	{
		return mDrawingAreaOpened;
	}
	
	/**
	 * Sets the canvas size.
	 * 
	 * @param newWidth
	 *            New canvas width.
	 * @param newHeight
	 *            New canvas height.
	 */
	public void setSize (int newWidth, int newHeight)
	{
		mDrawingAreaJava.Call ("setSize", newWidth, newHeight);
	}
	
	/**
	 * Sets the text box default size.
	 * 
	 * @param newWidth
	 *            New default text box width.
	 * @param newHeight
	 *            New default text box height.
	 */
	public void setTextBoxDefaultSize (int newWidth, int newHeight)
	{
		mDrawingAreaJava.Call ("setTextBoxDefaultSize", newWidth, newHeight);
	}
	
	/**
	 * Sets canvas bitmap data.
	 * 
	 * @param drawingData
	 *            New canvas bitmap data.
	 * 
	 * @return True, if operation was successful, false otherwise.
	 */
	public bool setSCanvasBitmapData (byte[] drawingData)
	{
		return mDrawingAreaJava.Call<bool> ("setSCanvasBitmapData", drawingData);
	}
	
	/**
	 * Shows or hides the eraser cursor.
	 * 
	 * @param visible
	 *            Whether or not the eraser cursor should be visible.
	 */
	public void setEraserCursorVisible (bool visible)
	{
		mDrawingAreaJava.Call ("setEraserCursorVisible", visible);
	}
	
	/**
	 * Checks if redo is available on the canvas.
	 * 
	 * @return True, if canvas supports redo operation, false otherwise.
	 */
	public bool isRedoable ()
	{
		return mDrawingAreaJava.Call<bool> ("isRedoable");
	}
	
	/**
	 * Checks if undo is available on the canvas.
	 * 
	 * @return True, if canvas supports undo operation, false otherwise.
	 */
	public bool isUndoable ()
	{
		return mDrawingAreaJava.Call<bool> ("isUndoable");
	}
	
	/**
	 * Returns the background color.
	 * 
	 * @return The background color as a 32-bit ARGB value.
	 */
	public int getBGColor ()
	{
		return mDrawingAreaJava.Call<int> ("getBGColor");
	}
	
	/**
	 * Draws a SAMM stroke point.
	 * 
	 * @param action
	 *            The action.
	 * @param x
	 *            The x coordinate.
	 * @param y
	 *            The y coordinate.
	 * @param pressure
	 *            The touch pressure.
	 * @param sammMetaState
	 *            The SAMM meta state. Use one of following:
	 *            SObjectStroke.SAMM_METASTATE_HAND,
	 *            SObjectStroke.SAMM_METASTATE_PEN,
	 *            SObjectStroke.SAMM_METASTATE_ERASER
	 * @param downTime
	 *            The time when the down touch event occurs.
	 * @param eventTime
	 *            The time when the current touch event occurs.
	 * 
	 * @return True for success. False for failure.
	 */
	public bool drawSAMMStrokePoint (int action, float x, float y, float pressure, int sammMetaState, long downTime, long eventTime)
	{
		return mDrawingAreaJava.Call<bool> ("drawSAMMStrokePoint", action, x, y, pressure, sammMetaState, downTime, eventTime);
	}
	
	/**
	 * Deletes the entire image drawn on a canvas, as well as any associated data/title/background/attached data, such as background audio 
	 * and background text. If initial foreground image of canvas (set by setClearImageBitmap method) is defined, the screen shows 
	 * this initial foreground image and Undo is not supported. If not, the screen is cleared and Undo is supported (for drawn data only). 
	 * Clears the last image returned from canvas view.
	 */
	public void clearSCanvasView ()
	{
		mImage = null;
		mDrawingAreaJava.Call ("clearSCanvasView");
	}
	
	/**
	 * Cancels drawing.
	 */
	public void cancelDrawing ()
	{
		mDrawingAreaJava.Call ("cancelDrawing");
	}
	
	/**
	 * Sets the canvas background image.
	 * 
	 * @param bmpData
	 *            Bitmap as byte array.
	 * 
	 * @return True for success. False for failure.
	 */
	public bool setBackgroundImage (byte[] bmpData)
	{
		object[] newParams = new object[1];
		newParams [0] = bmpData;
		return mDrawingAreaJava.Call<bool> ("setBackgroundImage", newParams);
	}
	
	/**
	 * Sets the background drawable.
	 * 
	 * @param source
	 *            New background drawable.
	 */
	public void setBackgroundDrawable (byte[] source)
	{
		object[] newParams = new object[1];
		newParams [0] = source;
		mDrawingAreaJava.Call ("setBackgroundDrawable", newParams);
	}
	
	/**
	 * Returns the current image drawn on the canvas in the drawing area. If drawing area is closed, it returns the last returned image. 
	 * Null, if there was none.
	 * 
	 * @param pOnlyIncludeForeground If true, only foreground of the drawn image will be returned as a result. If false, also 
	 * background will be included.
	 * 
	 * @return Current image as a byte array.
	 */
	public byte[] getImage (bool pOnlyIncludeForeground)
	{
		ZLog.Log ("DrawingArea getImage start, pOnlyIncludeForeground: " + pOnlyIncludeForeground);
		if (mDrawingAreaOpened) { // for performance reasons
			object[] imageSettings = new object[1];
			imageSettings [0] = pOnlyIncludeForeground;

			return mDrawingAreaJava.Call<byte[]> ("getImage", imageSettings);
		} else {
			return mImage;
		}
	}
	
	/** Callback from Java DrawingArea class. */
	private void onDrawingAreaResultListener ()
	{
		ZLog.Log ("DrawingArea resultListener start");
		
		mImage = getImage(mOnlyIncludeForeground);
		mDrawingAreaOpened = false; //This line must be AFTER getImage(), otherwise it will wrongly return the cached image.
		mCaller.onDrawingAreaResult (mImage);

		ZLog.Log ("DrawingArea resultListener end");
	}

}
