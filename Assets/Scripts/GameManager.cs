using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	public float levelStartDelay = 2f;
	public int maxNotLevelSceneIndex;

	public int score = 0;

	[HideInInspector] public bool isGround = true;
	[HideInInspector] public int level = 0;
	[HideInInspector] public bool finish = false;

	private bool restart = true;
	private GameObject resumeButton;
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
	}

	void OnLevelWasLoaded(int index)
	{
		// 
		if (index <= maxNotLevelSceneIndex) {
			restart = true;
			return;
		}
		if (restart) {
			restart = false;
			if (finish) {
				level = 0;
				finish = false;
			}
			score = 0;
		} else {
			level++;
		}
		//Call InitGame to initialize our level.
		InitGame();
	}

	void InitGame() {
		playerObject = GameObject.Find ("Player");
		playerObject.SetActive (false);

		resumeButton = GameObject.Find ("RestartButton");
		resumeButton.SetActive (false);

		levelImage = GameObject.Find("LevelImage");
		levelText = GameObject.Find("LevelText").GetComponent<Text>();
		levelText.text = "Level " + (level + 1);
		StartCoroutine(goAfterDelay());
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
		resumeButton.SetActive (true);
		enabled = false;
		restart = true;
	}

	public void RestartGame() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
	}

	IEnumerator goAfterDelay() {
		levelImage.SetActive (true);
		yield return new WaitForSecondsRealtime(levelStartDelay);
		levelImage.SetActive (false);
		boardScript.SetupScene (level);
		playerObject.SetActive (true);
	}

	public BoardManager getBoardManager() {
		return boardScript;
	}

	public bool getRestart() {
		return restart;
	}
}