using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NameSubmitter : MonoBehaviour {

	[SerializeField]
	private Text nameInputText = null;

	private LevelManager levelManager;

	void Start () {
		levelManager = GameObject.FindObjectOfType<LevelManager>();
	}

	public void SubmitName () {
		PlayerPrefsManager.SetPlayerName(nameInputText.text);

		levelManager.LoadLevel("Test Lvl 2");
	}
}
