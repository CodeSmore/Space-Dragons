using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsController : MonoBehaviour {

	public Text godModeText;
	public Text musicText;
	
	private MusicPlayer musicPlayer;
	// Turns GodMode on/off
	void Start () {
		musicPlayer = GameObject.FindObjectOfType<MusicPlayer>();
	}
	
	void Update () {
		if (PlayerController.godMode == true) {
			godModeText.text = "ON";
		} else {
			godModeText.text = "OFF";
		} 
		
		if (musicPlayer) {
			if( musicPlayer.paused == true) {
				musicText.text = "OFF";
			} else {
				musicText.text = "ON";
			}
		} 
	}
	
	public void GodMode () {
		if (PlayerController.godMode == true) {
			PlayerController.godMode = false;
			godModeText.text = "OFF";
		} else {
			PlayerController.godMode = true;
			godModeText.text = "ON";
		}
	}
	
	public void Music () {
		if (musicPlayer.paused == true) {
			musicPlayer.ToggleMusic ();
			musicText.text = "ON";
		} else {
			musicPlayer.ToggleMusic ();
			musicText.text = "OFF";
		}
	}
}
