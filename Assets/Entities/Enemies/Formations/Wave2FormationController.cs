﻿using UnityEngine;
using System.Collections;

public class Wave2FormationController : MonoBehaviour {

	// Takes the enemy prefab so it can be instantiated within this method.
	public GameObject tankPrefab;
	public GameObject medEnemyPrefab;
	private GameObject bossFormation;
	
	// Determines the size of the formation on the screen.
	public float width = 10, height = 5;
	
	// The speed the entire formation moves.
	public float speed = 1;
	
	// Used to create an extra boundary in case the formation leaves the camera view.
	public float padding = 0;
	
	// Delay in seconds between each spawned enemy.
	public float spawnDelaySeconds = 1f;
	
	// Int that acts as a bool for the direction the formation moves; +1 for right or -1 for left.
	private int direction = 1;
	
	private static int bossSpawnScore;
	// Boundaries for visible gamespace. 
	private float boundaryRightEdge, boundaryLeftEdge;
	private bool retreat = false;
	private MusicPlayer bossMusic;
	
	// Use this for initialization
	void Start () {
		bossSpawnScore = FormationController.getBossSpawnScore ();
		bossMusic = FindObjectOfType<MusicPlayer>();
		bossFormation = GameObject.Find ("BossFormation");
		bossFormation.SetActive (false);
		
		// Initializes a Camera type object so that it's position and ViewportToWorldPoint values
		// can be used to move the formation within the visible gamespace.
		Camera camera = Camera.main;
		
		// Initializes 'distance' to hold the z distrance between the camera and formation.
		float distance = transform.position.z - camera.transform.position.z;
		
		// Defines the boundaries for the visible gamespace, therefore setting the boundaries
		// for where we want the formation to move.
		boundaryLeftEdge = camera.ViewportToWorldPoint (new Vector3(0, 0, distance)).x + padding;
		boundaryRightEdge = camera.ViewportToWorldPoint (new Vector3(1, 1, distance)).x - padding;
	
		// Calls the 'SpawnEnemies ()' local method.
		SpawnEnemies ();
	}
	
	void Update () {
	
		// Initializes the boundaries of the formation as x positions.
		float formationRightEdge = transform.position.x + 0.5f * width;
		float formationLeftEdge = transform.position.x - 0.5f * width;
		
		// Causes a change in direction of the formation once 
		// the formation's boundary crosses visible gamespace's boundary.
		if 		 (formationRightEdge > boundaryRightEdge) {
			direction = -1; 
		}else if (formationLeftEdge < boundaryLeftEdge) {
			direction = 1; 
		}
		
		// This single statement determines the movement of the formation in the x direction.
		// Extra: Movement is based on last know position + conversion from /frame to /second * direction (+1 or -1) * speed.
		if (retreat == false) {
			this.transform.position = new Vector3 (transform.position.x + Time.deltaTime * direction * speed, this.transform.position.y, 0);
		}
		
		
		// It does what it says, using methods.
		if (AllMembersAreDead()) {
			SpawnUntilFull();
		}
		
		if (ScoreKeeper.getScore() >= bossSpawnScore) {
			SpawnBoss ();
		}
	}
	
	void SpawnBoss () {
		retreat = true;
		this.transform.position = new Vector3 (transform.position.x, this.transform.position.y + Time.deltaTime * speed, 0);
	
		if (this.transform.position.y >= 8.5) {
			bossFormation.SetActive(true);
			
			if (bossMusic) {
				bossMusic.PlayBossMusic();
			}
			Destroy (gameObject);
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
	
	// Spawns individual enemies until all positions in the formation are full.
	void SpawnUntilFull () {
	
		// Initialize a Transform that holds the next empty position as defined by method NextFreePosition ().
		Transform freePos = NextFreePosition ();
		
		// If freePos recieved a value from NextFreePosition()...
		if (freePos != null) {
			// Initialize an enemy object while Instantiating the object at the same time!
			// Extra: "as GameObject" must be added as Instantiate returns a regular 'Object'.
			GameObject enemy;
			
			if (freePos.name == "Tank") {
				enemy = Instantiate(tankPrefab, freePos.position, Quaternion.identity) as GameObject;
			} else {
				enemy = Instantiate(medEnemyPrefab, freePos.position, Quaternion.identity) as GameObject;
			}
			
			
			// Then the parent's transform is set to freePos.
			// This is done so the game knows an enemy occupies this position.
			// W/o this statement, an infinite amound of enemies spawn constantly in this position.
			// ...and the game crashes...
			enemy.transform.parent = freePos;
			
		}
		
		// This method checks to see if more free positions exists. If so, this method is called again
		// so an enemy can be spawned. 
		// W/o this method, only one enemy would spawn. 
		// Extra: Invoke ("method", timedelay) is used to make each ship enter formation alone instead of all at once.
		if (FreePositionExists()) {
			Invoke ("SpawnUntilFull", spawnDelaySeconds);
		}
	}
	
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
	
	// Spawns all the enemies at once instead of one at a time like the SpawnUntilFull() method.
	void SpawnEnemies () {
		// Foreach child that exists in the formation...
		foreach (Transform child in transform) {
			// Spawn an enemy as the position's child...
			GameObject enemy;
			
			if (child.name == "Tank") {
				enemy = Instantiate(tankPrefab, child.transform.position, child.transform.rotation) as GameObject;
			} else {
				enemy = Instantiate(medEnemyPrefab, child.transform.position, child.transform.rotation) as GameObject;
			}
			
			// Then set the enemy parent to the Transform called 'child'...
			enemy.transform.parent = child;
		} 
	}
	
	public static int getBossSpawnScore () {
		return bossSpawnScore;
	}
}
