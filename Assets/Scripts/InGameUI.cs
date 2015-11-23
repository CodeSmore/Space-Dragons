using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InGameUI : MonoBehaviour {

	private Text myText;
	private int laserLevel;
	private int numSecondary;
	
	// Use this for initialization
	void Start () {
		myText = GetComponent<Text>();
		if (this.tag == "LaserLevel") {
			myText.text = "Laser Level 1"; 
		} else if (this.tag == "NumSecondary") {
			myText.text = "Secondary: 0";
		}
	}
	
	// Update is called once per frame
	void Update () {
		int laserLevel = PlayerController.getLaserLevel();
		int numSecondary = PlayerController.getNumSecondary();
		
		if (this.tag == "LaserLevel") {
			myText.text = "Laser Level " + laserLevel; 
		} else if (this.tag == "NumSecondary") {
			myText.text = "Secondary: " + numSecondary;
		}
	}
}
