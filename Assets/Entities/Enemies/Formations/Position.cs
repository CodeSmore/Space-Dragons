using UnityEngine;
using System.Collections;

public class Position : MonoBehaviour {

	public GameObject enemyPrefab;
	private bool hasOwnEnemyPrefab = false;

	private float boundaryLeftEdge;
	private float boundaryRightEdge;
	
	private float padding = 0.7f;
	
	private Rigidbody2D thisRigidbody;
	
	public int direction = 1;
	public float speed = 1;
	
	Camera foregroundCamera;
	// Draws a wire sphere w/ a radius of 1 over each position in the formation.
	void OnDrawGizmos () {
		Gizmos.DrawWireSphere (transform.position, 1);
	}
	
	// Added Awake() b/c GetHasOwnEnemyPrefab () is being called in another scripts Start ()
	void Awake () {
		if (enemyPrefab != null) {
			hasOwnEnemyPrefab = true;
		}
	}
	
	void Start () {
		thisRigidbody = GetComponent<Rigidbody2D>();
		if (thisRigidbody != null) {
			foregroundCamera = GameObject.Find ("Foreground Camera").GetComponent<Camera>();
		
			boundaryLeftEdge = foregroundCamera.ViewportToWorldPoint (new Vector3(0, 0, 0)).x + padding;
			boundaryRightEdge = foregroundCamera.ViewportToWorldPoint (new Vector3(1, 1, 0)).x - padding;
		}
	}
	
	void Update () {
		if (thisRigidbody != null) {
			if (transform.position.x <= boundaryLeftEdge) { 
				direction = 1;
			} else if (transform.position.x >= boundaryRightEdge) {
				direction = -1;
			}
			
			this.transform.position = new Vector3 (
				transform.position.x + Time.deltaTime * direction * speed, 
				transform.position.y,
				0
			);
		}
	}
	
	public Object InstantiateEnemyPrefab () {
		return Instantiate(enemyPrefab, transform.position, Quaternion.identity);
	}
	
	public bool GetHasOwnEnemyPrefab () {
		return hasOwnEnemyPrefab;
	}
}
