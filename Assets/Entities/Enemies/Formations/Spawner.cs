using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject enemyPrefab;
	
	private Position childPositionScript;
	
	private Camera foregroundCamera;

	private FormationController formationController;
	
	private bool spawned = false;
	
	// Use this for initialization
	void Start () {
		foregroundCamera = GameObject.Find ("Foreground Camera").GetComponent<Camera>();
		formationController = GetComponent<FormationController>();
	}
	
	void Update () {
		if (!spawned && transform.position.y < foregroundCamera.ViewportToWorldPoint (new Vector3 (0, 1, 0)).y + 5) {
			SpawnEnemies ();
			formationController.ActivateSpawnSpeeds();
		}
	}
	
	// Spawns all the enemies at once instead of one at a time like the SpawnUntilFull() method.
	void SpawnEnemies () {
		// Foreach child that exists in the formation...

		foreach (Transform child in transform) {
			// Spawn an enemy as the position's child...
			
			GameObject enemy;
			childPositionScript = child.gameObject.GetComponent<Position>();
			
			
			
			if (childPositionScript.GetHasOwnEnemyPrefab () == true) {
				enemy = childPositionScript.InstantiateEnemyPrefab () as GameObject;
			} else {
				enemy = Instantiate(enemyPrefab, child.position, Quaternion.identity) as GameObject;
			}
					
			// Then set the enemy parent to the Transform called 'child'...
			if (!enemy.GetComponent<ChargerBehavior>() && !enemy.GetComponent<DebrisController>()) {
				enemy.transform.parent = child;
				enemy.transform.position = Vector3.zero;
			}
			
			
		} 
		
		spawned = true;
	}
}
