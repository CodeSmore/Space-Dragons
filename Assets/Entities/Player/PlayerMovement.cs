using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	// Speed player can move side to side and extra padding amount between player ship
	// and screen boundaries.
	public float speed, padding;
	// X position values that mark the booundaries
	// the player ship can move.
	private float xMin, xMax, yMin, yMax;
	
	void Start () {
		// A local camera holds the values for the game camera so that boundaries for ship movement 
		// can be determined. We don't want the ship to leave the view of the player.
		Camera camera = Camera.main;
		float distance = transform.position.z - camera.transform.position.z;
		xMin = camera.ViewportToWorldPoint (new Vector3(0, 0, distance)).x + padding;
		xMax = camera.ViewportToWorldPoint (new Vector3(1, 1, distance)).x - padding;
		yMin = camera.ViewportToWorldPoint (new Vector3(0, 0, distance)).y + padding;
		yMax = camera.ViewportToWorldPoint (new Vector3(1, .5f, distance)).y + padding;
	}
	
	void Update () {
		MoveWithFinger ();
	
	
	
	
	
	
//		// If the player presses the left OR right arrow keys...
//		if (Input.GetKey (KeyCode.LeftArrow)) {
//			// Then the position of the player is changed to a new Vector3.
//			// Mathf.Clamp is used to keep the ship in view of the camera.
//			this.transform.position = new Vector3 (
//				Mathf.Clamp (this.transform.position.x - speed * Time.deltaTime, xMin, xMax), 
//				this.transform.position.y, 
//				this.transform.position.z);
//		} else if (Input.GetKey (KeyCode.RightArrow)) {
//			this.transform.position = new Vector3 (
//				Mathf.Clamp (this.transform.position.x + speed * Time.deltaTime, xMin, xMax),
//				this.transform.position.y, 
//				this.transform.position.z);
//		} 
//		
//		if (Input.GetKey (KeyCode.DownArrow)) {
//			this.transform.position = new Vector3 (
//				this.transform.position.x,
//				Mathf.Clamp (this.transform.position.y - speed * Time.deltaTime, yMin, yMax),
//				this.transform.position.z);
//		} else if (Input.GetKey (KeyCode.UpArrow)) {
//			this.transform.position = new Vector3 (
//				this.transform.position.x,
//				Mathf.Clamp (this.transform.position.y + speed * Time.deltaTime, yMin, yMax),
//				this.transform.position.z);
//		}
	}
	
	private void MoveWithFinger () {
		// Captures the current possition of the ship
		Vector3 shipPos = new Vector3 (Input.mousePosition.x / Screen.width * 18.4f, 
									   Input.mousePosition.y / Screen.height * 27.2f + 2f, 
									   transform.position.z);
									   
		
		transform.position = shipPos;
		
	}
}	
