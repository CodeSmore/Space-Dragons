using UnityEngine;
using System.Collections;

public class PlayerPrefsManager : MonoBehaviour {
	private const string PLAYER_NAME_KEY = "player_name";

	private const string HIGH_SCORE_NAME_1_KEY = "high_score_name_1";
	private const string HIGH_SCORE_NAME_2_KEY = "high_score_name_2";
	private const string HIGH_SCORE_NAME_3_KEY = "high_score_name_3";
	private const string HIGH_SCORE_NAME_4_KEY = "high_score_name_4";
	private const string HIGH_SCORE_NAME_5_KEY = "high_score_name_5";
	private const string HIGH_SCORE_NAME_6_KEY = "high_score_name_6";
	private const string HIGH_SCORE_NAME_7_KEY = "high_score_name_7";
	private const string HIGH_SCORE_NAME_8_KEY = "high_score_name_8";
	private const string HIGH_SCORE_NAME_9_KEY = "high_score_name_9";
	private const string HIGH_SCORE_NAME_10_KEY = "high_score_name_10";

	private const string HIGH_SCORE_1_KEY = "high_score_1";
	private const string HIGH_SCORE_2_KEY = "high_score_2";
	private const string HIGH_SCORE_3_KEY = "high_score_3";
	private const string HIGH_SCORE_4_KEY = "high_score_4";
	private const string HIGH_SCORE_5_KEY = "high_score_5";
	private const string HIGH_SCORE_6_KEY = "high_score_6";
	private const string HIGH_SCORE_7_KEY = "high_score_7";
	private const string HIGH_SCORE_8_KEY = "high_score_8";
	private const string HIGH_SCORE_9_KEY = "high_score_9";
	private const string HIGH_SCORE_10_KEY = "high_score_10";


	public static void SetPlayerName (string name) {
		PlayerPrefs.SetString(PLAYER_NAME_KEY, name);
	}

	public static string GetPlayerName () {
		return PlayerPrefs.GetString(PLAYER_NAME_KEY);
	}

	public static void SaveHighScores (string[] names, int[] scores) {
		PlayerPrefs.SetString(HIGH_SCORE_NAME_1_KEY, names[0]);
		PlayerPrefs.SetString(HIGH_SCORE_NAME_2_KEY, names[1]);
		PlayerPrefs.SetString(HIGH_SCORE_NAME_3_KEY, names[2]);
		PlayerPrefs.SetString(HIGH_SCORE_NAME_4_KEY, names[3]);
		PlayerPrefs.SetString(HIGH_SCORE_NAME_5_KEY, names[4]);
		PlayerPrefs.SetString(HIGH_SCORE_NAME_6_KEY, names[5]);
		PlayerPrefs.SetString(HIGH_SCORE_NAME_7_KEY, names[6]);
		PlayerPrefs.SetString(HIGH_SCORE_NAME_8_KEY, names[7]);
		PlayerPrefs.SetString(HIGH_SCORE_NAME_9_KEY, names[8]);
		PlayerPrefs.SetString(HIGH_SCORE_NAME_10_KEY, names[9]);


		PlayerPrefs.SetInt(HIGH_SCORE_1_KEY, scores[0]);
		PlayerPrefs.SetInt(HIGH_SCORE_2_KEY, scores[1]);
		PlayerPrefs.SetInt(HIGH_SCORE_3_KEY, scores[2]);
		PlayerPrefs.SetInt(HIGH_SCORE_4_KEY, scores[3]);
		PlayerPrefs.SetInt(HIGH_SCORE_5_KEY, scores[4]);
		PlayerPrefs.SetInt(HIGH_SCORE_6_KEY, scores[5]);
		PlayerPrefs.SetInt(HIGH_SCORE_7_KEY, scores[6]);
		PlayerPrefs.SetInt(HIGH_SCORE_8_KEY, scores[7]);
		PlayerPrefs.SetInt(HIGH_SCORE_9_KEY, scores[8]);
		PlayerPrefs.SetInt(HIGH_SCORE_10_KEY, scores[9]);
	}

	public static int[] GetHighScores () {
		int[] scoresArray = new int[10];

		scoresArray[0] = PlayerPrefs.GetInt(HIGH_SCORE_1_KEY);
		scoresArray[1] = PlayerPrefs.GetInt(HIGH_SCORE_2_KEY);
		scoresArray[2] = PlayerPrefs.GetInt(HIGH_SCORE_3_KEY);
		scoresArray[3] = PlayerPrefs.GetInt(HIGH_SCORE_4_KEY);
		scoresArray[4] = PlayerPrefs.GetInt(HIGH_SCORE_5_KEY);
		scoresArray[5] = PlayerPrefs.GetInt(HIGH_SCORE_6_KEY);
		scoresArray[6] = PlayerPrefs.GetInt(HIGH_SCORE_7_KEY);
		scoresArray[7] = PlayerPrefs.GetInt(HIGH_SCORE_8_KEY);
		scoresArray[8] = PlayerPrefs.GetInt(HIGH_SCORE_9_KEY);
		scoresArray[9] = PlayerPrefs.GetInt(HIGH_SCORE_10_KEY);

		return scoresArray;
	}

	public static string[] GetHighScoreNames () {
		string[] namesArray = new string[10];

		namesArray[0] = PlayerPrefs.GetString(HIGH_SCORE_NAME_1_KEY);
		namesArray[1] = PlayerPrefs.GetString(HIGH_SCORE_NAME_2_KEY);
		namesArray[2] = PlayerPrefs.GetString(HIGH_SCORE_NAME_3_KEY);
		namesArray[3] = PlayerPrefs.GetString(HIGH_SCORE_NAME_4_KEY);
		namesArray[4] = PlayerPrefs.GetString(HIGH_SCORE_NAME_5_KEY);
		namesArray[5] = PlayerPrefs.GetString(HIGH_SCORE_NAME_6_KEY);
		namesArray[6] = PlayerPrefs.GetString(HIGH_SCORE_NAME_7_KEY);
		namesArray[7] = PlayerPrefs.GetString(HIGH_SCORE_NAME_8_KEY);
		namesArray[8] = PlayerPrefs.GetString(HIGH_SCORE_NAME_9_KEY);
		namesArray[9] = PlayerPrefs.GetString(HIGH_SCORE_NAME_10_KEY);
	
		return namesArray;
	}
}
