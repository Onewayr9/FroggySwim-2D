using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary {
	public float xMin, xMax, yMin, yMax;
}

public class ShellBehavior : MonoBehaviour {

	public float dodge;
	public float smoothing;
	public Boundary boundary;
	public Vector2 maneuverTime;

	private float currentSpeed;
	private float targetManeuver;
	private Rigidbody2D rb2d;

	void Start() {
		rb2d = GetComponent<Rigidbody2D> ();
		currentSpeed = rb2d.velocity.x;
		StartCoroutine (Evade ());
	}

	IEnumerator Evade() {
		while (true) {
			targetManeuver = Random.Range (1, dodge) * -Mathf.Sign (this.transform.position.y);
			Debug.Log (targetManeuver);
			yield return new WaitForSeconds (Random.Range (maneuverTime.x, maneuverTime.y));
			targetManeuver = 0;
			//			yield return new WaitForSeconds (Random.Range (maneuverTime.x, maneuverTime.y));
		}
	}

	void FixedUpdate() {
		float newManeuver = Mathf.MoveTowards (rb2d.velocity.y, targetManeuver, Time.deltaTime * smoothing);
		rb2d.velocity = new Vector2 (currentSpeed, newManeuver);
		rb2d.position = new Vector2 (
			Mathf.Clamp(rb2d.position.x, boundary.xMin, boundary.xMax),
			Mathf.Clamp(rb2d.position.y, boundary.yMin, boundary.yMax)
		);

	}

}
