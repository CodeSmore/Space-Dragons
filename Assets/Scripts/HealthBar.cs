using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour {
	
	private float currentHealth;
	private float maxHealth;
	private float healthRatio;
	
	private Image healthBarImage;
	
	

	// Use this for initialization
	void Start () {
		maxHealth = PlayerController.getMaxHealth ();
		healthBarImage = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		currentHealth = PlayerController.getCurrentHealth ();
		
		HandleHealthBar ();
	}
	
	void HandleHealthBar () {
		healthRatio = currentHealth / maxHealth;
		
		healthBarImage.fillAmount = healthRatio;
		
		if (healthRatio > .50) {
			healthBarImage.color = new Color32 ((byte)(255 * (1 - healthRatio) * 2), 255, 0, 255);
		} else {
			healthBarImage.color = new Color32 (255, (byte)(255 * healthRatio), 0, 255);
		}
	}
}
