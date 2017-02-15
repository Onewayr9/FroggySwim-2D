using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	public int score = 0;

	[HideInInspector] public bool isGround = true;

	private BoardManager boardScript;
	private int level = 0;
	private Text levelText;

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
		levelText = GameObject.Find ("Level").GetComponent<Text> ();
		levelText.text = "Level " + (level + 1);
		boardScript.SetupScene (level);
	}

	public void GameOver() {
		levelText.text = "Game Over";
		enabled = false;
	}
}