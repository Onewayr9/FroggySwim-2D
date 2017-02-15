using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftAndDown : MonoBehaviour {

//	private bool isUp;
	private Vector2 startPosition;
	public float speed;
	public float size;

	// Use this for initialization
	void Start () {
//		isUp = true;
		startPosition = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		float newPosition = Mathf.Repeat (Time.time * speed, size * 2);
		if (newPosition >= size) {
			transform.position = startPosition + Vector2.up * (2 * size - newPosition);
		} 
		else {
			transform.position = startPosition + Vector2.up * newPosition;
		}
	}
}
