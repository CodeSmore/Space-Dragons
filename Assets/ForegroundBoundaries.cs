using UnityEngine;
using System.Collections;

public class ForegroundBoundaries : MonoBehaviour {
	// Method used to draw gizmos in the Scene view.
	// In this case, it's used to draw the boundaries of the formation.
	void OnDrawGizmos () {
		float xMin, xMax, yMin, yMax;
	
		xMin = GetComponent<Camera>().ViewportToWorldPoint (new Vector3(0, 0, 0)).x;
		xMax = GetComponent<Camera>().ViewportToWorldPoint (new Vector3(1, 0, 0)).x;
		yMin = GetComponent<Camera>().ViewportToWorldPoint (new Vector3(0, 0, 0)).y;
		yMax = GetComponent<Camera>().ViewportToWorldPoint (new Vector3(0, 1, 0)).y;
		// Draws the lines on the Scene view.
		// Extra: Gizmo.Drawline draws a straight line from one Vector3 point in space to another.
		// left side
		Gizmos.DrawLine (new Vector3 (xMin, yMin, 0), new Vector3 (xMin, yMax, 0));
		// right side
		Gizmos.DrawLine (new Vector3 (xMax, yMin, 0), new Vector3 (xMax, yMax, 0));
		// bottom
		Gizmos.DrawLine (new Vector3 (xMin, yMin, 0), new Vector3 (xMax, yMin, 0));
		// top
		Gizmos.DrawLine (new Vector3 (xMin, yMax, 0), new Vector3 (xMax, yMax, 0));
	}
}
