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
	private float angle = 30;
	// Use this for initialization
	private int jumpCnt = 0;
	void Start () {
		playerState = PLAYER_STATE.RIGHT;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			Jump();
		}
		transform.position += new Vector3(Mathf.Cos(angle) * velocityX,Mathf.Sin(angle) * velocityY,0);

	}

	void Jump(){
		switch(playerState){
			case PLAYER_STATE.RIGHT:
				if(jumpCnt == 0)
				{
					velocityX = -1f;
					velocityY = 1f;
					angle = 30 * Mathf.Deg2Rad;
					jumpCnt++;
				}else if(jumpCnt == 1)
				{
					velocityX = -1f;
					velocityY = 1f;
					angle = 60 * Mathf.Deg2Rad;
					jumpCnt++;
				}
			break;
			case PLAYER_STATE.LEFT:
				if(jumpCnt == 0)
				{
					velocityX = 1f;
					velocityY = 1f;
					angle = 30 * Mathf.Deg2Rad;
					jumpCnt++;
				}else if(jumpCnt == 1)
				{
					velocityX = 1f;
					velocityY = 1f;
					angle = 60 * Mathf.Deg2Rad;
					jumpCnt++;
				}
			break;
			case PLAYER_STATE.Air:
				velocityX = 0;
				velocityY = 0;
				jumpCnt = 0;
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
			jumpCnt = 0;
			break;
			case PLAYER_STATE.LEFT:
			playerState = PLAYER_STATE.RIGHT;
			jumpCnt = 0;
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
