using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HighScoreManager : MonoBehaviour {
// this script compares the new score with the PlayerPrefs data, and replaces old scores with new ones
// also displays "NEW HIGH SCORE" in the event of a new high score...obviously

	[SerializeField]
	private GameObject newHighScoreText = null;

	private int newScore;

	// Use this for initialization
	void Start () {
		newScore = ScoreKeeper.getScore();

		if (isNewHighScore()) {
			newHighScoreText.SetActive(true);
			AddScoreToHighScoreList();
		}
	}

	bool isNewHighScore () {
		int[] highScores = PlayerPrefsManager.GetHighScores();

		foreach (int oldScore in highScores) {
			if (oldScore < newScore) {
				return true;
			}
		}

		return false;
	}

	void AddScoreToHighScoreList () {
		int[] oldHighScores = PlayerPrefsManager.GetHighScores();
		int[] newHighScores = new int[oldHighScores.Length];

		string[] oldNames = PlayerPrefsManager.GetHighScoreNames();
		string[] newNames = new string[oldNames.Length];

		for (int i = oldHighScores.Length - 1; i >= 0; i--) {	
			if (i == 0 || newScore < oldHighScores[i-1]) {
				// add to list, end loop
				for (int j = 0; j < i; j++) {
					newHighScores[j] = oldHighScores[j];
					newNames[j] = oldNames[j];
				}

				newHighScores[i] = newScore;
				newNames[i] = PlayerPrefsManager.GetPlayerName();

				for (int j = i + 1; j < newHighScores.Length; j++) {
					newHighScores[j] = oldHighScores[j-1];
					newNames[j] = oldNames[j-1];
				}

				i = -1;
			}
		}

		PlayerPrefsManager.SaveHighScores(newNames, newHighScores);
	}
}
