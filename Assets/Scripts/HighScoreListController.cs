using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HighScoreListController : MonoBehaviour {

	[SerializeField]
	private Text[] names = null, scores = null;


	// Use this for initialization
	void Start () {
		InputNames();
		InputScores();
	}

	void InputNames() {
		string[] namesArray = PlayerPrefsManager.GetHighScoreNames();

		for (int i = 0; i < namesArray.Length; i++) {
			names[i].text = namesArray[i];
		}
	}

	void InputScores() {
		int[] scoresArray = PlayerPrefsManager.GetHighScores();

		for (int i = 0; i < scoresArray.Length; i++) {
			scores[i].text = scoresArray[i].ToString();
		}
	}
	

}
