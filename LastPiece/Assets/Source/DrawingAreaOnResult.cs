/** 
 * DrawingAreaOnResult must be implemented by any class wishing to use DrawingArea class.
 * The calling class is used as a parameter in DrawingArea.getInstance(DrawingAreaOnResult) call.
 * Method onDrawingAreaResult is called by a DrawingArea method onDrawingAreaResultListener, which is a callback method from Java, 
 * called when DrawingArea is closed. Created image is passed in parameter.
 * */
public interface DrawingAreaOnResult
{
	/**
	 * Method onDrawingAreaResult is called when uses quits the DrawingArea, with the created imaged passed in parameter.
	 * 
	 * @param result Created image as byte array.
	 */
   void onDrawingAreaResult(byte[] result);

	/**
	 * Method onDrawingAreaCanvasInitialized is called from DrawingArea.onDrawingAreaCanvasInitialized 
	 * when onInitialized is called for the SCanvasView in the DrawingArea.
	 */
	void onDrawingAreaCanvasInitialized();
}
