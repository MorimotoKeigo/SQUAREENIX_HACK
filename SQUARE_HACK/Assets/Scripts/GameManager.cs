﻿using System.Collections;
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

	[SerializeField]
	public GameObject FadeUnit;


	[SerializeField]
	private AudioClip BGM;
	
	[SerializeField]
	private AudioClip LavaSE;	
	GameObject Camera;

	[SerializeField]
	public Vector3 LavaSpeed1;
	
	[SerializeField]
	public Vector3  LavaSpeed2;
	
	[SerializeField]
	public Vector3 LavaSpeed3;

	[SerializeField]
	public Vector3 CameraOffset;
	
	[SerializeField]
	public float ClearHeight;

	public float SceneChangeTime;
	//カメラ類
	[SerializeField]
	float maxPlayerPosY = 0;

	[SerializeField]
	//GameObject SceneUnit = null;

	void Awake(){
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			//このオブジェクトを破壊する
			Destroy(gameObject);
		}

		InitGame();
		
	}

	bool SECountFrag = false;

	bool GameStartFrag = false;

	bool  Starting = false;
	// Use this for initialization
	void Start () {
		SoundManager.instance.PlaySingleSound(BGM);
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.Space) && Starting == false){
			GameStart();
			Starting = true;
		}

		if(GameStartFrag == true){
		CalcCurrentFrame();
		
		
		CheckHitLava();
		MoveLavaPosition();
		
		MoveCamera();

			if(Player.transform.position.y > ClearHeight){
				GameClear();
			}
		}

	}

	void InitGame(){

		
		Camera = GameObject.Find("Main Camera");

		CurrentFrame = 0;


		FadeManager.FadeIn();
		//SceneUnit = GameObject.Find("FadeUnit");
		//SceneUnit.GetComponent<FadeSystem>().FadeIn();
		//FadeUnit.GetComponent<FadeSystem>().ChangeStagingType(1);
		//LavaPositon = new Vector3(0,0,0);
		//PlayerPosition = new Vector3(0,0,0);
		//ParticleManager.instance.PlayFX(new Vector3(0f,0f,0f),0);

	}

	void CalcCurrentFrame(){
		CurrentFrame += Time.deltaTime;
	}
	void GameOver(){
		//Debug.Log("testGameOver");
		//ParticleManager.instance.PlayFX_tap(new Vector3(0f,0f,0f));

		//ParticleManager.instance.PlayFX(new Vector3(0f,0f,0f),2);
		Invoke("SceneChange",1.0f);
	}

	void SceneChange(){
		
		//FadeUnit.GetComponent<FadeSystem>().ChangeStagingType(2);
		SoundManager.instance.StopSingleSound();
		SceneManager.LoadScene("RESULT");
	}
	public float GetCurrentFrameTime(){
		return CurrentFrame;
	}

	void CheckHitLava(){
		float distance = Player.transform.position.y - (Lava.transform.position.y + (Lava.transform.localScale.y / 2) );
		


		if(SECountFrag == false && distance <= 5f){
			SoundManager.instance.PlaySoundSE(LavaSE);
			SECountFrag = true;
		}
		
		if(distance >= 10f){
			SECountFrag = false;
		}
		
		
		if(distance <= 0){
			//Debug.Log("testCheckHitLava");
			GameOver();
		}
	}
	
	void MoveLavaPosition(){

		if(Player.transform.position.y - Lava.transform.position.y > 25f){
			Lava.transform.position = new Vector3(Lava.transform.position.x, Player.transform.position.y -  20f, Lava.transform.position.z); 
		}else{
			Lava.transform.position += LavaSpeed1;
		}
		//Lava.transform.position += new Vector3(0.0f,0.01f,0.0f);
		//Lava.transform.position += LavaSpeed1;
	}

	void MoveCamera(){
		
		
		//float offset = Player.transform.position.y - Camera.transform.position.y;
		
		
		if(Player.transform.position.y >  maxPlayerPosY){
			//Debug.Log(maxPlayerPosY);
			maxPlayerPosY = Player.transform.position.y;
		}
		
		
		if(Mathf.Abs(Camera.transform.position.y - maxPlayerPosY) > 0.3f){
			Vector3 wantPos = new Vector3(0,maxPlayerPosY,Camera.transform.position.z);
			Camera.transform.position = Vector3.Lerp(Camera.transform.position, wantPos + CameraOffset, 5 * Time.deltaTime);
		}
		
	}

	void GameStart(){
		GameStartFrag = true;
		//Player.GetComponent<Animator>().SetInteger("State", 4);
	}

	void GameClear(){
		TimeToResult.instance.GetComponent<TimeToResult>().RecordTime(GetCurrentFrameTime());
		
		FadeManager.FadeOut();
		Debug.Log("FadeFade");
		
		Invoke("SceneChange",SceneChangeTime);
	}


}
