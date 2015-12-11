using UnityEngine;
using System.Collections;

public class DebrisController : MonoBehaviour {

	public float fallSpeed = 10;
	public float spinSpeed = 50;
	
	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody2D>().velocity = new Vector2 (Random.Range (-1f, 1f), -fallSpeed);
	}
	
	// Update is called once per frame
	void Update () {
		// Spin
		transform.rotation = Quaternion.Euler (new Vector3 (0, 0, spinSpeed * Time.timeSinceLevelLoad));
	}
}
