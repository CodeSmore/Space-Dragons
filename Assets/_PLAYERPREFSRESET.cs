using UnityEngine;
using System.Collections;

public class _PLAYERPREFSRESET : MonoBehaviour {

	// Use this for initialization
	void Start () {
		PlayerPrefs.DeleteAll();
	}
}
