using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {
	
	// WEAPONS
	// Player's weapons assigned in the Inspector.
	public GameObject weapon;
	public GameObject weapon2;
	public GameObject weapon3;
	public GameObject weapon4;
	public GameObject secondaryWeapon;
	public GameObject shield;
	
	// Sprites
	public Sprite normSprite;
	public Sprite hitSprite;
	
	// Speed of projectile and the rate it is fired if fire button is held.
	public float projectileSpeed = 10;
	private float projectileRepeatRate = 0.2f;
	// Number of secondary weapons in reserve.
	private static int numSecWeapon = 0;
	public int bonusWeaponScore = 1000;
	private int numSecWeaponsEarned = 0;
	private static int laserLevel = 1;
	public float shieldPadding;
	
	// PLAYER MANIPULATION
	// Player health
	public static float curHealth;
	public static float maxHealth;
	
	// God mode enabled or not.
	public static bool godMode = false;
	public float shieldDuration = 10;
	
	// SoundController so that the SoundController object's methods can be utilized.
	private SoundController playerSounds;
	private GameObject myShield;
	
	// Timer for sprite changes
	private float spriteTimer = 0;
	// Timer for shield
	private float shieldTimer = 0;
	private float continuousDamageTimer = 0;
	private float blastOffSpeed = 0;
	
	// Use this for initialization
	void Start () {
		// Reset 'spriteTimer'
		spriteTimer = 0;
	
		// Reset numSecWeapon, numSecWeaponsEarned, laserLevel, and health
		numSecWeapon = 0;
		numSecWeaponsEarned = 0;
		laserLevel = 1;
		
		maxHealth = 1000;
		curHealth = maxHealth;
		
		// if godmode is enabled, give the player essentially infinite health.
		if (godMode) {
			maxHealth = 9999999;
			curHealth = 9999999;
			numSecWeapon = 99999;
			projectileRepeatRate = .1f;
			shieldDuration = 10000;
			SpawnShield ();
		}
		
		// Initializes our SoundController by finding the one in the scene.
		playerSounds = GameObject.FindObjectOfType<SoundController>();
		
		InvokeRepeating ("Fire", 0.000001f, projectileRepeatRate);
	}
	
	// Update is called once per frame
	void Update () {
		
		
		// Add secondary weapons based on score.
		if (ScoreKeeper.getScore () >= (bonusWeaponScore * (numSecWeaponsEarned + 1))) {
			numSecWeaponsEarned++;
			numSecWeapon++;
		}
		
		// Shoot labias!
		// If the player holds down the Fire1 button, projectiles are continuously fired
		// at projectileRepeatRate rate.
		// Extra: First is method name, then time before it is called, then time between calls.
		// .0001f is used instead of zero due to potential issues, though zero seems to work fine.

//		if (CrossPlatformInputManager.GetButtonDown ("Fire1")) {
//			
//		}
//		
//		if (Input.GetKeyDown (KeyCode.LeftControl) || Input.GetKeyDown (KeyCode.RightControl)) {
//			SecondaryFire ();
//		}
//		
//		// This cancels the 'InvokeRepeating'. Otherwise, it wont stop and 
//		// each time you pressed the Fire1 button, a new loop of projectiles would
//		// join the original. It can get quite hectic.
//		if (CrossPlatformInputManager.GetButton ("Fire1")) {
//			CancelInvoke ("Fire");
//		}
		
		// Return Sprite component of 'this' to normal sprite.
		spriteTimer += Time.deltaTime;
		shieldTimer += Time.deltaTime;
		
		if (spriteTimer >= .1) {
			this.GetComponent<SpriteRenderer>().sprite = normSprite;
		}
		
		if (shieldTimer >= shieldDuration) {
			Destroy (myShield);
		}
		
		// Function to destroy extra GameObjects
		CleanUp ();
		
		// Pressing Q button exits to the 'Win Screen'
		if (Input.GetKeyDown (KeyCode.Q)) {
			Die ();
		}
	}
	
	// Controls all aspects of firing a projectile from the player.
	// Instantiates the projectile, gives it velocity, and plays the sound effects.
	void Fire () {
		GameObject beam = null;
		// Offset puts the projectile one world unit above the ship. 
		// Extra: Unnecessary now, but was done to keep weapon from triggering ship.
		Vector3 offset = new Vector3 (0, 1, 0);
		// Projectile is Instantiated and set to 'beam' so it can be manipulated.
		if (godMode == false) {
			if (laserLevel == 1) {
				beam = Instantiate (weapon, transform.position + offset, Quaternion.identity) as GameObject;
			} else if (laserLevel == 2){
				beam = Instantiate (weapon2, transform.position + offset, Quaternion.identity) as GameObject;
			} else if (laserLevel == 3){
				beam = Instantiate (weapon3, new Vector3 (transform.position.x + 0.5f, transform.position.y + 1, 0), Quaternion.identity) as GameObject;
				GameObject beam2 = Instantiate (weapon3, new Vector3 (transform.position.x - 0.5f, transform.position.y + 1, 0), Quaternion.identity) as GameObject;
				beam2.GetComponent<Rigidbody2D>().velocity = new Vector3 (0, projectileSpeed, 0);
			} else if (laserLevel == 4) {
				beam = Instantiate (weapon4,transform.position + offset, Quaternion.identity) as GameObject;
				GameObject beam2 = Instantiate (weapon4, new Vector3 (transform.position.x - 0.25f, transform.position.y + 1, 0), Quaternion.identity) as GameObject;
				GameObject beam3 = Instantiate (weapon4, new Vector3 (transform.position.x + 0.25f, transform.position.y + 1, 0), Quaternion.identity) as GameObject;
				
				beam2.GetComponent<Rigidbody2D>().velocity = new Vector3 (projectileSpeed / 2, projectileSpeed / 2, 0);
				beam3.GetComponent<Rigidbody2D>().velocity = new Vector3 (projectileSpeed / -2, projectileSpeed / 2, 0);
			}
		} else {
			laserLevel = 3;
			beam = Instantiate (weapon4,transform.position + offset, Quaternion.identity) as GameObject;
			GameObject beam2 = Instantiate (weapon4, new Vector3 (transform.position.x - 0.25f, transform.position.y + 1, 0), Quaternion.identity) as GameObject;
			GameObject beam3 = Instantiate (weapon4, new Vector3 (transform.position.x + 0.25f, transform.position.y + 1, 0), Quaternion.identity) as GameObject;
			
			beam2.GetComponent<Rigidbody2D>().velocity = new Vector3 (projectileSpeed / 2, projectileSpeed / 2, 0);
			beam3.GetComponent<Rigidbody2D>().velocity = new Vector3 (projectileSpeed / -2, projectileSpeed / 2, 0);
		}
		
		// Velocity is set using the Rigidbody2D attached to the projectile.
		if (beam != null) {
			if (laserLevel == 1) {
				beam.GetComponent<Rigidbody2D>().velocity = new Vector3 (0, projectileSpeed / 2, 0);
				projectileRepeatRate = 0.5f;
				CancelInvoke ("Fire");
			} else {
				beam.GetComponent<Rigidbody2D>().velocity = new Vector3 (0, projectileSpeed, 0);
				projectileRepeatRate = 0.2f;
				CancelInvoke ("Fire");
			}
			
			InvokeRepeating ("Fire", projectileRepeatRate, projectileRepeatRate);
		}
		// Plays the weapon sound!
		playerSounds.PlayerFireSound();
	}
	
	void SecondaryFire () {
		// Offset puts the projectile one world unit above the ship. 
		// Extra: Unnecessary now, but was done to keep weapon from triggering ship.
		Vector3 offset = new Vector3 (0, 1, 0);
		
		if (numSecWeapon > 0) {
			GameObject weapon = Instantiate (secondaryWeapon, transform.position + offset, Quaternion.identity) as GameObject;
			GameObject initialPos = new GameObject();
			initialPos.transform.position = transform.position;
			
			weapon.transform.parent = initialPos.transform;
			playerSounds.PlayerLaserangFireSound();
			numSecWeapon--;
		}
		
	}
	
	// Continuous Damage from a persistent attack (shocker attack, laser, ect)
	void OnTriggerStay2D (Collider2D collider) {
		
		
		
		if (collider.gameObject.tag == "Enemy Projectiles") {
			continuousDamageTimer += Time.deltaTime; 
			
			Projectile missile = collider.GetComponent<Projectile>();
			
			curHealth -= missile.GetDamage();
			
			// Sprite changes to all white on impact
			this.GetComponent<SpriteRenderer>().sprite = hitSprite;
			spriteTimer = 0;
			
			// Plays damage sound effect
			playerSounds.PlayerShockSound ();
		}
		
		// Once health reaches zero or below, the player object is destroyed
		// and the 'Win Screen' scene is loaded.
		if (curHealth <= 0) {
			Die ();
		}
	}
	
	// Called when a collider triggers the player.
	// Controls what happens when the player is hit by a projectile.
	void OnTriggerEnter2D (Collider2D collider) {
		
		// We try to find a projectile that the collider is attached to.
		Projectile missile = collider.GetComponent<Projectile>();
		GameObject collisionObject = collider.gameObject;
		
		// If we are correct and it is a weapon, then we take damage
		// and destroy the weapon.
		if (collisionObject.tag == "PowerUp") {
			if (collisionObject.name == "Shield Drop") {
				SpawnShield ();
			} else if (collisionObject.name == "Laser PowerUp") {
				laserLevel = Mathf.Clamp (++laserLevel, 1, 4);
			}
			
			Destroy (collisionObject);
		} else if (collisionObject.tag == "Enemy") {
			Destroy (collisionObject);
			if (myShield) {
				Destroy (myShield);
			} else {
				curHealth -= 100;
				
				// Plays damage sound effect
				playerSounds.PlayerDamageSound ();
			}
			
		} else if (!myShield && missile && missile.tag != "PlayerLaser") {
			missile.Hit ();
			curHealth -= missile.GetDamage();
			// Sprite changes to all white on impact
			this.GetComponent<SpriteRenderer>().sprite = hitSprite;
			spriteTimer = 0;
			
			// Plays damage sound effect
			playerSounds.PlayerDamageSound ();
		}
		
		
		// Once health reaches zero or below, the player object is destroyed
		// and the 'Win Screen' scene is loaded.
		if (curHealth <= 0) {
			Die ();
		}
	}
	
	void SpawnShield () {
		shieldTimer = 0;
		if (!myShield) {
			myShield = Instantiate (shield, new Vector3 (transform.position.x, transform.position.y + shieldPadding, 0), Quaternion.identity) as GameObject;
			myShield.transform.parent = transform;
		}
	}
	
	// Controls player death. The player is destroyed, and the 'Win Screen' scene is loaded.
	void Die () {
		LevelManager man = GameObject.Find ("LevelManager").GetComponent<LevelManager>();
		man.LoadLevel ("Lose Screen");
		Destroy (gameObject);
	}
	
	// Method called in update that destroys unncessary objects.
	void CleanUp () {
		GameObject garbage = GameObject.Find ("New Game Object");
		
		// If 'garbage' is not equal to 'null'...
		if (garbage) {
			// DESTROY IT!
			if (garbage.transform.childCount <= 0) {
				Destroy (garbage);
			}
		}
		// INSERT EVIL LAUGHTER
	}
	
//	void OnGui() {
//		GUI.Box (new Rect(10, 10, Screen.width / 2 / (maxHealth / curHealth), 10), "Player Health");
//	}

	// Ship leaves screen after boss is defeated.
	public void BlastOff () {
		// accelerate ship speed upwards until no longer in camera view.
		gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2 (0, blastOffSpeed);
		blastOffSpeed++;
		if (gameObject.transform.position.y >= 10) {
			Destroy (gameObject);
		}
	}
	
	public static int getLaserLevel () {
		return laserLevel;
	}
	
	public static int getNumSecondary () {
		return numSecWeapon;
	}
	
	public static float getCurrentHealth () {
		return curHealth;
	}
	
	public static float getMaxHealth () {
		return maxHealth;
	}
}
