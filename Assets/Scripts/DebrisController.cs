using UnityEngine;
using System.Collections;

public class DebrisController : MonoBehaviour {

	public float fallSpeed = 10;
	public float spinSpeed = 50;
	public float health = 450;
	public int scoreValue = 100;
	
	public Sprite normSprite;
	public Sprite hitSprite;
	
	private float spriteTimer = 0;
	private SoundController enemySounds;
	private ScoreKeeper scoreKeeper;
	private Vector2 movementVelocity;
	
	
	// Use this for initialization
	void Start () {
		movementVelocity = new Vector2 (Random.Range (-1f, 1f), -fallSpeed);
		GetComponent<Rigidbody2D>().velocity = movementVelocity;
		
		enemySounds = GameObject.FindObjectOfType<SoundController>();
		scoreKeeper = GameObject.FindObjectOfType<ScoreKeeper>();
	}
	
	// Update is called once per frame
	void Update () {
	
		spriteTimer += Time.deltaTime;
		
		if (spriteTimer >= .1) {
			this.GetComponent<SpriteRenderer>().sprite = normSprite;
		}
		
		// Spin
		transform.rotation = Quaternion.Euler (new Vector3 (0, 0, spinSpeed * Time.timeSinceLevelLoad));
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
	
	void Die () {
		enemySounds.EnemyDeathSound();
		
		foreach (Transform child in transform) {
			child.gameObject.AddComponent<Rigidbody2D>().isKinematic = true;	
			child.gameObject.GetComponent<Rigidbody2D>().velocity = movementVelocity;
			
			child.parent = null;
		}
		
		scoreKeeper.Score (scoreValue);
		
		Destroy (gameObject);
	}
}
