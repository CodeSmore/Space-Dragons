using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject enemyPrefab;
	
	// Use this for initialization
	void Start () {
		SpawnEnemies ();
	}
	
	// Spawns all the enemies at once instead of one at a time like the SpawnUntilFull() method.
	void SpawnEnemies () {
		// Foreach child that exists in the formation...
		foreach (Transform child in transform) {
			// Spawn an enemy as the position's child...
			GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			// Then set the enemy parent to the Transform called 'child'...
			enemy.transform.parent = child;
			enemy.transform.position = Vector3.zero;
		} 
	}
}
