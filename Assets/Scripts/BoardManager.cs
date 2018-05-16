using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {
	public GameObject[] levels;

	//store backgrounds
	//Reminder: Each level must have a background;
	//How to build a background: 1. add a Quad in scene, reset the position. Set the scale to 17.5, 10;
	//2. put the background image in sprites folder, set Texture type to Default, Wrap mode to repeat;
	//3. Drag the image to Quad; 4. addBGScroller script to the quad, set the speed to 0.2
	public GameObject[] backgrounds;

	private Transform boardHolder;

	public void SetupScene (int level)
	{
		Instantiate (backgrounds[level], new Vector3 (0, 0, 0), Quaternion.identity);
		//Instantiate Board and set boardHolder to its transform.
		boardHolder = new GameObject ("Board").transform;

		GameObject instance =
			Instantiate (levels[level], new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
		instance.transform.SetParent (boardHolder);
	}

	public int getTotalLevels() {
		return levels.Length;
	}
}
