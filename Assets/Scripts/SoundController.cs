using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {

	// Public AudioClips assigned in the Inspector
	public AudioClip playerFire;
	public AudioClip enemyFire;
	public AudioClip enemyDies;
	public AudioClip laserangFire;
	public AudioClip enemyDamage;
	public AudioClip playerDamage;

	// All three methods simply played their designated sound.
	// Extra: AudioSource.PlayClipAtPoint is used in case 'this' is destroyed, so that the sound will play anyways.
	public void PlayerFireSound () {
		AudioSource.PlayClipAtPoint (playerFire, transform.position);
	}
	
	public void EnemyFireSound () {
		AudioSource.PlayClipAtPoint (enemyFire, transform.position);
	}
	
	public void EnemyDeathSound () {
		AudioSource.PlayClipAtPoint (enemyDies, transform.position);
	}
	
	public void PlayerLaserangFireSound () {
		AudioSource.PlayClipAtPoint (laserangFire, transform.position);
	}
	
	public void EnemyDamageSound () {
		AudioSource.PlayClipAtPoint (enemyDamage, transform.position);
	}
	
	public void PlayerDamageSound () {
		AudioSource.PlayClipAtPoint (playerDamage, transform.position);
	}
		
}
