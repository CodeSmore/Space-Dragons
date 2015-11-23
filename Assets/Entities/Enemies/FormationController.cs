using UnityEngine;
using System.Collections;

public class FormationController : MonoBehaviour {

	// Takes the enemy prefab so it can be instantiated within this method.
	public GameObject enemyPrefab;
//	private GameObject bossFormation;
//	private GameObject wave2Formation;
	
	// Determines the size of the formation on the screen.
	public float width = 10, height = 5;
	
	// The speed the entire formation moves.
	public float speed = 1;
	public float downwardsSpeed = 1;
	private float initialSpeed;
	private float dieSpeed = 0;
	
	// Used to create an extra boundary in case the formation leaves the camera view.
	public float padding = -2f;
	
	// Int that acts as a bool for the direction the formation moves; +1 for right or -1 for left.
	public int direction = 1;
	
	public static int bossSpawnScore;
	
	// Boundaries for visible gamespace. 
	private float boundaryRightEdge, boundaryLeftEdge;
	private float formationRightEdge = 0;
	private float formationLeftEdge = 0;
	
	// Use this for initialization
	void Start () {
	
		initialSpeed = speed;
		
		// All formations are present at the beginning, so the others are disabled immediately upon load
		// until they are called upon.
//		wave2Formation = GameObject.Find ("EnemyFormation 2");
//		wave2Formation.SetActive (false);
//	
//		bossFormation = GameObject.Find ("BossFormation");
//		bossFormation.SetActive (false);
		
		// Initializes a Camera type object so that it's position and ViewportToWorldPoint values
		// can be used to move the formation within the visible gamespace.
		Camera camera = Camera.main;
		
		// Initializes 'distance' to hold the z distrance between the camera and formation.
		float distance = transform.position.z - camera.transform.position.z;
		
		// Defines the boundaries for the visible gamespace, therefore setting the boundaries
		// for where we want the formation to move.
		boundaryLeftEdge = camera.ViewportToWorldPoint (new Vector3(0, 0, distance)).x + padding;
		boundaryRightEdge = camera.ViewportToWorldPoint (new Vector3(1, 1, distance)).x - padding;
	}
	
	void Update () {
		// Causes a change in direction of the formation once 
		// the formation's boundary crosses visible gamespace's boundary.
		if 	(formationRightEdge > boundaryRightEdge) {
			direction = -1; 
			print ("trying to change");
		}
		if (formationLeftEdge < boundaryLeftEdge) {
			direction = 1; 
			print ("changing back");
		}
		// This single statement determines the movement of the formation in the x direction.
		// Extra: Movement is based on last know position + conversion from /frame to /second * direction (+1 or -1) * speed.
		
		Debug.Log ("Boundary Right: " + boundaryRightEdge);
		Debug.Log ("Formation Right: " + formationRightEdge);
		
		Debug.Log ("Boundary Left: " + boundaryLeftEdge);
		Debug.Log ("Formation Left: " + formationLeftEdge);
		
		this.transform.position = new Vector3 (
			transform.position.x + Time.deltaTime * direction * speed, 
			this.transform.position.y + Time.deltaTime * downwardsSpeed,
			0
		);
		
		UpdateBoundaries();
		
	}
	
	public void UpdateBoundaries () {
		// Initializes the boundaries of the formation as x positions.
		bool childrenExist = false;
		formationLeftEdge = 50;
		formationRightEdge = 0;
		
		foreach (Transform position in transform) {
			foreach (Transform child in position.transform) {
				if (child.childCount > 0) {
					
					childrenExist = true;
					
					if (formationRightEdge < child.position.x) {
						formationRightEdge = child.position.x;
					}
					if (formationLeftEdge > child.position.x) {
						formationLeftEdge = child.position.x;
					}
				}	
			}
		}
				
		if (!childrenExist) {
			speed = dieSpeed;
		} else {
			speed = initialSpeed; 
		}
	}
	
	// Method used to draw gizmos in the Scene view.
	// In this case, it's used to draw the boundaries of the formation.
	void OnDrawGizmos () {
		// Variables to hold boundaries of the formation as ints.
		float xMin, xMax, yMin, yMax;
		
		// Defining those values.
		xMin = transform.position.x - 0.5f * width;
		xMax = transform.position.x + 0.5f * width;
		yMin = transform.position.y - 0.5f * height;
		yMax = transform.position.y + 0.5f * height;
		
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
	
	// As it's name implies, it returns a bool value based on if all enemies in the formation are dead.
	bool AllMembersAreDead () {
		// 'foreach' is a loop used to access each object in a group.
		// It's format goes 'foreach (Transform *data type* position <variable name> in transform *as in, this.transform*) {}
		// In this situation, we access each Transform that exists as a child of 'this'. If it doesn't exist, false is returned.
		// Otherwise, true is returned. 
		foreach (Transform position in transform) {
			if (position.childCount > 0) {
				return false;
			}
		}
		return true;
	}
	
//	// Spawns individual enemies until all positions in the formation are full.
//	void SpawnUntilFull () {
//	
//		// Initialize a Transform that holds the next empty position as defined by method NextFreePosition ().
//		Transform freePos = NextFreePosition ();
//		
//		// If freePos recieved a value from NextFreePosition()...
//		if (freePos != null) {
//			// Initialize an enemy object while Instantiating the object at the same time!
//			// Extra: "as GameObject" must be added as Instantiate returns a regular 'Object'.
//			GameObject enemy = Instantiate(enemyPrefab, freePos.position, Quaternion.identity) as GameObject;
//			
//			// Then the parent's transform is set to freePos.
//			// This is done so the game knows an enemy occupies this position.
//			// W/o this statement, an infinite amound of enemies spawn constantly in this position.
//			// ...and the game crashes...
//			enemy.transform.parent = freePos;
//		}
//		
//		// This method checks to see if more free positions exists. If so, this method is called again
//		// so an enemy can be spawned. 
//		// W/o this method, only one enemy would spawn. 
//		// Extra: Invoke ("method", timedelay) is used to make each ship enter formation alone instead of all at once.
//		if (FreePositionExists()) {
//			Invoke ("SpawnUntilFull", spawnDelaySeconds);
//		}
//	}
	
	// This method returns true if a free position in the formation exists, false if not.
	bool FreePositionExists () {
		// Again, uses the foreach loop. If a child of the formation has no child, then it is a free position
		// and true is return, ending the method. If not, false is returned.
		foreach (Transform position in transform) {
			if (position.childCount <= 0) {
				return true;
			}
		}
		return false;
	}
	
	// This method returns the postion of the next free position in the formation.
	Transform NextFreePosition () {
		// foreach loop is used. Same as FreePositionExists, except that the position is return if it's free, 
		// null if none are free.
		foreach (Transform position in transform) {
			if (position.childCount <= 0) {
				return position;
			}
		}
		return null;
	}
	
	
	
	public static int getBossSpawnScore () {
		return bossSpawnScore;
	}
}
