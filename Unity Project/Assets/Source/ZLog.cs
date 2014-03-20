using UnityEngine;
using System;

/**
 * A wrapper class for Debug logging, with debug level support. Currently only debugLevel > 0 condition is checked.
 */
public class ZLog
{
	private static int debugLevel = 1;

	/**
	 * Returns the current debug level.
	 *
	 * @return Current debug level.
	 */
	public static int getDebugLevel() {
		return debugLevel;
	}

	/**
	 * Sets the debug level. Currently debug level > 0 means message will be logged.
	 *
	 * @param pDebugLevel New debug level.
	 */
	public static void setDebugLevel(int pDebugLevel) {
		debugLevel = pDebugLevel;
	}

	/**
	 * Wrapper around Debug.Log
	 *
	 * @param msg Message to be logged if debug level allows it.
	 */
	public static void Log(String msg) {
		if (debugLevel > 0)
			Debug.Log(msg);
	}

	/**
	 * Wrapper around Debug.LogWarning
	 *
	 * @param msg Message to be logged if debug level allows it.
	 */
	public static void LogWarning(String msg) {
		if (debugLevel > 0)
			Debug.LogWarning(msg);
	}

	/**
	 * Wrapper around Debug.LogError
	 *
	 * @param msg Message to be logged if debug level allows it.
	 */
	public static void LogError(String msg) {
		if (debugLevel > 0)
			Debug.LogError(msg);
	}
}
