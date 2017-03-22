using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	public float levelStartDelay = 2f;

	public int score = 0;

	[HideInInspector] public bool isGround = true;
	[HideInInspector] public int level = 0;

	private Text levelText;
	private GameObject playerObject;
	private GameObject levelImage;
	private BoardManager boardScript;

	void Awake () {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}

		DontDestroyOnLoad (gameObject);

		boardScript = GetComponent<BoardManager> ();

		InitGame ();
	}

	void OnLevelWasLoaded(int index)
	{
		//Add one to our level number.
		level++;
		//Call InitGame to initialize our level.
		InitGame();
	}

	void InitGame() {
		playerObject = GameObject.Find ("Player");
		playerObject.SetActive (false);
////
		levelImage = GameObject.Find("LevelImage");
		levelText = GameObject.Find("LevelText").GetComponent<Text>();
		levelText.text = "Level " + (level + 1);
		StartCoroutine(goAfterDelay());


//		levelImage.SetActive(true);
//		Invoke("HideLevelImage", levelStartDelay);
//		HideLevelImage ();
//		boardScript.SetupScene (level);
//		playerObject.SetActive (true);
	}

	//Hides black image used between levels
	void HideLevelImage()
	{
		//Disable the levelImage gameObject.
		levelImage.SetActive(false);
	}

	public void GameOver() {
		levelText.text = "Game Over";
		levelImage.SetActive (true);
		enabled = false;
	}

	IEnumerator goAfterDelay() {
		levelImage.SetActive (true);
		yield return new WaitForSecondsRealtime(levelStartDelay);
		levelImage.SetActive (false);
		boardScript.SetupScene (level);
		playerObject.SetActive (true);
	}
}