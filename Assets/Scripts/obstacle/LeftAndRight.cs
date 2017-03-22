using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftAndRight : MonoBehaviour {

	public float distance;
	public float speed;
	private Vector2 startPosition;

	// Use this for initialization
	void Start () {
		startPosition = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		float newPosition = Mathf.Repeat (Time.time * speed, 2 * distance);
		if (newPosition >= distance) {
			transform.position = startPosition + (2 * distance - newPosition) * Vector2.right;
		} 
		else {
			transform.position = startPosition + newPosition * Vector2.right;
		}
	}
}
