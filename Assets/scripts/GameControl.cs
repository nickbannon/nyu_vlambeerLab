using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour {

	bool gameOver;
	public GameObject gameOverObj;

	// Use this for initialization
	void Awake () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!gameOver){
			if (Pathmaker.currentTiles == Pathmaker.maxTiles) {
				gameOver = true;
				gameOverObj.SetActive(true);
				Debug.Log("over");
			}
		}
		else{
			if (Input.GetKeyDown(KeyCode.R)) {
				Pathmaker.currentTiles = 0;
				Pathmaker.tiles = new List<GameObject>();
				SceneManager.LoadScene("mainLabScene");
			}
		}
	}
}
