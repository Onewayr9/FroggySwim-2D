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

	// sound effects
	public AudioClip collectCoins;
	public AudioClip death;
	public AudioClip health;
	public AudioClip obstacle;
	public AudioClip healthKit;
	public AudioClip shieldSound;

	// sound source
	private AudioSource source;

	private Rigidbody2D rb2d;
	private int HP = 100;
	//
	private int pickUpCount = 0;
	// new
	private bool transport = false;
	private double xPosition;

	// new fetures
	private bool hasKey = false;
	private bool hasMushroom = false;
	public GameObject bridge;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		LevelText.text = "Level " + (GameManager.instance.level + 1);
		Debug.Log (GameManager.instance.level);
		ScoreText.text = "Score: " + GameManager.instance.score;
		HPText.text = "HP: " + HP;

		// init sound source
		source = GetComponent<AudioSource> ();
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
			// HP += 10;
			other.gameObject.SetActive (false);
			//
//			pickUpCount++;
////			Debug.Log ("player pick up");
//			if (pickUpCount >= 3) {
////				Debug.Log("Shield being produced");
//				this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
//
//				pickUpCount = 0;
//			}
			source.PlayOneShot (collectCoins, 1.0f);
		} else if (other.tag == "HealthKit") {
			HP += 10;
			other.gameObject.SetActive (false);
			source.PlayOneShot (healthKit, 1.0f);
		} else if (other.tag == "Transport") {
//			double xPosition = rb2d.transform.position.x;
//			xPosition += 25;
//			rb2d.transform.position.x = xPosition;
			transport = true;
			other.gameObject.SetActive (false);
		} else if (other.tag == "Heart") {
			HP += 15;
			other.gameObject.SetActive (false);
			source.PlayOneShot (health, 1.0f);
		} else if (other.tag == "shield") {
			pickUpCount++;
			if (pickUpCount >= 1) {
//				Debug.Log("Shield being produced");
				this.gameObject.transform.GetChild (0).gameObject.SetActive (true);
				pickUpCount = 0;
			}
			other.gameObject.SetActive (false);
			source.PlayOneShot (shieldSound, 1.0f);
		} else if (other.tag == "Obstacle") {
//			Debug.Log ("player obstacle");
			source.PlayOneShot (obstacle, 1.0f);
			if (this.gameObject.transform.GetChild (0).gameObject.activeSelf) {
//				Debug.Log ("Shield being deactive");
				this.gameObject.transform.GetChild (0).gameObject.SetActive (false);
			} else {
				HP -= 60;
			}
			pickUpCount = 0;
			other.gameObject.SetActive (false);
			CheckIfGameOver ();
		} else if (other.tag == "Death") {
			HP = 0;
			CheckIfGameOver ();
		} else if (other.tag == "Cactus") {
//			Debug.Log ("Cactus");
			HP -= 60;
			other.gameObject.SetActive (false);
			CheckIfGameOver ();
		} else if (other.tag == "MovingWall") {
//			Debug.Log ("Moving Wall");
			HP -= 10;
			other.gameObject.SetActive (false);
			CheckIfGameOver ();
		} else if (other.tag == "Shell") {
//			HP = 0;
			if (this.gameObject.transform.GetChild (0).gameObject.activeSelf) {
				this.gameObject.transform.GetChild (0).gameObject.SetActive (false);
			} else {
				HP -= 45;
			}
//			Debug.Log ("Shell");
			other.gameObject.SetActive (false);
			CheckIfGameOver ();
		} else if (other.tag == "key") {
			hasKey = true;
			other.gameObject.SetActive (false);
		} else if (other.tag == "door") {
			if (hasKey == false) {
				HP = 0;
			} else {
				other.gameObject.SetActive (false);
			}
			CheckIfGameOver ();
		} else if (other.tag == "Mushroom") {
			hasMushroom = true;
			other.gameObject.SetActive (false);
//			// build bridge
//			bridge = GameObject.Find("Bridge");
//			Vector3 p = new Vector3 (21.26f, -4.33f, 0.0f);
//			bridge.transform.position = p;
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
			rb2d.AddForce (new Vector2 (0, 650));
			GameManager.instance.isGround = false;
		}
		if (hasMushroom) {
			// build bridge
			bridge = GameObject.Find("Bridge");
			bridge.transform.position = new Vector3 (bridge.transform.position.x, bridge.transform.position.y + 100, 0);
			hasMushroom = false;
//			rb2d.transform.position = new Vector3(rb2d.transform.position.x + 20, rb2d.transform.position.y, 0);
//			transport = false;
		}
	}

	//Restart reloads the scene when called.
	private void Restart ()
	{
		//Load the last scene loaded, in this case Main, the only scene in the game. And we load it in "Single" mode so it replace the existing one
		//and not load all the scene object in the current scene.
		BoardManager boardScript = GameManager.instance.getBoardManager();
		if (GameManager.instance.level + 1 == boardScript.getTotalLevels()) {
			Debug.Log (GameManager.instance.level + ": Cong!");
//			Destroy (GameManager.instance);
			SceneManager.LoadScene ("CongratulateScene", LoadSceneMode.Single);

			return;
		}
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
	}

	private void CheckIfGameOver ()
	{
		//Check if food point total is less than or equal to zero.
		if (HP <= 0) 
		{
			source.PlayOneShot (death, 1.0f);
			LevelText.text = "Game Over";
			//Call the GameOver function of GameManager.
			GameManager.instance.GameOver ();
		}
	}
}
