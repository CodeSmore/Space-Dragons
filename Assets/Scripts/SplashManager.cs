using UnityEngine;
using System.Collections;

public class SplashManager : MonoBehaviour {

	public float timer;
	
	private LevelManager levelManager;
	public GUITexture sketch;
	// Use this for initialization
	void Start () {
		timer = 0;
		levelManager = FindObjectOfType<LevelManager>();
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		
		if (timer >= 5 || Input.anyKeyDown) {
			levelManager.LoadLevel ("Start Menu");
		}
	}
}
