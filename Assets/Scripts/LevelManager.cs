using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	// Loads a new scene based on the entered name.
	public void LoadLevel(string name){
		Debug.Log ("New Level load: " + name);
		Application.LoadLevel (name);
	}

	// Quits the game. Only works in finished builds.
	public void QuitRequest(){
		Debug.Log ("Quit requested");
		Application.Quit ();
	}

}
