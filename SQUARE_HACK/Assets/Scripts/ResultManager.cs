using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ResultManager : MonoBehaviour {


	[SerializeField]
	private AudioClip BGM;
	
	[SerializeField]
	private AudioClip SE;
	


	// Use this for initialization
	void Start () {
		SoundManager.instance.PlaySingleSound(BGM);	
		Debug.Log("Result Time" + TimeToResult.instance.GetComponent<TimeToResult>().GetRecordTime());
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKeyDown(KeyCode.Space)){
			ChangeScene();
		}
		


	}

	void ChangeScene(){
		SoundManager.instance.PlaySoundSE(SE);
		SceneManager.LoadScene("GAME");
	}
}
