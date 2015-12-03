using UnityEngine;
using System.Collections;

public class EmptySpaceController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnMouseDown () {
		PlayerMovement.TogglePlayMode ();
	}
	
}
