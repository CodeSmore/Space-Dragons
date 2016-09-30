using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreDisplay : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// initializes 'myText' as the Text element of 'this'
		Text myText = GetComponent<Text>();
		
		// alters 'myText' to be the score as a string
		myText.text = ScoreKeeper.getScore().ToString ();
	}
}
