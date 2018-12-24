using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultTimeController : MonoBehaviour {
	public List<Sprite> timeSprite;
	//public float nowTime;
	//public float startTime;
	//public Text timeText;
	public List<Image> timeImage;
	
	public float ClearTime;
	// Use this for initialization
	void Awake() {
		//timeText = GetComponent<Text>();
	}
	void Start () {
		//startTime = GameManager.instance.GetCurrentFrameTime();
		
	}
	
	// Update is called once per frame
	void Update () {

		ClearTime = TimeToResult.instance.GetRecordTime();
		//nowTime = GameManager.instance.GetCurrentFrameTime();
		// Debug.Log(nowTime - startTime);
		//var diffTime = nowTime - startTime;
		

		int minute = (int)(ClearTime / 60f);
		int second = (int)(ClearTime % 60f);
		var millis = ClearTime % 1;
		var millisStr = millis.ToString().Remove(0,2).Remove(2);
		timeImage[0].sprite = timeSprite[minute / 10];
		timeImage[1].sprite = timeSprite[minute % 10];
		timeImage[2].sprite = timeSprite[second / 10];
		timeImage[3].sprite = timeSprite[second % 10];
		timeImage[4].sprite = timeSprite[Int16.Parse(millisStr.Substring(0,1))];
		timeImage[5].sprite = timeSprite[Int16.Parse(millisStr.Substring(1,1))];
		
		/* 

		int dig_one = (int)(ClearTime % 60); ClearTime /= 60;
		int dig_two = (int)(ClearTime % 60); ClearTime /= 60;
		int dig_three = (int)(ClearTime % 60); ClearTime /= 60;
		int dig_four = (int)(ClearTime % 60); ClearTime /= 60;
		int dig_five = (int)(ClearTime % 60); ClearTime /= 60;
		int dig_six = (int)(ClearTime % 60);

		timeImage[0].sprite = timeSprite[dig_six];
		timeImage[1].sprite = timeSprite[dig_five];
		timeImage[2].sprite = timeSprite[dig_four];
		timeImage[3].sprite = timeSprite[dig_three];
		timeImage[4].sprite = timeSprite[dig_two];
		timeImage[5].sprite = timeSprite[dig_one];
		
		*/
		// if(second / 10 == 0)
		// 	timeText.text = minute + ":0" + second + "." + millisStr;
		// else
		// 	timeText.text = minute + ":" + second + "." + millisStr;

	}
}