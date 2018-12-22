using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	
	//現在のFrameTime
	public static float CurrentFrame;



	[SerializeField]
	public Vector3 LavaPositon;
	public Vector3 PlayerPosition; 
	
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
	}

	void InitGame(){
		CurrentFrame = 0;
		LavaPositon = new Vector3(0,0,0);
		PlayerPosition = new Vector3(0,0,0);
	}

	void CalcCurrentFrame(){
		CurrentFrame += Time.deltaTime;
	}
	void GameOver(){
		Debug.Log("testGameOver");
	}

	float GetCurrentFrameTime(){
		return CurrentFrame;
	}

	void CheckHitLava(){
		var distance = PlayerPosition.y - LavaPositon.y;
		if(distance <= 0){
			Debug.Log("testCheckHitLava");
			GameOver();
		}
	}
	
}
