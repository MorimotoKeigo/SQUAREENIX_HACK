using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeightController : MonoBehaviour {
	public List<Sprite> HeightSprite;
	//public float nowTime;
	//public float startTime;
	//public Text timeText;
	public List<Image> HeightImage;

	//public static float TookTime;
	// Use this for initialization

	public float Height;
	void Awake() {
		//timeText = GetComponent<Text>();
	}
	void Start () {
		//startTime = GameManager.instance.GetCurrentFrameTime();
		Height = GameManager.instance.GetHeight();
	}
	
	// Update is called once per frame
	void Update () {
		
		Height = GameManager.instance.GetHeight();

		//int minute = (int)(Height / 10000f);
		//int second = (int)(Height / 100f);
		//var millis = (int)(Height % 100);


		int dig_one = (int)(Height % 10); Height /= 10;
		int dig_two = (int)(Height % 10); Height /= 10;
		int dig_three = (int)(Height % 10); Height /= 10;
		int dig_four = (int)(Height % 10); Height /= 10;
		int dig_five = (int)(Height % 10); Height /= 10;
		int dig_six = (int)(Height % 10);
		



//		var millisStr = millis.ToString().Remove(0,2).Remove(2);
		//HeightImage[0].sprite = HeightSprite[minute / 10];
		//HeightImage[1].sprite = HeightSprite[minute % 10];
		//HeightImage[2].sprite = HeightSprite[second / 10];
		//HeightImage[3].sprite = HeightSprite[second % 10];
		//HeightImage[4].sprite = HeightSprite[millis / 10];
		//HeightImage[5].sprite = HeightSprite[millis % 10];



		HeightImage[0].sprite = HeightSprite[dig_six];
		HeightImage[1].sprite = HeightSprite[dig_five];
		HeightImage[2].sprite = HeightSprite[dig_four];
		HeightImage[3].sprite = HeightSprite[dig_three];
		HeightImage[4].sprite = HeightSprite[dig_two];
		HeightImage[5].sprite = HeightSprite[dig_one];
		//HeightImage[4].sprite = HeightSprite[Int16.Parse(millisStr.Substring(0,1))];
		//HeightImage[5].sprite = HeightSprite[Int16.Parse(millisStr.Substring(1,1))];
		
		/* 
		nowTime = GameManager.instance.GetCurrentFrameTime();
		// Debug.Log(nowTime - startTime);
		var diffTime = nowTime - startTime;
		int minute = (int)(diffTime / 60f);
		int second = (int)(diffTime % 60f);
		var millis = diffTime % 1;
		var millisStr = millis.ToString().Remove(0,2).Remove(2);
		timeImage[0].sprite = timeSprite[minute / 10];
		timeImage[1].sprite = timeSprite[minute % 10];
		timeImage[2].sprite = timeSprite[second / 10];
		timeImage[3].sprite = timeSprite[second % 10];
		timeImage[4].sprite = timeSprite[Int16.Parse(millisStr.Substring(0,1))];
		timeImage[5].sprite = timeSprite[Int16.Parse(millisStr.Substring(1,1))];
		
		*/


	
		//TookTime = diffTime;
		// if(second / 10 == 0)
		// 	timeText.text = minute + ":0" + second + "." + millisStr;
		// else
		// 	timeText.text = minute + ":" + second + "." + millisStr;

	}
}