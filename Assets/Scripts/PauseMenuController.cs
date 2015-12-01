using UnityEngine;
using System.Collections;

public class PauseMenuController : MonoBehaviour {

	public GameObject pauseMenu;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void TogglePauseMenu () {
		if (GameObject.Find ("Pause Menu(Clone)")) {
			Destroy (GameObject.Find ("Pause Menu(Clone)"));
		} else {
			Instantiate (pauseMenu);
			GameObject.Find ("Pause Menu(Clone)").transform.parent = GameObject.Find ("Canvas").transform;
		}
	}
}
