using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
	public float restartLevelDelay = 1f;
	public Text ScoreText;
	public Text HPText;

	private Rigidbody2D rb2d;
	private int HP = 100;
	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		ScoreText.text = "Score: " + GameManager.instance.score;
		HPText.text = "HP: " + HP;
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (!GameManager.instance.isGround && collision.collider.tag == "Ground") {
			GameManager.instance.isGround = true;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "NextLevel") {
			//Invoke the Restart function to start the next level with a delay of restartLevelDelay (default 1 second).
			Invoke ("Restart", restartLevelDelay);

			//Disable the player object since level is over.
			enabled = false;
		} else if (other.tag == "Coin") {
			GameManager.instance.score += 10;
			HP += 10;
			other.gameObject.SetActive (false);
		} else if (other.tag == "Obstacle") {
			HP -= 30;
			CheckIfGameOver ();
		}
		ScoreText.text = "Score: " + GameManager.instance.score;
		HPText.text = "HP: " + HP;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.instance.isGround && Input.GetKeyDown ("space")) {
			rb2d.AddForce (new Vector2 (0, 1400));
			GameManager.instance.isGround = false;
		}
	}

	//Restart reloads the scene when called.
	private void Restart ()
	{
		//Load the last scene loaded, in this case Main, the only scene in the game. And we load it in "Single" mode so it replace the existing one
		//and not load all the scene object in the current scene.
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
	}

	private void CheckIfGameOver ()
	{
		//Check if food point total is less than or equal to zero.
		if (HP <= 0) 
		{
			//Call the GameOver function of GameManager.
			GameManager.instance.GameOver ();
		}
	}
}
