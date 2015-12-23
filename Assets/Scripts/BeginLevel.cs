using UnityEngine;
using System.Collections;

public class BeginLevel : MonoBehaviour {
	
	private GameObject enemies;
	private Scroller background;
	private bool timerStarted = false;
	
	private float timer = 0;
	
	public float endChargeTime = 50;
	
	void Start () {
		enemies = GameObject.Find ("Enemies");
		enemies.SetActive (false);
		
		background = FindObjectOfType<Scroller>();
	}
	
	void Update () {
		if (timerStarted) {
			timer += Time.deltaTime;
			
			foreach (Transform child in gameObject.transform) {
				Destroy (child.gameObject);
			}
			
			if (timer > endChargeTime) {
				PlayerMovement.SetMovementEnabled (true);
				Destroy (gameObject);
			}
		}
	}
	
	
	public void OnMouseDown () {
		enemies.SetActive (true);
		background.BeginScrolling ();
		
		
		
		StartTimer ();
	}
	
	void StartTimer () {
		timerStarted = true;
	}
}

