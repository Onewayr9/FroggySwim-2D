using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpAndDown : MonoBehaviour {

	private Vector2 startPosition;
	private Vector2 endPosition;
	private Rigidbody2D rb2d;
	private float deltaY;
	public int gravityScale;
	public float upSpeed;
	private bool isFalling;

	// Use this for initialization
	void Start () {
		isFalling = true;
		deltaY = 0;
		startPosition = this.transform.position;
		rb2d = GetComponent<Rigidbody2D> ();
		rb2d.gravityScale = gravityScale;
	}

	void Update() {
		if (isFalling == false) {
			if (transform.position.y >= startPosition.y) {
				rb2d.isKinematic = false;
				isFalling = true;
				return;
			}
			transform.position = endPosition + deltaY * Vector2.up;
			deltaY += Time.deltaTime * upSpeed;
		}
	}
	
	void OnTriggerEnter2D (Collider2D other) {
//		Debug.Log ("YES");
		rb2d.velocity = Vector2.zero;
		rb2d.angularVelocity = 0.0f;
		rb2d.isKinematic = true;
		isFalling = false;
		endPosition = transform.position;
		deltaY = 0;
	}
}
