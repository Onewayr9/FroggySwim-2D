using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellBehavior : MonoBehaviour {

	private Rigidbody2D rb2d;
	public Vector2 horizontal_velocity;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		rb2d.velocity = horizontal_velocity;
	}
	
//	Update is called once per frame
//	void Update () {
//		
//	}
}
