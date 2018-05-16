using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CongratulateSceneController : MonoBehaviour {

	public void OnStartGame () {
//		SceneManager.UnloadSceneAsync ("FroggySwim-2D");
		GameManager.instance.finish = true;
		SceneManager.LoadScene ("FroggySwim-2D", LoadSceneMode.Single);
	}
}