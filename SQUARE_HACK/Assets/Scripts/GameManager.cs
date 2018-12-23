using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	
	//現在のFrameTime
	public static float CurrentFrame;

	[SerializeField]
	//public Vector3 LavaPositon;
	public GameObject Lava;
	
	//public Vector3 PlayerPosition; 
	public GameObject Player;

	void Awake(){
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			//このオブジェクトを破壊する
			Destroy(gameObject);
		}

		InitGame();
		
	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		CalcCurrentFrame();
		Debug.Log(GetCurrentFrameTime());
		CheckHitLava();
		MoveLavaPosition();
	}

	void InitGame(){
		CurrentFrame = 0;
		//LavaPositon = new Vector3(0,0,0);
		//PlayerPosition = new Vector3(0,0,0);
	}

	void CalcCurrentFrame(){
		CurrentFrame += Time.deltaTime;
	}
	void GameOver(){
		Debug.Log("testGameOver");
		Invoke("SceneChange",3.0f);
	}

	void SceneChange(){
		SceneManager.LoadScene("RESULT");
	}
	float GetCurrentFrameTime(){
		return CurrentFrame;
	}

	void CheckHitLava(){
		float distance = Player.transform.position.y - Lava.transform.position.y;
		if(distance <= 0){
			Debug.Log("testCheckHitLava");
			GameOver();
		}
	}
	
	void MoveLavaPosition(){
		Lava.transform.position += new Vector3(0.0f,0.3f,0.0f);
	}
}
