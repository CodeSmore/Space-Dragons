using UnityEngine;
using System.Collections;

public class Shredder : MonoBehaviour {

	void OnTriggerExit2D (Collider2D col) {
		Destroy(col.transform.gameObject);
		
		if (col.transform.parent != null) {
			if (col.transform.parent.tag != "DoNotDestroy") {
				Destroy (col.transform.parent.gameObject);
			}
		}
	}
}
