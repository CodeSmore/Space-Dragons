using UnityEngine;
using System.Collections;

public class SplashManager : MonoBehaviour {

	public float timer;

	public GUITexture sketch;

	private LevelManager levelManager;

	// Use this for initialization
	void Start () {
		levelManager = GameObject.FindObjectOfType<LevelManager>();
		timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		
		if (timer >= 5 || Input.anyKeyDown) {
			levelManager.LoadLevel ("Start Menu");
		}
	}
}
