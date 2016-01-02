using UnityEngine;
using System.Collections;

public class Scroller : MonoBehaviour {

	public float scrollSpeed;
	public float spaceScrollScale = 5;
	public float speedScale = 0;
	public float tileSizeY;
	public Camera backgroundCamera; 
	private Animator playerAnim;
	
	public Sprite[] backgroundSprites;
	private int currentSprite = 0;
	private int tilesSinceLastShift = 0;
	public int numTilesBetweenTransitions = 6;
	
	private bool isScrolling = false;
	private Vector3 startPosition;
	private bool detachmentHasOccured = false;
	public float timeSinceIsActive = 0;
	
	void Start ()
	{
		playerAnim = GameObject.Find ("Player").GetComponent<Animator>();
	}
	
	void Update ()
	{
		if (isScrolling) {
			timeSinceIsActive += Time.deltaTime;
			if (currentSprite < 6) {
				if (speedScale >= 1.0f) {
					speedScale = 1f;
				} else if (speedScale < 0.1f) {
					speedScale += Time.deltaTime * .05f;
				} else if (speedScale < 1.0f) {
					speedScale += Time.deltaTime * 0.5f;
				} 
			}
			
			transform.position += Vector3.down * scrollSpeed * Time.deltaTime * speedScale;

			foreach (Transform child in transform) {
				float offset = child.position.y + tileSizeY;
				if (offset < backgroundCamera.ViewportToWorldPoint (new Vector3(0, 0, 0)).y - 4) {
					child.localPosition += new Vector3 (0f , 24f, 0f);
					
					tilesSinceLastShift++;
					
					if (speedScale < 1 && currentSprite < 7) {
						currentSprite = 0;
					} else {
						if (currentSprite <= 1 && tilesSinceLastShift >= numTilesBetweenTransitions) {
							currentSprite++;
							tilesSinceLastShift = 0;
						} else if (currentSprite % 2 == 0 && currentSprite != 8 /*starfield*/) {
							currentSprite++; 
						} else if (currentSprite % 2 == 1 && tilesSinceLastShift >= numTilesBetweenTransitions) {
							currentSprite++;
							tilesSinceLastShift = 0;
						}
					}
					
					child.gameObject.GetComponent<SpriteRenderer>().sprite = backgroundSprites[currentSprite];
				}
			}
		}
		
		if (transform.position.y <= -540 && !detachmentHasOccured) {
			speedScale = spaceScrollScale;
			if (playerAnim) {
				playerAnim.SetTrigger ("DetachmentTrigger");
				detachmentHasOccured = true;
			}
		}
	}
	
	public void BeginScrolling () {
		isScrolling = true;
	}
}
