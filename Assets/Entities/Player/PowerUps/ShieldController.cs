using UnityEngine;
using System.Collections;

public class ShieldController : MonoBehaviour {
	
//	void Update () {
//		this.transform.position = new Vector3 (player.transform.position.x, -3.9f, 0);
//	}
	
	void OnTriggerEnter2D (Collider2D collider) {
		if (collider.gameObject.tag != "PlayerLaser" && collider.gameObject.tag != "Laserang" && collider.gameObject.tag != "PowerUp" && collider.gameObject.tag != "Formation"	)
		Destroy (collider.gameObject);
	}
}
