using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	private Rigidbody2D rb2d;
	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (!GameManager.instance.isGround && collision.collider.tag == "Ground") {
			GameManager.instance.isGround = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.instance.isGround && Input.GetKeyDown ("space")) {
			rb2d.AddForce (new Vector2 (0, 1200));
			GameManager.instance.isGround = false;
		}
	}
}
