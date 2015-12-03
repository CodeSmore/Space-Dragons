using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	void Start () {
		Time.timeScale = 1;
	}
	// Loads a new scene based on the entered name.
	public void LoadLevel(string name){
		Application.LoadLevel (name);
	}
	
	public void LoadLevel (int level) {
		Application.LoadLevel (level);
	}
	
	public void LoadNextLevel () {
		Application.LoadLevel (Application.loadedLevel + 1);
	}

	// Quits the game. Only works in finished builds.
	public void QuitRequest(){
		Debug.Log ("Quit requested");
		Application.Quit ();
	}

}
