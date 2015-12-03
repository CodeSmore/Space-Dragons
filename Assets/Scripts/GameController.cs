using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject Formation1;
	public GameObject Formation2;
	public GameObject Formation3;
	public GameObject Brofist;
	
	private GameObject f4;
	
	private bool spawned2 = false;
	private bool spawned3 = false;
	private bool spawned4 = false;
	
	public float timer = 0;
	
	public bool DroneAssault = true;
		
	// Use this for initialization
	void Start () {
		Instantiate (Formation1, new Vector3 (0, 20, 0), Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer > 22 && !spawned4) {
			f4 = Instantiate (Brofist, new Vector3 (Camera.main.ViewportToWorldPoint (new Vector2 (.5f, .5f)).x, 25, 0), Quaternion.identity) as GameObject;
			spawned4 = true;
		} else if (timer > 15 && !spawned3) {
			Instantiate (Formation3, new Vector3 (0, 20, 0), Quaternion.identity);
			spawned3 = true;
		} else if (timer > 5 && !spawned2) {
			Instantiate (Formation2, new Vector3 (0, 20, 0), Quaternion.identity);
			spawned2 = true;
		} else if ((spawned4 && !f4) || DroneAssault == false) {
			Level2Controller.StartCharge ();
			Destroy (f4);
		}
	}
}
