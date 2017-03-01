using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearBarController : MonoBehaviour {
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			if (other.GetComponent<Rigidbody2D> ().velocity.y <= 0) {
				gameObject.SetActive (false);
			}
		}
	}
}
