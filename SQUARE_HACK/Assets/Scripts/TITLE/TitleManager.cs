using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour {

	[SerializeField]
	private AudioClip BGM;
	
	[SerializeField]
	private AudioClip SE1;
	// Use this for initialization
	void Start () {
		SoundManager.instance.PlaySingleSound(BGM);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			ChangeScene();		
		}
	}

	void ChangeScene(){
		SoundManager.instance.PlaySoundSE(SE1);
		SceneManager.LoadScene("GAME");
	}
}
