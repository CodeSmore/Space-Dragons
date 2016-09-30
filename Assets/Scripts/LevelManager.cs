using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	void Start () {
		Time.timeScale = 1;
	}
	// Loads a new scene based on the entered name.
	public void LoadLevel(string name){
		SceneManager.LoadScene (name);
	}
	
	public void LoadLevel (int level) {
		SceneManager.LoadScene (level);
	}
	
	public void LoadNextLevel () {
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
	}

	// Quits the game. Only works in finished builds.
	public void QuitRequest(){
		Debug.Log ("Quit requested");
		Application.Quit ();
	}

}
