using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject Formation1;
	public GameObject Formation2;
	public GameObject Formation3;
	public GameObject Brofist;
	
	private GameObject f1;
	private GameObject f2;
	private GameObject f3;
	private GameObject f4;
	public float timer = 0;
	
	
	// Use this for initialization
	void Start () {
		Instantiate (Formation1, new Vector3 (0, 12, 0), Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (f4 || timer > 40) {
		
		} else if (timer > 30 && !f4) {
			f4 = Instantiate (Brofist, new Vector3 (0, 12, 0), Quaternion.identity) as GameObject;
		} else if (timer > 15 && !f3) {
			f3 = Instantiate (Formation3, new Vector3 (0, 12, 0), Quaternion.identity) as GameObject;
		} else if (timer > 5 && !f2) {
			f2 = Instantiate (Formation2, new Vector3 (0, 12, 0), Quaternion.identity) as GameObject;
		}
	}
}
