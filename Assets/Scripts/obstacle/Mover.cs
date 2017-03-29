using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

	public float speed;

	// Use this for initialization
	void Start () {
		//this.transform.Translate (-Time.deltaTime * speed, 0, 0, Space.World);	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Translate (-Time.deltaTime * speed, 0, 0, Space.World);
	}
}
