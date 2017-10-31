using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathmaker : MonoBehaviour {

public int maxTime;
float currentTime;
public Transform pathmakerSpherePrefab;
public static List<GameObject> tiles = new List<GameObject>();
public Transform mapObj;
public static int currentTiles;
public static int maxTiles = 500;
float maxCamSize;
static float camY;
static GameObject mainCam;

public GameObject[] tilePrefabs;

	void Awake(){
		mainCam = Camera.main.gameObject;
		maxCamSize =60;
		camY = mainCam.transform.position.y;
	}

	void Update () {
		if (currentTiles >= maxTiles){
			Destroy(gameObject);
		}
		
		else {
			float randNum = Random.Range(0f,1f);

			if (randNum < .075f) transform.Rotate(Vector3.up * 90); 
			else if (randNum < .15f && randNum >= .075f) transform.Rotate(-Vector3.up * 90);
			else if (randNum >=.96f && randNum <= 1f) Instantiate(pathmakerSpherePrefab, transform.position, Quaternion.identity, mapObj); 

			GameObject newObj = (GameObject) Instantiate(ChooseRandomObject(tilePrefabs), transform.position, Quaternion.identity, mapObj);
			tiles.Add(newObj);
			currentTiles ++;
			mainCam.transform.position = FindAveragePosition(tiles);
			mainCam.transform.position = new Vector3(mainCam.transform.position.x, camY, mainCam.transform.position.z);
			mainCam.GetComponent<Camera>().orthographicSize = ((float) currentTiles/maxTiles) * maxCamSize;
			transform.Translate(Vector3.forward * 2.0f);
		}

		currentTime += Time.fixedUnscaledDeltaTime;

		if (currentTime >= maxTime){
			Destroy(gameObject);
		}
	}

	IEnumerator RotateMe(){
		while (true){
		transform.Rotate(Vector3.up * 90f);
		yield return new WaitForSeconds (3f);
		}
	}

	GameObject ChooseRandomObject(GameObject[] obj){
		return obj[Random.Range(0, obj.Length)];
	}

	Vector3 FindAveragePosition(GameObject[] positions){
		Vector3 avg = Vector3.zero;
		foreach(GameObject gobj in positions){
			avg += gobj.transform.position;
		}
		avg /= positions.Length;
		return avg;
	}

	Vector3 FindAveragePosition(List<GameObject> positions){
		Vector3 avg = Vector3.zero;
		foreach(GameObject gobj in positions){
			avg += gobj.transform.position;
		}
		avg /= positions.Count;
		return avg;
	}


}

// OPTIONAL EXTRA TASKS TO DO, IF YOU WANT: ===================================================


// BETTER UI:
// learn how to use UI Sliders (https://unity3d.com/learn/tutorials/topics/user-interface-ui/ui-slider) 
// let us tweak various parameters and settings of our tech demo
// let us click a UI Button to reload the scene, so we don't even need the keyboard anymore!

// WALL GENERATION
// add a "wall pass" to your proc gen after it generates all the floors
// 1. raycast downwards around each floor tile (that'd be 8 raycasts per floor tile, in a square "ring" around each tile?)
// 2. if the raycast "fails" that means there's empty void there, so then instantiate a Wall tile prefab
// 3. ... repeat until walls surround your entire floorplan
// (technically, you will end up raycasting the same spot over and over... but the "proper" way to do this would involve keeping more lists and arrays to track all this data)
