using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject boss;
	
	private GameObject bossSpawnTrigger;
	private bool bossInScene = false;
		
	// Use this for initialization
	void Start () {
		bossSpawnTrigger = GameObject.FindGameObjectWithTag ("BossSpawnTrigger");
	}
	
	// Update is called once per frame
	void Update () {
		if (!bossSpawnTrigger && !bossInScene) {
			Instantiate (boss);
			bossInScene = true;
		}
	}
}
