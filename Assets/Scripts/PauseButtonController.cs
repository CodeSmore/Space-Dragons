using UnityEngine;
using System.Collections;

public class PauseButtonController : MonoBehaviour {

	public GameObject pauseMenu;
	// Use this for initialization
	void Start () {
		pauseMenu = GameObject.Find ("Pause Menu");
		pauseMenu.SetActive (false);
		
		gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void OnMouseDown () {
	
		pauseMenu.SetActive (true);
		PlayerMovement.TogglePlayMode ();
		// disable ship movement, stop game time, bring up menu
	}
	
	public void TogglePauseMenu () {
		if (pauseMenu.activeSelf) {
			pauseMenu.SetActive (false);
		} else {
			pauseMenu.SetActive (true);
		}
	}
}
