using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CannonTimer : MonoBehaviour {

//	public Text timerText;
	private float startTime;
//	private GameManager gameManager;

	// Use this for initialization
	void Start () {
		startTime = Time.time;
//		gameManager = GetComponent<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.instance.getRestart() == true) {
			return;
		}
		float t = Time.time - startTime;
		string minutes = ((int)t / 60).ToString ();
		string seconds = (t % 60).ToString ("f2");
//		timerText.text = "Be A Man! Keep 8 seconds!" + minutes + ":" + seconds;
		if (t >= 20.0f) {
//			timerText.text = "Congratulations! You Win!";
			SceneManager.LoadScene ("CongratulateScene", LoadSceneMode.Single);
		}
	}
}
