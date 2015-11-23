using UnityEngine;
using System.Collections;

public class DroneBehavior : MonoBehaviour {
	
	public Sprite normSprite;
	public Sprite hitSprite;
	// Holds each enemy's health.
	public float health = 150;
	public float maxHealth;

	// How many points a kill of this enemy is worth.
	public int scoreValue = 150;
	// Number of enemies destroyed
	private static int numEnemiesDestroyed = 0;
	private float timer = 0;
	
	public GameObject shieldDrop;
	public float dropRate = 0.05f;
	public float dropSpeed = 2;
	
	// ScoreKeeper and SoundController objects brought in to utilize their methods.
	private ScoreKeeper scoreKeeper;
	private SoundController enemySounds; 
	
	void Start () {
		// Reset 'timer'
		timer = 0;
		
		maxHealth = health;
		// So, two ways to grab a script. The first is more specific, including the actual 
		// game object it's attached to. The second is very general and should only be used
		// when there is only one instance of the script.
		scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
		enemySounds = FindObjectOfType<SoundController>();
	}
	
	void Update () {	
		timer += Time.deltaTime;
		
		if (timer >= .1) {
			this.GetComponent<SpriteRenderer>().sprite = normSprite;
		}
	}
	
	// Called when a projectile hits an enemy.
	void OnTriggerEnter2D (Collider2D collider) {
		
		// Grabs the projectile that has hit using it's collider.
		Projectile missile = collider.GetComponent<Projectile>();
		
		// Tests that it WAS a missile that caused the trigger.
		// "if missile exists..."
		if (missile) {
			// Calls the Projectile method for what happens to a projectile when
			// it hits something.
			missile.Hit ();
			// Reduces the health of the enemy based on the 'Projectiles' damage rating.
			health -= missile.GetDamage();
			
			this.GetComponent<SpriteRenderer>().sprite = hitSprite;
			timer = 0;
			
			enemySounds.EnemyDamageSound ();
		}
		
		
		// When health reaches or goes below zero, it is destroyed.
		if (health <= 0) {
			Die ();
		}
	}
	
	// Our funeral home for enemies.
	// Plays the death sound, destroyes the enemy, and even updates the score
	// based on the point value of the enemy destroyed.
	// Increments number of destroyed enemies.
	void Die () {
		float probability = dropRate;
		float random = Random.value;
		
		Debug.Log ("probability: " + probability);
		Debug.Log ("random: " + random);
		if (numEnemiesDestroyed >= 6 && random <= probability) {
			DropShit ();
		}
		
		enemySounds.EnemyDeathSound();
		Destroy (gameObject);
		scoreKeeper.Score (scoreValue);
		numEnemiesDestroyed++;
	}
	
	void DropShit () {
		GameObject shield = Instantiate (shieldDrop, transform.position, Quaternion.identity) as GameObject;
		shield.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -dropSpeed);
	}
	
	public static int getNumEnemiesDestroyed () {
		return numEnemiesDestroyed;
	}
	
	public static void resetNumEnemiesDestroyed () {
		numEnemiesDestroyed = 0;
	}
}
