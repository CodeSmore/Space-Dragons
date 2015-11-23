using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProgressBar : MonoBehaviour {

	private float statusBarHeight = 1;
	private float maxScore;
	private float curScore = 0;
	private Image progressBarImage;
	private bool bossTime = false;
	private float maxHealth;
	private float curHealth = 50;
	private EnemyBehavior bossInstance;

	// Use this for initialization
	void Start () {
		GetComponent<RectTransform>().sizeDelta = new Vector2 (Screen.width, statusBarHeight);
		progressBarImage = GetComponent<Image>();
		progressBarImage.fillAmount = 0;
		
		
		maxScore = FormationController.getBossSpawnScore ();
	}
	
	// Update is called once per frame
	void Update () {
		curScore = ScoreKeeper.getScore ();
		if (curScore <= maxScore) {
			ManageProgressBar ();
		} else {
			ManageBossHealthBar ();
		}
			
	}
	
	void ManageProgressBar () {
		progressBarImage.fillAmount = curScore / maxScore;
	}
	
	void ManageBossHealthBar () {
		if (bossTime == false) {
			progressBarImage.fillAmount = 1;
		}
		
		if (GameObject.Find ("Boss(Clone)")) {
			bossTime = true;
			bossInstance = GameObject.Find ("Boss(Clone)").GetComponent<EnemyBehavior>();
			maxHealth = bossInstance.maxHealth;
			curHealth = bossInstance.health;
			
			progressBarImage.fillAmount = curHealth / maxHealth;
		} else if (bossTime == true) {
			progressBarImage.fillAmount = 0;
		}
	}
}
