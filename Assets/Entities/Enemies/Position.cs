using UnityEngine;
using System.Collections;

public class Position : MonoBehaviour {

	// Draws a wire sphere w/ a radius of 1 over each position in the formation.
	void OnDrawGizmos () {
		Gizmos.DrawWireSphere (transform.position, 1);
	}
}
