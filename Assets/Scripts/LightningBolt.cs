using UnityEngine;
using System.Collections;

public class LightningBolt : MonoBehaviour {

	private float timeSinceInstantiated = 0;
	public float maxAgeInSeconds;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timeSinceInstantiated += Time.deltaTime;
		
		if (timeSinceInstantiated > maxAgeInSeconds) {
			Destroy (gameObject);
		}
	}
	
	public float getAgeInSeconds () {
		return timeSinceInstantiated;
	}
}
