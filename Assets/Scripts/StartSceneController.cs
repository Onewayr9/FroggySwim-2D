using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneController : MonoBehaviour {

	public void OnStartGame () {
		SceneManager.LoadScene ("FroggySwim-2D", LoadSceneMode.Single);
	}
}
