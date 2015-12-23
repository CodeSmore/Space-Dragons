﻿using UnityEngine;
using System.Collections;

public class ClingerAssaultController : MonoBehaviour {
	
	public GameObject clinger;
	public float timer = 0;
	public float spawnRate = 5;
	public int interval = 0;
	
	private Vector3 startPos;
	
	// position values that mark the booundaries
	// for where the enemy may move.
	private float xMin, xMax;
	private float padding = 2;
	
	// Use this for initialization
	void Start () {	
		Camera camera = Camera.main;
		float distance = transform.position.z - camera.transform.position.z;
		xMin = camera.ViewportToWorldPoint (new Vector3(0, 0, distance)).x + padding;
		xMax = camera.ViewportToWorldPoint (new Vector3(1, 1, distance)).x - padding;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		
		if (timer > interval) {
			InvokeRepeating ("Spawn", Random.Range (.1f, .9f), spawnRate);
			interval += 10;
		} 
	}
	
	private void Spawn () {
		startPos = new Vector3 (Random.Range (xMin, xMax), 30, 0);
		Instantiate (clinger, startPos, Quaternion.identity);
	}
}

