using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TapBar : MonoBehaviour {
	[SerializeField]
	private float depreciationRate = 0.5f, valuePerTap = 10, endTotalProgress = 100, endTimer = 0, endTimeVariable = 2;

	private float currentProgress;
	private bool startDepreciation = false, endTap = false;

	private Image tapBarImage;

	private BeginLevel beginLevelController;

	// Use this for initialization
	void Start () {
		tapBarImage = GameObject.Find("Tap Bar").GetComponent<Image>();
		beginLevelController = GameObject.FindObjectOfType<BeginLevel>();
	}
	
	// Update is called once per frame
	void Update () {
		DepreciateBar();

		if (endTap) {
			endTimer += Time.deltaTime;

			if (endTimer >= endTimeVariable) {
				Destroy(gameObject);
			}
		}
	}

	public void RegisterTap () {
		startDepreciation = true;
		currentProgress += valuePerTap;

		UpdateBar();
	}

	public void OnMouseDown () {
		RegisterTap();
	}

	void UpdateBar () {
		tapBarImage.fillAmount = currentProgress / endTotalProgress;

		if (currentProgress >= endTotalProgress) {
			// end tap
			// begin launch
			beginLevelController.LaunchShip();
			// start timer to destroy bar
			endTap = true;
		}

		Mathf.Clamp(currentProgress, 0, 100);
	}

	void DepreciateBar () {
		if (startDepreciation && !endTap) {
			currentProgress -= depreciationRate;
			UpdateBar();
		}
	}

	void UpdateColor () {}
}
