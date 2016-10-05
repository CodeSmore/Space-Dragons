using UnityEngine;
using System.Collections;

public class BulletShredder : MonoBehaviour {
	void OnTriggerExit2D (Collider2D col) {
		if (col.tag == "PlayerLaser") {
			Destroy(col.transform.gameObject);
		
			if (col.transform.parent != null) {
				if (col.transform.parent.tag != "DoNotDestroy") {
					Destroy (col.transform.parent.gameObject);
				}
			}
		}
	}
}
