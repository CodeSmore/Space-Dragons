using UnityEngine;
using System.Collections;

public class LightningBehavior : MonoBehaviour {

	public GameObject bolt;
	
	public float secondsBetweenBoltSpawns;
	public int maxNumBolts;
	
	private float spawnRotZLeft = -31f;
	private float spawnRotZRight = 391f;
	
	private Vector3 previousTailPos;
	private Vector3 initialPos;
	
	private Vector3 spawnPosition = new Vector3 ();
	private Quaternion spawnRotation = Quaternion.identity;
	
	private float spawnTimer = 0;
	
	// Use this for initialization
	void Start () {
		spawnRotation =	transform.rotation;
		initialPos = transform.position;
		GameObject newBolt = Instantiate (bolt, initialPos, spawnRotation) as GameObject;
		newBolt.transform.parent = transform;
//		lastBoltPosition = newBolt.transform.position;
		
		foreach (Transform childTransform in newBolt.transform) {
			if (childTransform.gameObject.name == "Tail") {
				previousTailPos = childTransform.position;
			}
		}
		newBolt.GetComponentInChildren<SpriteRenderer>().sprite = null;
	}
	
	// Update is called once per frame
	void Update () {
		// Spawn bolts based on predetermined interval "secondsBetweenBoltSpawns"
		spawnTimer += Time.deltaTime;
		
		SpawnChain ();
//		if (spawnTimer > secondsBetweenBoltSpawns) {
//			SpawnChain();
//			secondsBetweenBoltSpawns += interval;
//		}
		
		// Check if gameObject has a number of children greater than "maxNumBolts".
		// If so, destroy the eldest one.
		MaintainBoltLimit ();
	}
	
	void MaintainBoltLimit () {
		if (this.transform.childCount > maxNumBolts) {
			Transform eldest = null;
			foreach (Transform child in transform) {
				
				if (!eldest || eldest.GetComponent<LightningBolt>().getAgeInSeconds () < child.GetComponent<LightningBolt>().getAgeInSeconds ()) {
					eldest = child;
				} 
			}
			
			Destroy (eldest.gameObject);
		}	
	}
	
	void SpawnChain () {
		float rand = Random.value ;
				
		// Determines which direction next bolt goes, left or right
		if (rand < .5) {
			spawnRotation.eulerAngles = new Vector3 (0, 0, transform.rotation.eulerAngles.z + spawnRotZLeft);
		} else {
			spawnRotation.eulerAngles = new Vector3 (0, 0, transform.rotation.eulerAngles.z + spawnRotZRight);
		}
		
		spawnPosition = previousTailPos;
		
		GameObject newBolt = Instantiate (bolt, spawnPosition, spawnRotation) as GameObject;
		newBolt.transform.localScale *= rand * .5f;
		foreach (Transform childTransform in newBolt.transform) {
			if (childTransform.gameObject.name == "Tail") {
				
				previousTailPos = childTransform.position;
			}
		}
		newBolt.transform.parent = transform;
//		lastBoltPosition = newBolt.transform.position;
	}
	
//	void SpawnChain () {
//
//		
//		float rand = Random.value ;
//		
//		// Determines which direction next bolt goes, left or right
//		if (rand < .5) {
//			spawnPosition = new Vector3 (lastBoltPosition.x + -spawnPosX, lastBoltPosition.y + spawnPosY, 0);
//			spawnRotation.eulerAngles = new Vector3 (0, 0, spawnRotZLeft);
//		} else {
//			spawnPosition = new Vector3 (lastBoltPosition.x + spawnPosX, lastBoltPosition.y + spawnPosY, 0);
//			spawnRotation.eulerAngles = new Vector3 (0, 0, spawnRotZRight);
//		}
//		
//		
//		GameObject newBolt = Instantiate (bolt, spawnPosition, spawnRotation) as GameObject;
//		newBolt.transform.parent = transform;
//		lastBoltPosition = newBolt.transform.position;
//	}
}
