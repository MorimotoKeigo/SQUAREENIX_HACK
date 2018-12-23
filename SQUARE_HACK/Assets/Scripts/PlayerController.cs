using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public enum PLAYER_STATE{
		RIGHT,
		LEFT,
		Air
	}

	[SerializeField]
	float velocityX,velocityY;
	[SerializeField]
	private PLAYER_STATE playerState;
	// Use this for initialization
	void Start () {
		playerState = PLAYER_STATE.RIGHT;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			Jump();
		}
		transform.position += new Vector3(velocityX,velocityY,0);

	}

	void Jump(){
		switch(playerState){
			case PLAYER_STATE.RIGHT:
			velocityX = -1;
			velocityY = 1;
			break;
			case PLAYER_STATE.LEFT:
			velocityX = 1;
			velocityY = 1;
			break;
			case PLAYER_STATE.Air:
			velocityX = 0;
			velocityY = 0;
			break;

		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		// if(other.gameObject.tag == "wall"){
			velocityX = 0;
			velocityY = 0;
			switch(playerState){
			case PLAYER_STATE.RIGHT:
			playerState = PLAYER_STATE.LEFT;
			break;
			case PLAYER_STATE.LEFT:
			playerState = PLAYER_STATE.RIGHT;
			break;
			case PLAYER_STATE.Air:
			break;
			}
		// }
	}

	public Vector3 GetPlayerPosition(){
		return transform.position;
	}
}
