using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarDisappear : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log ("player");
		if(other.tag == "Player") {
			Rigidbody2D rb2d = other.GetComponent<Rigidbody2D>();
			if (rb2d.velocity.y < 0) {
				Debug.Log ("falling ? ");
				Destroy (this.gameObject);
			}
		}
	}

}
