using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	// How much damage a projectile does.
	public float damage = 100f;
	public float rotationSpeed = 0;
	
	void Update () {
		RotateProjectile ();
	}
	
	// returns the damage to...whomever calls this method.
	public float GetDamage () {
		return damage;
	}
	
	// Signals that the projectile has hit something and destroys the projectile.
	public void Hit () {
		Destroy (gameObject);
	}
	
	public void RotateProjectile () {
		this.transform.Rotate (0f, 0f, rotationSpeed);
	}
}
