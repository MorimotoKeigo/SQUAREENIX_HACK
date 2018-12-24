using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeToResult : MonoBehaviour {

	// Use this for initialization
	public static TimeToResult instance = null; 

	public float recordtime = 0;
	void Awake(){
		if (instance == null ) {
			instance = this;
		} else if (instance != this) {
			//このオブジェクトを破壊する
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject);
	}
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void RecordTime(float time){
		recordtime = time;
	}

	public float GetRecordTime(){
		return recordtime;
	}
}
