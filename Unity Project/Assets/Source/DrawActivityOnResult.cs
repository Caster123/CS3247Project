/** 
 * DrawActivityOnResult must be implemented by any class wishing to use DrawActivity class.
 * The calling class is used as a parameter in DrawActivity.getInstance(DrawActivityOnResult) call.
 * Method onDrawActivityResult is called by a DrawActivity method onDrawActivityResultListener, which is a callback method from Java, 
 * called when DrawActivity is closed. Created image is passed in parameter.
 * */

public interface DrawActivityOnResult
{
	/**
	 * Method onDrawActivityResult is called when uses quits the DrawActivity, with the created imaged passed in parameter.
	 * 
	 * @param result Created image as byte array.
	 */
   void onDrawActivityResult(byte[] result);

	/**
	 * Method onDrawActivityCanceled is called when uses quits the DrawActivity without saving the image, as in by using the back arrow.
	 */
   void onDrawActivityCanceled();
}
