using UnityEngine;
using System.Collections;

public class BeginLevel : MonoBehaviour {
	
	private GameObject enemies;
	private GameObject gameController;
	
	private Scroller background;
	private bool timerStarted = false;
	
	private float timer = 0;
	
	public float endChargeTime = 50;
	
	void Awake () {
		enemies = GameObject.Find ("Enemies");
		enemies.SetActive (false);
		gameController = GameObject.FindGameObjectWithTag ("GameController");
		gameController.SetActive (false);
		
		
		background = FindObjectOfType<Scroller>();
	}
	
	void Update () {
		if (timerStarted) {
			timer += Time.deltaTime;
			
			// destroys touch "Touch to Begin" text GameObject
			foreach (Transform child in gameObject.transform) {
				Destroy (child.gameObject);
			}
			
			if (timer > endChargeTime) {
				Destroy (gameObject);
			}
		}
	}
	
	
	public void OnMouseDown () {
		enemies.SetActive (true);
		gameController.SetActive (true);
		
		background.BeginScrolling ();
		
		StartTimer ();
	}
	
	void StartTimer () {
		timerStarted = true;
	}
}

