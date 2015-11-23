using UnityEngine;
using System.Collections;

public class LightningBehavior : MonoBehaviour {

	public GameObject bolt;
	
	public float waitTime;

	private float spawnPosX = .124f;
	private float spawnPosY = -0.443f;
	private float spawnRotZLeft = -31f;
	private float spawnRotZRight = 391f;
	
	private Vector3 initialPosChange = new Vector3();
	
	private Vector3 lastBoltPosition = new Vector3(-.124f, -0.443f, 0);
	
	private Vector3 spawnPosition = new Vector3 ();
	private Quaternion spawnRotation = Quaternion.identity;
	
	// Use this for initialization
	void Start () {
		GameObject newBolt = Instantiate (bolt, initialPosChange, spawnRotation) as GameObject;
		newBolt.transform.parent = transform;
		lastBoltPosition = newBolt.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		StartCoroutine (SpawnChain());
	}
	
	IEnumerator SpawnChain () {
		yield return new WaitForSeconds (waitTime);
		Debug.Log ("trying...");
		
		float rand = Random.value ;
		
		if (rand < .5) {
			spawnPosition = new Vector3 (lastBoltPosition.x + -spawnPosX, lastBoltPosition.y + spawnPosY, 0);
			spawnRotation.eulerAngles = new Vector3 (0, 0, spawnRotZLeft);
		} else {
			spawnPosition = new Vector3 (lastBoltPosition.x + spawnPosX, lastBoltPosition.y + spawnPosY, 0);
			spawnRotation.eulerAngles = new Vector3 (0, 0, spawnRotZRight);
		}
		
		
		
		
		GameObject newBolt = Instantiate (bolt, spawnPosition, spawnRotation) as GameObject;
		newBolt.transform.parent = transform;
		lastBoltPosition = newBolt.transform.position;
	}
}
