using UnityEngine;
using System.Collections;

public class ClingerBehavior : MonoBehaviour {

	public GameObject weapon;
	public float health = 250;
	public float padding = 0;
	public float secondsBetweenShots = 1;
	public float weaponSpeed = 1;
	
	public Sprite normSprite;
	public Sprite hitSprite;
	
	private Camera mainCamera;
	private SoundController enemySounds;
	private GameObject player;
	private float xMin, xMax, yMin, yMax;
	private float spriteTimer = 0;
	private int scoreValue = 200;
	private Vector3 movementForce;
	
	// Use this for initialization
	void Start () {
		enemySounds = FindObjectOfType<SoundController>();
		player = GameObject.FindGameObjectWithTag ("Player");
		mainCamera = Camera.main;
		float distance = transform.position.z - mainCamera.transform.position.z;
		xMin = mainCamera.ViewportToWorldPoint (new Vector3(0, 0, distance)).x + padding;
		xMax = mainCamera.ViewportToWorldPoint (new Vector3(1, 1, distance)).x - padding;
		yMin = mainCamera.ViewportToWorldPoint (new Vector3(0, 0, distance)).y + padding;
		yMax = mainCamera.ViewportToWorldPoint (new Vector3(1, 1f, distance)).y - padding;
		
		
	}
	
	// Update is called once per frame
	void Update () {
		spriteTimer += Time.deltaTime;
		
		if (spriteTimer >= .1) {
			this.GetComponent<SpriteRenderer>().sprite = normSprite;
		}
		
		// Only fires when clinger is in view of Camera.main, other wise the fire order is canceled
		if (ClingerIsInView () /* in screen view */) {
			if (!IsInvoking ("Fire"))  {
				InvokeRepeating ("Fire", 0.000001f, secondsBetweenShots);
			} 
		} else {
			CancelInvoke ("Fire");
		}
	}
	
	void Fire () {
		GameObject beam = null;
		
		// Projectile is Instantiated and set to 'beam' so it can be manipulated.
		beam = Instantiate (weapon, transform.position, Quaternion.identity) as GameObject;
		Vector3 vectorToTarget = (player.transform.position - transform.position);
		vectorToTarget.Normalize ();
		
		movementForce = vectorToTarget * weaponSpeed;
		beam.GetComponent<Rigidbody2D>().AddForce (movementForce);
		
		float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
		Quaternion q = Quaternion.AngleAxis(angle + 90, Vector3.forward);				
		beam.transform.rotation = q;
		
	}	
	
	bool ClingerIsInView () {
		if (transform.position.x > xMin && transform.position.x < xMax && transform.position.y > yMin && transform.position.y < yMax) {
			return true;
		} else {
			return false;
		}
	}
	
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
		FindObjectOfType<ScoreKeeper>().Score (scoreValue);
		
		if (enemySounds /* exists */ ) {
			enemySounds.EnemyDeathSound();
		}
		
		Destroy (gameObject);
	}
	
	public Vector3 GetMovementForce () {
		return movementForce;
	}
}
