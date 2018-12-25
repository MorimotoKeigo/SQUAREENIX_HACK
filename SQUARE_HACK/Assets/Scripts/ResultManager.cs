using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ResultManager : MonoBehaviour {


	[SerializeField]
	private float rankS;
	[SerializeField]
	private float rankA;
	[SerializeField]
	private float rankB;
	[SerializeField]
	private float rankC;

	[SerializeField]
	private AudioClip BGM;
	
	[SerializeField]
	private AudioClip SE;
	
	public Text RankText;

	public float Score;
	// Use this for initialization
	void Start () {
		//SoundManager.instance.PlaySingleSound(BGM);	
		Score = TimeToResult.instance.GetRecordTime();
		//Debug.Log("Result Time" + TimeToResult.instance.GetComponent<TimeToResult>().GetRecordTime());
	}
	
	// Update is called once per frame
	void Update () {

		
		
		if(Input.GetKeyDown(KeyCode.Space)){
			ChangeScene();
		}
		
		displayRank();

	}

	void ChangeScene(){
		SoundManager.instance.PlaySoundSE(SE);
		SceneManager.LoadScene("GAME");
	}

	void displayRank(){
		if(Score < rankC){
			RankText.GetComponent<Text>().text = "rankC";
		}else if(Score < rankB){
			RankText.GetComponent<Text>().text = "rankB";
		}else if(Score < rankA){
			RankText.GetComponent<Text>().text = "rankA";
		}else if(Score < rankS){
			RankText.GetComponent<Text>().text = "rankS";
		}
	}
}
