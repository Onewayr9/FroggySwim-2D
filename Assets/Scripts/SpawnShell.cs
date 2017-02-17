using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnShell : MonoBehaviour {

	public GameObject shell;
	public Transform shellSpawn;

	// Use this for initialization
	void Start () {
		StartCoroutine (spawn ());
	}
	
	// Update is called once per frame
//	void Update () {
//		
//	}

	IEnumerator spawn() {
		yield return new WaitForSeconds (3);
		while (true) {
			Instantiate (shell, shellSpawn.position, Quaternion.identity);
			yield return new WaitForSeconds (3);
		}
	}
}
