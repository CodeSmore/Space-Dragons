using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {
	
	// Private and static so it cannot be changed outside of this script
	// AND so it's value is consistent throughout the game.
	private static int score = 0;
	
	// Text component that displays score during play.
	private Text myText;
	
	// Use this for initialization
	void Start () {
		// Sets 'myText' to 'Score' object's 'Text' component in inspector.
		myText = GetComponent<Text>();
		
		// Resets score in case of old or garbage value.
		EnemyBehavior.resetNumEnemiesDestroyed();
		Reset ();
	}
	
	// Method for adding to score and altering myText to reflect the change.
	public void Score (int points) {
		score += points;
		
		// ToString() is used as 'score' is an int and cannot be set to a string otherwise.
		myText.text = score.ToString();
	}
	
	// Method to reset 'score' to 0.
	public static void Reset () {
		score = 0;
	}
	
	// Method to get 'score'. Used so that 'score' can remain private.
	public static int getScore () {
		return score;
	}
}
