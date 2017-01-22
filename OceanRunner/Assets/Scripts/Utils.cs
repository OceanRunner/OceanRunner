using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils{

	public static int calibrationTime = 3;

	public static float gameSpeed = 3;
	public static float amplitude = 1.5f;
	public static float freqency = 2f;

	/**
	 * Calculate camera bounds.
	 * http://answers.unity3d.com/questions/501893/calculating-2d-camera-bounds.html
	 **/
	public static Bounds getCameraBounds(){
		float screenAspect = (float)Screen.width / (float)Screen.height;
		float cameraHeight = Camera.main.orthographicSize * 2;
		Bounds bounds = new Bounds(
			Camera.main.transform.position,
			new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
		return bounds;
	}
	public static Vector3[] MakeSmoothCurve(Vector3[] points ,int inbetween){
		// Eg. inbetween = 2: result will be o1 n2 n3 o4 n5 n6 o7 ...
		Vector3[] result = new Vector3[(points.Length - 1) * (inbetween + 1) + 1];
		Vector3 p0, p1, p2, p3;
		for (int i = 0; i < points.Length - 1; i++) {
			if (i == 0)
				p0 = points [i];
			else
				p0 = points [i - 1];
			if (i == points.Length - 2)
				p3 = points [i + 1];
			else
				p3 = points [i + 2];
			p1 = points [i];
			p2 = points [i + 1];
			result [i * (inbetween + 1)] = p1;
			for (int j = 0; j < inbetween; j++) {
				float t = ((float)j) / ((float)inbetween);

				result [i * (inbetween + 1) + j + 1] = 0.5f * ((2 * p1) +
					(-p0 + p2) * t +
					(2 * p0 - 5 * p1 + 4 * p2 - p3) * Mathf.Pow (t, 2) +
					(-p0 + 3 * p1 - 3 * p2 + p3) * Mathf.Pow (t, 3));
			}
		}
		result [result.Length - 1] = points[points.Length-1];
		return result;
	}
}
