using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrintResult : MonoBehaviour {


	public PlayerController playerCtrl;
	private Text resultText;
	// Use this for initialization
	void Awake() {
		resultText = GetComponent<Text>();
	}
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		switch(playerCtrl.GetJustResult()){
			case PlayerController.JUST_RESULT.BAD:
				resultText.text = "Bad";
				Debug.Log("aa");
				break;
			case PlayerController.JUST_RESULT.GOOD:
				resultText.text = "Good";
				Debug.Log("aa");

				break;
			case PlayerController.JUST_RESULT.PERFECT:
				resultText.text = "Perfect";
				Debug.Log("aa");

				break;

		}
	}
}
