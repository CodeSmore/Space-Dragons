﻿using UnityEngine;
using System.Collections;

public class ClingerBehavior : MonoBehaviour {

	public GameObject weapon;
	public float padding = 0;
	public float secondsBetweenShots = 1;
	public float weaponSpeed = 1;
	
	private Camera mainCamera;
	private GameObject player;
	private float xMin, xMax, yMin, yMax;
	
	// Use this for initialization
	void Start () {
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
		beam.GetComponent<Rigidbody2D>().AddForce (vectorToTarget * weaponSpeed);
		
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
}
