using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {
	
	public Sprite normSprite;
	public Sprite hitSprite;
	// Holds each enemy's health.
	public float health = 150;
	public float maxHealth;
	// Speed of enemy lasers
	public float projectileSpeed = 10;
	// Shots/Second
	public float shotsPerSecond = 0.5f;
	// The projectile attached in the Inspector.
	public GameObject projectile;
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
		// probability is ACTUALLY shots/second, but needs to be calculated in update due
		// to the variable nature of framerate.
		float probability = Time.deltaTime * shotsPerSecond;
		
		// Random.value is always inclusively between 0.0 and 1.0. This makes it a probability of
		// shooting, making the shooting patterns of enemies less predictable.
		if (Random.value < probability) {
			Fire ();
		}
		
		timer += Time.deltaTime;
		
		if (timer >= .1) {
			this.GetComponent<SpriteRenderer>().sprite = normSprite;
		}
	}
	
	// Fires a projectile from the enemy down onto the player.
	void Fire () {
		// Instantiates a 'projectile' GameObject, slightly below the enemy...
		if (this.name != "Tank(Clone)") {
			GameObject missile;
			float xAxisSpeed = 0; 
			float yAxisSpeed = projectileSpeed;
			
			if (this.tag == "Boss") {
				float random = Random.value;
				
				// straight
				if (random < .2)
					missile = Instantiate (projectile, transform.position + new Vector3 (0, -1.5f, 0), Quaternion.identity) as GameObject;
				// med left
				else if (random < .4) {
					missile = Instantiate (projectile, transform.position + new Vector3 (-1f, -1.5f, 0), Quaternion.identity) as GameObject;
					missile.transform.Rotate (0f, 0f, 340f);
					xAxisSpeed = -7f;
				// far left
				} else if (random < .6) {
					missile = Instantiate (projectile, transform.position + new Vector3 (-1.75f, -1.5f, 0), Quaternion.identity) as GameObject;
					missile.transform.Rotate (0f, 0f, 320f);
					xAxisSpeed = -9f;
					yAxisSpeed = 10f;
				// med right
				} else if (random < .8) {
					missile = Instantiate (projectile, transform.position + new Vector3 (1f, -1.5f, 0), Quaternion.identity) as GameObject;
					missile.transform.Rotate (0f, 0f, 25f);
					xAxisSpeed = 7f;
				// far right
				} else {
					missile = Instantiate (projectile, transform.position + new Vector3 (1.75f, -1.5f, 0), Quaternion.identity) as GameObject;
					missile.transform.Rotate (0f, 0f, 40f);
					xAxisSpeed = 9f;
					yAxisSpeed = 10f;
				}
			} else {
				if (this.transform.parent.name == "LeftMedEnemy") {
					missile = Instantiate (projectile, transform.position + new Vector3 (0.75f, -1f, 0), transform.rotation) as GameObject;
					xAxisSpeed = 6f;
				} else if (this.transform.parent.name == "RightMedEnemy") {
					missile = Instantiate (projectile, transform.position + new Vector3 (-0.75f, -1f, 0), transform.rotation) as GameObject;
					xAxisSpeed = -6f;
				} else {
					missile = Instantiate (projectile, transform.position + new Vector3 (0, -1f, 0), Quaternion.identity) as GameObject;
				}
				
			}
			// Then sets the velocity using it's Rigidbody2D component...
			missile.GetComponent<Rigidbody2D>().velocity = new Vector2(xAxisSpeed, -yAxisSpeed);
			// And finally, utilizes our SoundController to play the sound effect.
			enemySounds.EnemyFireSound ();
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
		Instantiate (shieldDrop, transform.position, Quaternion.identity);
	}
	
	public static int getNumEnemiesDestroyed () {
		return numEnemiesDestroyed;
	}
	
	public static void resetNumEnemiesDestroyed () {
		numEnemiesDestroyed = 0;
	}
}
