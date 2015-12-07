using UnityEngine;
using System.Collections;

public class ShockerBehavior : MonoBehaviour {
	
	public Sprite normSprite;
	public Sprite hitSprite;
	public GameObject weapon;
	public GameObject explosion;
	
	// Holds each enemy's health.
	public float health = 150;
	
	// How many points a kill of this enemy is worth.
	public int scoreValue = 150;
	// Number of enemies destroyed
	private static int numEnemiesDestroyed = 0;
	private float spriteTimer = 0;
	private Vector3 movementTargetPos;
	
	private float movementTimer = 0;
	public float spinSpeed = 20;
	public float movementSpeed = 20;
	
	public GameObject shieldDrop;
	public float dropRate = 0.05f;
	public float dropSpeed = 2;
	
	// ScoreKeeper and SoundController objects brought in to utilize their methods.
	private ScoreKeeper scoreKeeper;
	private SoundController enemySounds; 
	
	void Start () {
		// Reset 'spriteTimer'
		spriteTimer = 0;
		movementTimer = 0;
		
		// So, two ways to grab a script. The first is more specific, including the actual 
		// game object it's attached to. The second is very general and should only be used
		// when there is only one instance of the script.
		scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
		enemySounds = FindObjectOfType<SoundController>();
		
		movementTargetPos = Camera.main.ViewportToWorldPoint (new Vector3 (Random.Range (0.1f, 0.9f), 0.5f, 0));
		Vector3 vectorToTarget = movementTargetPos - transform.position;
		GetComponent<Rigidbody2D>().AddForce (vectorToTarget * movementSpeed);
	}
	
	void Update () {	
		spriteTimer += Time.deltaTime;
		movementTimer += Time.deltaTime;
		
		if (spriteTimer >= .1) {
			this.GetComponent<SpriteRenderer>().sprite = normSprite;
		}
		
		Move ();
		
		// Once shocker reaches origin, detonate and release lightning attack
		if (transform.position.y <= movementTargetPos.y) {
			Attack ();
		}
	}
	
	void Attack () {
		Quaternion currRotationEuler = transform.rotation;
	
		Instantiate (weapon, transform.position, currRotationEuler);
		Instantiate (weapon, transform.position, Quaternion.Euler (currRotationEuler.eulerAngles.x, currRotationEuler.eulerAngles.y, currRotationEuler.eulerAngles.z + 90f));
		Instantiate (weapon, transform.position, Quaternion.Euler (currRotationEuler.eulerAngles.x, currRotationEuler.eulerAngles.y, currRotationEuler.eulerAngles.z + 180f));
		Instantiate (weapon, transform.position, Quaternion.Euler (currRotationEuler.eulerAngles.x, currRotationEuler.eulerAngles.y, currRotationEuler.eulerAngles.z + 270f));
		
		Instantiate (explosion, transform.position, Quaternion.identity);
		Destroy (gameObject);
	}
	
	void Move () {
		// Spin, spin, zala bim!
		transform.rotation = Quaternion.Euler (new Vector3 (0, 0, spinSpeed * Time.timeSinceLevelLoad));
	}
	
	// Called when a projectile hits an enemy.
	void OnTriggerEnter2D (Collider2D collider) {
		
		// Grabs the projectile that has hit using it's collider.
		Projectile missile = collider.GetComponent<Projectile>();
		
		// Tests that it WAS a missile that caused the trigger.
		// "if missile exists..."
		if (missile && missile.tag != "Enemy Projectiles") {
			// Calls the Projectile method for what happens to a projectile when
			// it hits something.
			missile.Hit ();
			// Reduces the health of the enemy based on the 'Projectiles' damage rating.
			health -= missile.GetDamage();
			
			this.GetComponent<SpriteRenderer>().sprite = hitSprite;
			spriteTimer = 0;
			
			if (enemySounds /*exists*/) {
				enemySounds.EnemyDamageSound ();
			}
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
