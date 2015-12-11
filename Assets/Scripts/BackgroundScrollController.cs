using UnityEngine;
using System.Collections;

public class BackgroundScrollController : MonoBehaviour {

	public float scrollSpeed = 1;
	public int direction = -1;
	public int posOfMesopauseTransition = -455;
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y <= posOfMesopauseTransition) {
			scrollSpeed = 1;
		} 
		
		transform.position = new Vector3 (transform.position.x, transform.position.y + scrollSpeed * Time.deltaTime * direction, transform.position.z);
	}
}
