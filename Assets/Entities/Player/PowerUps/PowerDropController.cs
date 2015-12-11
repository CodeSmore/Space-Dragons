using UnityEngine;
using System.Collections;

public class PowerDropController : MonoBehaviour {

	public float dropSpeed = 2f;
	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -dropSpeed);
	}
	
	
	
}
