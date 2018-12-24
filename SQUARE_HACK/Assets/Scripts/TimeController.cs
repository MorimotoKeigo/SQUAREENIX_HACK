using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour {
	public float nowTime;
	public float startTime;
	public Text timeText;
	// Use this for initialization
	void Awake() {
		timeText = GetComponent<Text>();
	}
	void Start () {
		startTime = GameManager.instance.GetCurrentFrameTime();
		
	}
	
	// Update is called once per frame
	void Update () {
		nowTime = GameManager.instance.GetCurrentFrameTime();
		// Debug.Log(nowTime - startTime);
		var diffTime = nowTime - startTime;
		int minute = (int)(diffTime / 60f);
		int second = (int)(diffTime % 60f);
		var millis = diffTime % 1;
		var millisStr = millis.ToString().Remove(0,2).Remove(3);
		if(second / 10 == 0)
			timeText.text = minute + ":0" + second + "." + millisStr;
		else
			timeText.text = minute + ":" + second + "." + millisStr;

	}
}