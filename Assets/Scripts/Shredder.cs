using UnityEngine;
using System.Collections;

public class Shredder : MonoBehaviour {

	void OnTriggerExit2D (Collider2D col) {
		Destroy(col.transform.gameObject);
		Destroy (col.transform.root.gameObject);
	}
}
