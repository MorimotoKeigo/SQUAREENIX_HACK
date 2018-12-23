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
	
	//カメラ類
	float offsettmp = 0;
	float cameracount = 0;

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
		SoundManager.instance.PlaySingleSound(BGM);
	}
	
	// Update is called once per frame
	void Update () {
		CalcCurrentFrame();
		
		
		CheckHitLava();
		MoveLavaPosition();
		
		MoveCamera();
	}

	void InitGame(){


		Camera = GameObject.Find("Main Camera");

		CurrentFrame = 0;
		//FadeUnit.GetComponent<FadeSystem>().ChangeStagingType(1);
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
		
		//FadeUnit.GetComponent<FadeSystem>().ChangeStagingType(2);
		SoundManager.instance.StopSingleSound();
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
		//Lava.transform.position += new Vector3(0.0f,0.01f,0.0f);
		Lava.transform.position += LavaSpeed1;
	}

	void MoveCamera(){
		
		float offset = Player.transform.position.y - Camera.transform.position.y;
		
		//offsettmp += offset;

		/* 
		if(offsettmp>3f){
			
			Camera.transform.position += new Vector3(0.0f, offsettmp, 0.0f);
			offsettmp = 0;
			//cameracount = 0; 
		}
		*/
		Camera.transform.position += new Vector3(0.0f, offset, 0.0f);
			


	}
}
