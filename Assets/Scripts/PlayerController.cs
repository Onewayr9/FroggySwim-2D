using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
	public float restartLevelDelay = 1f;
	public Text LevelText;
	public Text ScoreText;
	public Text HPText;
	//
	public GameObject shield;
	private GameObject this_shield = null;
	//

	private Rigidbody2D rb2d;
	private int HP = 100;
	//
	private int pickUpCount = 0;
	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		LevelText.text = "Level " + (GameManager.instance.level + 1);
		ScoreText.text = "Score: " + GameManager.instance.score;
		HPText.text = "HP: " + HP;
	}

	void OnCollisionStay2D(Collision2D collision) {
		if (!GameManager.instance.isGround && collision.collider.tag == "Ground" && rb2d.velocity.y == 0) {
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
			//
			pickUpCount++;
			Debug.Log ("player pick up");
			if (pickUpCount >= 3) {
				Debug.Log("Shield being produced");
				this.gameObject.transform.GetChild(0).gameObject.SetActive(true);

				pickUpCount = 0;
			}
		} else if (other.tag == "Obstacle") {
			Debug.Log ("player obstacle");
			if (this.gameObject.transform.GetChild (0).gameObject.activeSelf) {
				Debug.Log ("Shield being deactive");
				this.gameObject.transform.GetChild (0).gameObject.SetActive (false);
			} else {
				HP -= 30;
			}
			pickUpCount = 0;
			other.gameObject.SetActive (false);
			CheckIfGameOver ();
		} else if (other.tag == "Death") {
			HP = 0;
			CheckIfGameOver ();
		} else if (other.tag == "Cactus") {
			Debug.Log ("Cactus");
			HP -= 60;
			other.gameObject.SetActive (false);
			CheckIfGameOver ();
		} else if (other.tag == "MovingWall") {
			Debug.Log ("Moving Wall");
			HP -= 10;
			other.gameObject.SetActive (false);
			CheckIfGameOver ();
		} else if (other.tag == "Shell") {
			HP = 0;
			Debug.Log ("Shell");
			other.gameObject.SetActive (false);
			CheckIfGameOver ();
		}
		ScoreText.text = "Score: " + GameManager.instance.score;
		HPText.text = "HP: " + HP;
	}
	
	// Update is called once per frame
	void Update () {
		bool isJump = false;
	#if UNITY_STANDALONE || UNITY_WEBPLAYER
		isJump = Input.GetKeyDown ("space");
	#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
		isJump = (Input.touchCount > 0);
	#endif

		if (GameManager.instance.isGround && isJump) {
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
			LevelText.text = "Game Over";
			//Call the GameOver function of GameManager.
			GameManager.instance.GameOver ();
		}
	}
		
}
