using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour {

	// Speed player can move side to side and extra padding amount between player ship
	// and screen boundaries.
	public float speed, padding;
	public GameObject pauseIcon;
	// X position values that mark the booundaries
	// the player ship can move.
	private float xMin, xMax, yMin, yMax;
	
	// Offset of finger to ship, allows player to see the ship b/c it's not under finger.
	private Vector2 shipOffset = new Vector2 (0f, 2.0f);
	private float timer = 0;
	private static bool playMode = true;
	private GameObject menuIcon;
	private GameObject gameController;
	private Camera camera;
	
	void Awake () {
		gameController = GameObject.Find ("Game Controller");
		menuIcon = GameObject.Find ("Pause Button");
	}
	void Start () {
		// A local camera holds the values for the game camera so that boundaries for ship movement 
		// can be determined. We don't want the ship to leave the view of the player.
		camera = Camera.main;
		float distance = transform.position.z - camera.transform.position.z;
		xMin = camera.ViewportToWorldPoint (new Vector3(0, 0, distance)).x + padding;
		xMax = camera.ViewportToWorldPoint (new Vector3(1, 1, distance)).x - padding;
		yMin = camera.ViewportToWorldPoint (new Vector3(0, 0, distance)).y + padding;
		yMax = camera.ViewportToWorldPoint (new Vector3(1, 1f, distance)).y - padding;
	}
	
	void Update () {
		ManipulateGameSpeedAndPauseButton ();
//		Old Stuff: Arrow Key Input
		// If the player presses the left OR right arrow keys...
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
	
	private void ManipulateGameSpeedAndPauseButton () {
		if (GameObject.Find ("Pause Menu")) {
			Time.timeScale = 0;
		} else if ((Input.touchCount > 0 || Input.GetButton ("Fire1")) && playMode) {
			menuIcon.SetActive (false);
			timer = 0;
			Time.timeScale = 1;
			
			// Fade In
			gameController.GetComponent<FadeTextureController>().BeginFade (-1);
			
			// Move
			MoveWithFinger ();
		} else {
			menuIcon.SetActive (true);
			timer += Time.deltaTime * 2;
			
			if (timer > .9f) 
				timer = .9f;
			
			Time.timeScale = 1 - timer;
			
			playMode = false;
			
			// Fade Out
			gameController.GetComponent<FadeTextureController>().BeginFade (1);
		}
	}
	private void MoveWithFinger () {
		Vector2 mousePos = camera.ScreenToWorldPoint (Input.mousePosition);	

		float speedAdjustment = 1;
		
		// Snapping Effect for x and y
		if (Mathf.Abs (mousePos.x - transform.position.x) < 1 || Mathf.Abs (mousePos.y - transform.position.y) < 1) {
			speedAdjustment = 2.0f;
		} else if (Mathf.Abs (mousePos.x - transform.position.x) < 2 || Mathf.Abs (mousePos.y - transform.position.y) < 2) {
			speedAdjustment = 1.5f;
		} else {
			speedAdjustment = 1.0f;
		}
		
		
		// Standard Lerp for smooth movement
		Vector3 shipPos = Vector3.Lerp (
										gameObject.transform.position, 
										new Vector3 (
											Mathf.Clamp (mousePos.x + shipOffset.x, xMin, xMax), 
									  	    Mathf.Clamp (mousePos.y + shipOffset.y, yMin, yMax), 
									   		transform.position.z
									   	),
									   	Time.deltaTime * speed * speedAdjustment
						  );
									   
		transform.position = shipPos;
	}
	
	public static void TogglePlayMode () {
		if (playMode == false) {
			playMode = true;
		} else {
			playMode = false;
		}
	}
}	
